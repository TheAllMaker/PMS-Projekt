using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public class DataControl
    {

        public static bool IsUUIDTheSame(Patient Instanz, object[] mqttMessageArray)
        {
            Patient existingPatient = PatientDictionary.DictionaryCaller(mqttMessageArray[0]);
            object comparevalue = UuidDictionary.UUIDDictionaryCaller(mqttMessageArray[0]);

            string value1 = mqttMessageArray[7]?.ToString();
            string value2 = comparevalue?.ToString();

            if (string.Equals(value1,value2))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsNewMonitorOnline(object[] mqttMessageArray)
        {
            if ((mqttMessageArray[8] is int value && value == 0))
            {
                return false;
            }
            else 
            {
                return true;
            }

        }









    }
}
