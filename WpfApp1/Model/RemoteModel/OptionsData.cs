using System;
using System.Collections.Generic;

/*
 * Overview:
 * The OptionsData class serves as a data buffer to hold information related to the RemotePatient PopUp ListBoxWindow. It is designed to store and manage a collection of unique identifiers (UIDs) representing options available for the RemotePatient PopUp ListBoxWindow. This class acts as a callable data buffer instance.
 * 
 * Functionality:
 * - Options: A HashSet<object> that stores unique identifiers (UIDs) representing available options.
 * - OptionsInput(int UID): Adds a UID to the collection of options in the OptionsData class.
 * - OptionsPop(int UID): Removes a UID from the collection of options in the OptionsData class.
 * 
 * Usage:
 * - Use `OptionsData.OptionsInput(UID)` to add a unique identifier (UID) to the collection of available options.
 * - Use `OptionsData.OptionsPop(UID)` to remove a UID from the collection of available options.
 */ 



namespace MediTrack.Model.RemoteModel
{
    public static class OptionsData
    {
        public static HashSet<object> Options = new HashSet<object>();
       
        public static void OptionsInput(object UID)
        {
            try
            {
                OptionsData.Options.Add(UID);
            }
            catch 
            {
                throw;
            }
                     
        }

        public static void OptionsPop(object UID)
        {
            try
            {
                OptionsData.Options.Remove(UID);
            }
            catch
            {
                throw;
            }
            
        }

    }
}
