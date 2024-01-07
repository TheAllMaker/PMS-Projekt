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
        public static List<object> Options = new List<object>
        {
            "DauerStänder"
        };


        public static void OptionsInput(int UID)
        {
            
 
                OptionsData.Options.Add(UID); 
            
        }










    }
}
