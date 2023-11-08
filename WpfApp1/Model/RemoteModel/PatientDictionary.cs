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

    public class PatientDictionary
    {


        static Dictionary<string, Patient> PatientInstanceDictionary = new Dictionary<string, Patient>();


        public Patient DictionaryCaller(string selectedPatientKey)
        {

            if (PatientInstanceDictionary.ContainsKey(selectedPatientKey))
            {
                Patient selectedPatient = PatientInstanceDictionary[selectedPatientKey];
                Console.WriteLine($"Ausgewählter Patient: {selectedPatient.PatientNumber}");
                return selectedPatient;
            }

            else
            {
                Console.WriteLine("Ausgewählter Patient nicht gefunden.");
                Console.WriteLine("DataBase wird nach Patienten mit der DatenMonitorID angefragt:");
                return null;
            }

        }

        public void DictionaryRemover(string selectedPatientKey)
        {
            PatientInstanceDictionary.Remove(selectedPatientKey);
        }

        public void DictionaryInput(string selectedPatientKey)
        {
            
            try
            {
                PatientInstanceDictionary.Add(selectedPatientKey, new Patient());
                Console.WriteLine($"Dictionary Entry successful for {selectedPatientKey}");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"An element with Key {selectedPatientKey} already exists.");
            }
        }


    }
}
