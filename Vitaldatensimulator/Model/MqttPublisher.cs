using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace VitaldataSimulator
{
    public class MqttPublisher
    {
        private const string HostName = "mqtt.inftech.hs-mannheim.de";
        private const string User = "23pms01";
        private const string Pwd = "c3c242ff";
        private const string Topic = "23pms01/test";
        private const int Port = 8883;

        private static MqttClient _client;
        private static MqttPublisher _instance;
        public static event EventHandler<VitalData> VitalDataUpdated;
        public static bool IsSendingData = true;

        public MqttPublisher()
        {
            _client = new MqttClient(HostName, Port, true, null, null, MqttSslProtocols.TLSv1_2);
            _client.Connect(Guid.NewGuid().ToString(), User, Pwd);
        }

        public static MqttPublisher GetInstance()
        {
            return _instance ?? (_instance = new MqttPublisher());
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public void SendVitalData(VitalData singleMonitor)
        {
            var vitaldaten = singleMonitor.GetVitalData();
            if (singleMonitor.Alive == 0)
            {
                PublishVitaldataJson(vitaldaten);
                IsSendingData = false;
                _client.Disconnect();
            }
            else
            {
                VitalDataUpdated?.Invoke(null, singleMonitor);
                PublishVitaldataJson(vitaldaten);
            }
        }

        public void PublishVitaldataJson(string jsonVitaldata)
        {
            _client.Publish(Topic, Encoding.UTF8.GetBytes(jsonVitaldata), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }
    }

}

