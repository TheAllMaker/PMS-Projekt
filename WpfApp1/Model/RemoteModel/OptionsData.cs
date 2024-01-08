using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MediTrack.Model.RemoteModel
{
    public static class OptionsData
    {
        public static HashSet<object> Options = new HashSet<object>();
       


        public static void OptionsInput(int UID)
        {
                OptionsData.Options.Add(UID);          
        }

        public static void OptionsPop(int UID)
        {
            OptionsData.Options.Remove(UID);
        }










    }
}
