using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;


/*
 * MqttHandler Class in MediTrack.Model.RemoteModel Namespace
 * 
 * Overview:
 * The MqttHandler class is responsible for managing MQTT (Message Queuing Telemetry Transport) communications 
 * within the MediTrack system. It encapsulates the functionality to connect to an MQTT server, subscribe to 
 * topics, handle incoming messages, and disconnect from the server.
 * 
 * Key Functionalities:
 * - ConnectToServer(): Establishes a connection to the MQTT server using TLS encryption. It also sets up 
 *   event handlers for subscribed messages and messages received.
 * - SubScribeToTopic(): Subscribes to a specific MQTT topic to listen for incoming messages. It also logs 
 *   the subscription details for debugging purposes.
 * - Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e): An event handler that triggers 
 *   when a subscription is successfully made. Currently, this method is empty but can be modified for 
 *   specific actions upon successful subscription.
 * - Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e): An event handler that is 
 *   invoked when a new message is published to a subscribed topic. This method decodes the message and 
 *   enqueues it into the `MqttMessageQueue` for further processing.
 * - Disconnect(): Safely disconnects from the MQTT server.
 * 
 * Usage:
 * This class is used to handle real-time data communication with the MQTT server. It is essential for 
 * receiving patient health monitoring data and other relevant information in the MediTrack system.
 * 
 * Example:
 * To start receiving messages, first establish a connection using `ConnectToServer()`, then subscribe to 
 * desired topics using `SubScribeToTopic()`. Messages received on these topics will be automatically handled 
 * by `Client_MqttMsgPublishReceived()`.
 */

namespace MediTrack.Model.RemoteModel
{
    public class MqttHandler
    {
        private MqttClient client;

        public void ConnectToServer()
        {
            const string user = "23pms01";
            const string pwd = "c3c242ff";
            const string serverName = "mqtt.inftech.hs-mannheim.de";
            const int portIdentifier = 8883;

            client = new MqttClient(serverName, portIdentifier, true, null, null, MqttSslProtocols.TLSv1_2);

            client.Connect(Guid.NewGuid().ToString(), user, pwd);
            client.MqttMsgSubscribed += OnMqttMsgSubscribed;
            client.MqttMsgPublishReceived += OnMqttMsgPublishReceived;
        }

        public void SubScribeToTopic()
        {
            const string topic = "23pms01/test";
            var msgId = client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            //Debug.WriteLine($"{StringContainer.MQTTSubScribeTopic}{topic}");
            //Debug.WriteLine($"{StringContainer.MQTTSubScribeSucc}{msgId}");
        }

        private void OnMqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            //Debug.WriteLine("MediTrack Subscribed for id = " + e.MessageId);
        }

        private void OnMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var topic = e.Topic;
            var message = Encoding.UTF8.GetString(e.Message);
            //Console.WriteLine($"{topic}");
            //Console.WriteLine($"{message}");
            MqttMessageQueue.Enqueue(message);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }
    }
}
