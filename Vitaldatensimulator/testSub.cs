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
    class MQTT
    {
        private MqttClient client;

        void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }

        void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Message) + " (on topic " + e.Topic + ")");
        }

        public void Connect()
        {
            // mqtt client anlegen
            string hostName = "mqtt.inftech.hs-mannheim.de";
            int port = 8883;
            client = new MqttClient(hostName, port, true, null, null, MqttSslProtocols.TLSv1_2);

            // Verbindung aufbauen
            string user = "23pms01";
            string pwd = "c3c242ff";
            client.Connect(Guid.NewGuid().ToString(), user, pwd);
            Console.WriteLine("connected: " + client.IsConnected);
            Console.WriteLine("------------------------------------------");
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
        }

        public void Subscribe()
        {
            Console.WriteLine("subscribing to ...");
            string topic = "Herzschlag/test";
            ushort msgId = client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            Console.WriteLine("Subscribed to topic: " + topic);
            Console.WriteLine("Subscription Successfull, msgid: " + msgId);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

    }

}
