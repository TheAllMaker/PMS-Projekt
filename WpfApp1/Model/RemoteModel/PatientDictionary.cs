using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediTrack.Model.RemoteModel
{

    // Das Dictionary nimmt von MQTT die MonitorId bzw. PatientenId auf, und lässt durch eine Abfrage die PatientenInstanz ermittlen.
    // Ist die Abfrage erfolglos, wird null zurückgegeben um im Weiteren eine Databank Abfrage zu starten welche dann zum Binden der 
    // PatientenInstanz genutzt wird
    // Welcher Patient gehört zu Welche

    public static class PatientDictionary
    {


        static Dictionary<int?, int?> MonitorIDToPatientIDDictionary = new Dictionary<int?, int?>();


        public static int? DictionaryCaller(int? MonitorIDKey)
        {

            if (MonitorIDToPatientIDDictionary.ContainsKey(MonitorIDKey))
            {
                int? PatientIDKey = MonitorIDToPatientIDDictionary[MonitorIDKey];
                Console.WriteLine($"Ausgewählter Patient: {PatientIDKey}");
                return PatientIDKey;
            }

            else
            {
                Console.WriteLine("Ausgewählter Patient nicht gefunden.");
                Console.WriteLine("DataBase wird nach Patienten mit der DatenMonitorID angefragt:");
                return 0;
            }

        }

        public static void DictionaryRemover(int? MonitorIDKey)
        {
            MonitorIDToPatientIDDictionary.Remove(MonitorIDKey);
            Console.WriteLine($"Dictionary Key Entry {MonitorIDKey} deleted");
        }

        public static void DictionaryInput(int? MonitorIDKey, int? PatientIDKey)
        {
            
            try
            {
                MonitorIDToPatientIDDictionary.Add(MonitorIDKey, PatientIDKey);
                Console.WriteLine($"Dictionary Entry successful for {MonitorIDKey} with the patient id {PatientIDKey}");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"An element with Key {MonitorIDKey} already exists.");
            }
        }

        public static bool DictionaryContainer(int? MonitorIDKey)
        {
            if (MonitorIDKey == null) return false;
            if (MonitorIDToPatientIDDictionary.ContainsKey(MonitorIDKey))
            {
                Console.WriteLine("Selected patient found.");
                return true;
            }

            else
            {
                Console.WriteLine("Selected patient not found.");
                return false;
            }

        }
    }
}
