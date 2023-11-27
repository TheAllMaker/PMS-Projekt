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

    public class PatientDictionary
    {


        static Dictionary<string, string> MonitorIDToPatientIDDictionary = new Dictionary<string, string>();


        public string DictionaryCaller(string MonitorIDKeyString)
        {

            if (MonitorIDToPatientIDDictionary.ContainsKey(MonitorIDKeyString))
            {
                string PatientIDKeyString = MonitorIDToPatientIDDictionary[MonitorIDKeyString];
                Console.WriteLine($"Ausgewählter Patient: {PatientIDKeyString}");
                return PatientIDKeyString;
            }

            else
            {
                Console.WriteLine("Ausgewählter Patient nicht gefunden.");
                Console.WriteLine("DataBase wird nach Patienten mit der DatenMonitorID angefragt:");
                return null;
            }

        }

        public void DictionaryRemover(string MonitorIDKeyString)
        {
            MonitorIDToPatientIDDictionary.Remove(MonitorIDKeyString);
            Console.WriteLine($"Dictionary Key Entry {MonitorIDKeyString} deleted");
        }

        public void DictionaryInput(string MonitorIDKeyString, string PatientIDKeyString)
        {
            
            try
            {
                MonitorIDToPatientIDDictionary.Add(MonitorIDKeyString, PatientIDKeyString);
                Console.WriteLine($"Dictionary Entry successful for {MonitorIDKeyString} with the patient id {PatientIDKeyString}");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"An element with Key {MonitorIDKeyString} already exists.");
            }
        }


    }
}
