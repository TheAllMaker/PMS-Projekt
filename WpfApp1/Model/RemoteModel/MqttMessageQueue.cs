using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/*
 * MqttMessageQueue Class in MediTrack.Model.RemoteModel Namespace
 * 
 * Overview:
 * The MqttMessageQueue class is a static utility class designed to handle MQTT messages within the MediTrack system. 
 * It primarily functions as a message queue for MQTT messages, providing mechanisms for enqueuing and dequeuing 
 * these messages in a thread-safe manner. 
 * 
 * Usage:
 * - Enqueue(string message): Adds a new MQTT message to the queue. This method is used to insert messages 
 *   received from MQTT topics into the queue for subsequent processing.
 * - Dequeue(): Retrieves and removes the oldest message from the queue. It parses the JSON content of the 
 *   MQTT message, extracts vital health parameters, and returns them in an object array. This method is 
 *   typically called by consumers of MQTT data for processing.
 * - Count: Provides the current number of messages in the queue, useful for monitoring the queue's status.
 *
 * Details:
 * - The internal implementation uses a Queue <string> to store the messages.
 * - The Dequeue method is responsible for parsing the JSON message into a dynamic object, extracting relevant 
 *   health monitoring data (like HeartRate, RespirationRate, etc.), and converting them to their appropriate 
 *   data types (int, double, string) with necessary null checks and parsing.
 * - The class provides helper methods for data type conversions and rounding off double values.
 * 
 * Example:
 * To use the MqttMessageQueue, first enqueue messages using `MqttMessageQueue.Enqueue(message)`. Then, you can 
 * periodically dequeue messages using `MqttMessageQueue.Dequeue()` to process the latest health data.
 *
 */

namespace MediTrack.Model.RemoteModel
{
    public static class MqttMessageQueue
    {
        private static Queue<string>  MQTTQueue = new Queue<string>();

        public static void Enqueue(string message)
        {
            MQTTQueue.Enqueue(message);
        }

        public static object[] Dequeue()
        {
            if (MQTTQueue.Count > 0)
            {
            
                    dynamic parsedObject = JsonConvert.DeserializeObject(MQTTQueue.Dequeue());
                    Console.WriteLine(parsedObject.ToString());

                    object[] MQTTMessageContainer =
                    {
                    GetIntValue(parsedObject.MonitorID),
                    GetIntValue(parsedObject.HeartRate),
                    GetDoubleValue(parsedObject.RespirationRate),
                    GetDoubleValue(parsedObject.OxygenLevel),
                    GetIntValue(parsedObject.BloodPressureSystolic),
                    GetIntValue(parsedObject.BloodPressureDiastolic),
                    GetDoubleValue(parsedObject.Temperature),
                    GetUUIDValue(parsedObject.UUID),
                    GetIntValue(parsedObject.Alive)
                };

                    return MQTTMessageContainer;
            }

            else
            {
                return Array.Empty<object>();
            }

        }


        private static double? GetDoubleValue(object value)
        {

            if (value == null)
            {
                return null;
            }

            if (double.TryParse(value.ToString(), out double result))
            {

                result = Math.Round(result, 1);
                return result;
            }

            return null;
        }

        private static int? GetIntValue(object value)
        {

            if (value == null)
            {
                return null;
            }

            return int.TryParse(value.ToString(), out int result) ? (int?)result : null;
        }

        private static string GetUUIDValue(object value)
        {
            return value?.ToString();
        }

        public static int Count 
        {
            get 
            {
                return MQTTQueue.Count; 
            } 
        }

    }
}
