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
                dynamic parsedObject = JsonConvert.DeserializeObject(MQTTQueue.Dequeue());
                Console.WriteLine(parsedObject.ToString());
                int?[] array =
            {
            GetIntValue(parsedObject.MonitorID),
            GetIntValue(parsedObject.HeartRate),
            GetIntValue(parsedObject.RespirationRate),
            GetIntValue(parsedObject.OxygenLevel),
            GetIntValue(parsedObject.BloodPressureSystolic),
            GetIntValue(parsedObject.BloodPressureDiastolic),
            GetIntValue(parsedObject.Temperature)
            };
                return array;
            }
            else 
            {
                return null;
            }
            
        }

        private static int? GetIntValue(dynamic value)
        {
            if (value == null)
            {
                return null;
            }

            int result;
            return int.TryParse(value.ToString(), out result) ? result : (int?)null;
        }


        public static int Count 
        {
            get { return MQTTQueue.Count; } 
        }

    }
}
