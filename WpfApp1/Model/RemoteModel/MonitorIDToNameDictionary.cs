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

        public static string DictionaryCaller(int MonitorIdentifierKey)
        {
            if(MonitorIDToName.ContainsKey(MonitorIdentifierKey))
            {
                string PatientName = MonitorIDToName[MonitorIdentifierKey];
                return PatientName;
            }

            else
            { 
                return null;
            }
        }

        public static void DictionaryRemover( int MonitorIdentifierKey)
        {
            MonitorIDToName.Remove(MonitorIdentifierKey);
        }

        public static void DictionaryInput(int MonitorIdentifierKey, string PatientName)
        {
            try
            {
                MonitorIDToName.Add(MonitorIdentifierKey, PatientName);
            }
            catch (ArgumentException)
            {

            }
        }

        public static bool DictionaryContainer(int MonitorIdentifierKey)
        {
            if (MonitorIDToName.ContainsKey(MonitorIdentifierKey))
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
