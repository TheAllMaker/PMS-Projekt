using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public static class MqttMessageQueue
    {
        private static Queue<string>  MQTTQueue = new Queue<string>();

        public static void Enqueue(string message)
        {
            MQTTQueue.Enqueue(message);
        }

        public static string Dequeue(string message)
        {
            if (MQTTQueue.Count > 0)
            {
                return MQTTQueue.Dequeue();
            }
            else
            {
                return null;
            }
            
        }

        public static int Count 
        {
            get { return MQTTQueue.Count; } 
        }

    }
}
