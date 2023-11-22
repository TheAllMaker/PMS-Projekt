using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using System.Diagnostics;
namespace MediTrack.Model.RemoteModel
{
    public class MqttHandler
    {
        private MqttClient client;


        public void ConnectToServer()
        {
            const string user = "23pms01";
            const string pwd = "c3c242ff";
            const string ServerName = "mqtt.inftech.hs-mannheim.de";
            int PortIdentifier = 8883;

            client = new MqttClient(ServerName, PortIdentifier, true, null, null, MqttSslProtocols.TLSv1_2);

            client.Connect(Guid.NewGuid().ToString(), user, pwd);
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

        }
        
        public void SubScribeToTopic()
        {
            const string topic = "23pms01/test";
            ushort msgId = client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            Debug.WriteLine("Subscribed to topic: " + topic);
            Debug.WriteLine("Subscription succ, msgId:" + msgId);
            
        }
       public void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("MediTrack Subscribed for id = " + e.MessageId);
        }
        public void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Aufruf");
            Debug.WriteLine(Encoding.UTF8.GetString(e.Message) + " (MediTrackon on topic " + e.Topic + ")");
            Console.WriteLine(Encoding.UTF8.GetString(e.Message) + " (MediTrackon on topic " + e.Topic + ")");
            string topic = e.Topic;
            string message = System.Text.Encoding.UTF8.GetString(e.Message);
            Console.WriteLine($"Received message on topic '{topic}': {message}");
        }
        public void Disconnect()
        {
            client.Disconnect();
        }
    }
}
