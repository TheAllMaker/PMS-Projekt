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

        public static string DictionaryCaller(int monitorIdentifierKey)
        {
            if(MonitorIDToName.ContainsKey(monitorIdentifierKey))
            {
                string PatientName = MonitorIDToName[monitorIdentifierKey];
                return PatientName;
            }

            else
            { 
                return null;
            }
        }

        public static void DictionaryRemover( int monitorIdentifierKey)
        {
            MonitorIDToName.Remove(monitorIdentifierKey);
        }

        public static void DictionaryInput(int monitorIdentifierKey, string patientName)
        {
            try
            {
                MonitorIDToName.Add(monitorIdentifierKey, patientName);
            }
            catch (ArgumentException)
            {

            }
        }

        public static bool DictionaryContainer(int monitorIdentifierKey)
        {
            if (MonitorIDToName.ContainsKey(monitorIdentifierKey))
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
