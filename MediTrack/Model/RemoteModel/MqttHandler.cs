﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;

namespace MediTrack.Model.RemoteModel
{
    public class MQTTHandler
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
            const string topic = "Vitaldaten";
            ushort msgId = client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            Console.WriteLine("Subscribed to topic: " + topic);
            Console.WriteLine("Subscription succ, msgId:" + msgId);
        }
        void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("MediTrack Subscribed for id = " + e.MessageId);
        }
        void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Message) + " (MediTrackon topic " + e.Topic + ")");
        }
        public void Disconnect()
        {
            client.Disconnect();
        }
    }
}
