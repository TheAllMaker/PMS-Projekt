﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace VitaldataSimulator
{
    public class MqttPublisher
    {
        private MqttClient client;

        public MqttPublisher(string brokerAddress,int port, string user, string pwd)
        {
            client = new MqttClient(brokerAddress, port, true, null, null, MqttSslProtocols.TLSv1_2);
            client.Connect(Guid.NewGuid().ToString(), user, pwd);
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

        public void PublishVitaldata(string topic, string json)
        {
            //string payload = heartbeat.ToString();
            client.Publish(topic, Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }
    }

}

