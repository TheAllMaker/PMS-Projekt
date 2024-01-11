using System;
using System.Collections.Generic;


namespace MediTrack.Model.RemoteModel
{

    // Das Dictionary nimmt von MQTT die MonitorId bzw. PatientenId auf, und lässt durch eine Abfrage die PatientenInstanz ermittlen.
    // Ist die Abfrage erfolglos, wird null zurückgegeben um im Weiteren eine Databank Abfrage zu starten welche dann zum Binden der 
    // PatientenInstanz genutzt wird
    // Welcher Patient gehört zu Welche

    public static class PatientDictionary
    {


        static Dictionary<object, Patient> MonitorIDToPatientIDDictionary = new Dictionary<object, Patient>();


        public static Patient DictionaryCaller(object MonitorIDKey)
        {

            if (MonitorIDToPatientIDDictionary.ContainsKey(MonitorIDKey))
            {
                Patient PatientIDKey = MonitorIDToPatientIDDictionary[MonitorIDKey];
                return PatientIDKey;
            }

            else
            {
                return null;
            }

        }

        public static void DictionaryRemover(object MonitorIDKey)
        {
            MonitorIDToPatientIDDictionary.Remove(MonitorIDKey);
        }

        public static void DictionaryInput(object MonitorIDKey, Patient PatientIDKey)
        {
            
            try
            {
                MonitorIDToPatientIDDictionary.Add(MonitorIDKey, PatientIDKey);
            }
            catch (ArgumentException)
            {

            }
        }

        public static bool DictionaryContainer(object MonitorIDKey)
        {
            if (MonitorIDKey == null) return false;
            if (MonitorIDToPatientIDDictionary.ContainsKey(MonitorIDKey))
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
