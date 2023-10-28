﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Vitaldatensimulator
{
    class VitaldatenSimulator
    {
        static async Task Main()
        {
            //MQTT myMqtt = new MQTT();
           // myMqtt.Connect();
            //myMqtt.Subscribe();

//            Console.WriteLine("Ich warte auf den MQTT-Client ...");

            string hostName = "mqtt.inftech.hs-mannheim.de";
            string user = "23pms01";
            string pwd = "c3c242ff";
            string topic = "Herzschlag/test";
            int port = 8883;
            MqttPublisher publisher = new MqttPublisher(hostName, port, user, pwd);
            //Console.WriteLine("connected: " + publisher.IsConnected);
            Random r = new Random();

            while (true)
            {
                int Herzschlag = r.Next(60, 80);
                Console.WriteLine("Herzschlag Test: " + Herzschlag);

                // Herzschlagwert über MQTT veröffentlichen
                publisher.PublishHeartbeat(topic, Herzschlag);

                // Warten für 1 Sekunde
                await Task.Delay(1000);
            }
            //publisher.Disconnect();
        }
    }
}
