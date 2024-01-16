using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{
    public static class MonitorIDToNameDictionary
    {
        public static Dictionary<int,string> MonitorIDToName = new Dictionary<int,string>();

        public static string DictionaryCaller(int MonitorIDKey)
        {
            if(MonitorIDToName.ContainsKey(MonitorIDKey))
            {
                string PatientName = MonitorIDToName[MonitorIDKey];
                return PatientName;
            }

            else
            { 
                return null;
            }
        }

        public static void DictionaryRemover( int MonitorIDKey)
        {
            MonitorIDToName.Remove(MonitorIDKey);
        }

        public static void DictionaryInput(int MonitorIDKey, string PatientName)
        {
            try
            {
                MonitorIDToName.Add(MonitorIDKey, PatientName);
            }
            catch (ArgumentException)
            {

            }
        }

        public static bool DictionaryContainer(int MonitorIDKey)
        {
            if (MonitorIDToName.ContainsKey(MonitorIDKey))
            {
                return true;
            }

            else
            {
                return false;
            }


        }



    }
}
