using System;
using System.Collections.Generic;


/*
 * PatientDictionary Class in MediTrack.Model.RemoteModel Namespace
 * 
 * Overview:
 * The PatientDictionary class maps the Monitor IDs or Patient IDs with Patient instances in the MediTrack system. It serves as a lookup mechanism for quickly retrieving the associated Patient instance based on a provided Monitor ID.
 * 
 * Functionality:
 * - DictionaryCaller(object MonitorIDKey): Retrieves the Patient instance associated with a given Monitor ID or Patient ID. If the association is not found, it returns null, which can trigger a subsequent database query to bind the Patient instance.
 * - DictionaryRemover(object MonitorIDKey): Removes the association betwen a Monitor ID or Patient ID and the corresponding Patient instance from the dictionary.
 * - DictionaryInput(object MonitorIDKey, Patient PatientIDKey): Adds a new association between a monitor ID or Patient ID and the corresponding Patient instance in the dictionary. If an association already exists, it handles the exception gracefully.
 * - DictionaryContainer(object MonitorIDKey): Checks if the dictionary contains an association for a given Monitor ID or Patient ID.
 * 
 * Usage:
 * - Use `PatientDictionary.DictionaryCaller(MonitorIDKey)` to retrieve a Patient instance associated with a Monitor ID or Patient ID.
 * - Use `PatientDictionary.DictionaryRemver(MonitorIDKey)` to remove an association from the dictionary.
 * - Use `PatientDictionary.DictionaryInput(MonitorIDKey, PatientIDKey)` to add or update an association in the dictionary.
 * - Use `PatientDictionary.DictionaryContainer(MonitorIDKey)` to check if the dictionary contains an association for a given Monitor ID or Patient ID.
 * 
 */

namespace MediTrack.Model.RemoteModel
{
    public static class PatientDictionary
    {
        static Dictionary<object, Patient> _monitorIdToPatientIdDictionary = new Dictionary<object, Patient>();

        public static Patient DictionaryCaller(object monitorIdKey)
        {

            if (_monitorIdToPatientIdDictionary.ContainsKey(monitorIdKey))
            {
                Patient patientIdKey = _monitorIdToPatientIdDictionary[monitorIdKey];
                return patientIdKey;
            }

            else
            {
                return null;
            }

        }

        public static void DictionaryRemover(object monitorIdKey)
        {
            try
            {
                _monitorIdToPatientIdDictionary.Remove(monitorIdKey);
            }
            catch
            {
            }
        }

        public static void DictionaryInput(object monitorIdKey, Patient patientIdKey)
        {
            try
            {
                _monitorIdToPatientIdDictionary.Add(monitorIdKey, patientIdKey);
            }
            catch (ArgumentException)
            {

            }
        }

        public static bool DictionaryContainer(object monitorIdKey)
        {
            if (_monitorIdToPatientIdDictionary.ContainsKey(monitorIdKey))
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
