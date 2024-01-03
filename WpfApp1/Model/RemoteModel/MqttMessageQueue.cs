using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Database;
using Newtonsoft.Json;


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
