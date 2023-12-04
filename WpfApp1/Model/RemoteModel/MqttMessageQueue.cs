using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
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

        public static int?[] Dequeue()
        {
            if (MQTTQueue.Count > 0)
            {
                string message = MQTTQueue.Dequeue();
                dynamic parsedObject = JsonConvert.DeserializeObject(message);

                if (parsedObject != null)
                {
                    int?[] array =
                    {
                GetValue(parsedObject, "MonitorID"),
                GetValue(parsedObject, "HeartRate"),
                GetValue(parsedObject, "RespirationRate"),
                GetValue(parsedObject, "OxygenLevel"),
                GetValue(parsedObject, "BloodPressureSystolic"),
                GetValue(parsedObject, "BloodPressureDiastolic"),
                GetValue(parsedObject, "Temperature")
            };
                    return array;
                }
            }
            return null;
        }

        private static int? GetValue(dynamic obj, string propertyName)
        {
            if (obj == null || obj.GetType().GetProperty(propertyName) == null)
            {
                return null;
            }

            int result;
            return int.TryParse(obj[propertyName].ToString(), out result) ? result : (int?)null;
        }


        public static int Count 
        {
            get { return MQTTQueue.Count; } 
        }

    }
}
