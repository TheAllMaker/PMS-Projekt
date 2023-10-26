using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace Vitaldaten_Simulator
{
    internal class Mqtt
    {
        private MqttClient client;

        public Mqtt(string brokerAddress, int brokerPort, string clientId)
        {
            // Initialisierung des MQTT-Clients
            client = new MqttClient(brokerAddress);
            client.Connect(clientId);
        }

        public void PublishMessage(string topic, string message)
        {
            // Nachricht an das angegebene MQTT-Thema senden
            client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }

        public void Disconnect()
        {
            // MQTT-Verbindung trennen
            client.Disconnect();
        }
    }
}
