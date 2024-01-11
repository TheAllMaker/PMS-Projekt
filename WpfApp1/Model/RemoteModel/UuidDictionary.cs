using System;
using System.Collections.Generic;

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
