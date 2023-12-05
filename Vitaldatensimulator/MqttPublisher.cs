using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Vitaldatensimulator
{
    public class MqttPublisher
    {
        private const string HostName = "mqtt.inftech.hs-mannheim.de";
        private const string User = "23pms01";
        private const string Pwd = "c3c242ff";
        private const string Topic = "23pms01/test";
        private const int Port = 8883;
        private static MqttClient client;

        public MqttPublisher()
        {
            client = new MqttClient(HostName, Port, true, null, null, MqttSslProtocols.TLSv1_2);
            client.Connect(Guid.NewGuid().ToString(), User, Pwd);
        }
        public bool IsConnected
        {
            get
            {
                return client.IsConnected;
            }
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        public void PublishVitaldata(int data)
        {
            string payload = data.ToString();
            client.Publish(Topic, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }

        public void PublishVitaldataJSON(string jsonVitaldata)
        {
            client.Publish(Topic, Encoding.UTF8.GetBytes(jsonVitaldata), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }
    }

}

