using System;
using System.Collections.Generic;



/*
 *
 * Overview:
 * The UuidDictionary class manages a dictionary that maps the Monitor Identifiers with UUIDs (Universally Unique Identifiers) in the MediTrack system. It provides a mechanism to quickly retrieve the UUID associated with a given Monitor Identifier.
 *
 * Functionality:
 * - UUIDDictionaryCaller(object MonitorIdentifierKey): Retrieves the UUID associated with a given Monitor Identifier. If the association is not found, it returns null.
 * - DictionaryRemover(object MonitorIdentifierKey): Removes the association between a Monitor Identifier and its corresponding UUID from the dictionary.
 * - DictionaryInput(object MonitorIdentifierKey, object UUIDKey): Adds a new association between a Monitor Identifier and its corresponding UUID in the dictionary. It gracefully handles exceptions that may occur when attempting to add duplicate associations.
 * - DictionaryContainer(object MonitorIdentifierKey): Checks if the dictionary contains an association for a given Monitor Identifier.
 *
 * Usage:
 * - Use `UuidDictionary.UUIDDictionaryCaller(MonitorIdentifierKey)` to retrieve the UUID associated with a Monitor Identifier.
 * - Use `UuidDictionary.DictionaryRemover(MonitorIdentifierKey)` to remove an association from the dictionary.
 * - Use `UuidDictionary.DictionaryInput(MonitorIdentifierKey, UUIDKey)` to add or update an association in the dictionary.
 * - Use `UuidDictionary.DictionaryContainer(MonitorIdentifierKey)` to check if the dictionary contains an association for a given Monitor Identifier.
 */



namespace MediTrack.Model.RemoteModel
{
    public static class UuidDictionary
    {
       private static Dictionary<object, object> MonitorIdentifierTOUUID = new Dictionary<object, object>();

        public static object UUIDDictionaryCaller(object MonitorIdentifierKey)
        {
            if(MonitorIdentifierTOUUID.ContainsKey(MonitorIdentifierKey))
            {
                object UUID = MonitorIdentifierTOUUID[MonitorIdentifierKey];
                //Console.WriteLine(StringContainer.UUIDDictionarySuc + UUID);
                string uuidString = UUID.ToString();
                return uuidString;
            }
            
            else
            {
                //Console.WriteLine(StringContainer.UUIDDictionaryFai);
                return null; 
            }

        }

        public static void DictionaryRemover(object MonitorIdentifierKey) 
        {
            try
            {
                MonitorIdentifierTOUUID.Remove(MonitorIdentifierKey);
                //Console.WriteLine(StringContainer.UUIDDictionaryRemovedSuc + MonitorIdentifierKey);
            }
            
            catch (ArgumentException)
            {
                //Console.WriteLine(StringContainer.UUIDDictionaryRemovedFai + MonitorIdentifierKey);
            }

        }

        public static void DictionaryInput(object MonitorIdentifierKey, object UUIDKey)
        {
            try
            {
                MonitorIdentifierTOUUID.Add(MonitorIdentifierKey, UUIDKey);
                //Console.WriteLine(StringContainer.UUIDDictionaryInsertSuc);
            }

            catch (ArgumentException)
            {
                //Console.WriteLine(StringContainer.UUIDDictionaryInsertFai);
            }
        }

        public static bool DictionaryContainer(object MonitorIdentifierKey)
        {
            if (MonitorIdentifierKey == null) return false;
            if (MonitorIdentifierTOUUID.ContainsKey(MonitorIdentifierKey))
            {
                //Console.WriteLine(StringContainer.UUIDDictionaryContainSuc);
                return true;
            }

            else
            {
                //Console.WriteLine(StringContainer.UUIDDictionaryContainFai);
                return false;
            }

        }

    }
}
