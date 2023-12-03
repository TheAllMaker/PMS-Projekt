using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MediTrack.Model.RemoteModel
{
    public static class HashSet
    {
     private static HashSet<int> MonitorHashSet = new HashSet<int>();

        public static bool InsertMonitorKeyToHash(int MonitorKey)
        {
         return MonitorHashSet.Add(MonitorKey);
        }

        public static bool RemoveMonitorKeyToHash(int MonitorKey)
        {
          return MonitorHashSet.Remove(MonitorKey);
        }

        public static bool ContainsHashMonitorKey (int MonitorKey)
        {
           return MonitorHashSet.Contains(MonitorKey);
        }
        // -> für Selcuk: Wie greife ich darauf zu ? -> in deinem File: 1) namespace Zugriff deklarieren + 2) Assembly reinhauen 
        // + 3) dann zugreifen mit classennamen.klassenemthode -> also hier HashSet.InsertMonitorKeyToHash(MonitorID);
        // Wie würde ich es machen:
        // if( HashSet.ContainsHashMonitorKey (int MonitorKey) == false)
        // { if(HashSet.InsertMonitorKeyToHash == true) Console.Writeline("Insert succesful blablalbab else dann ne Nachricht
        // NICHT VERGESSEN: die Zugriffe geben Bools zurück so dass du sehen kannst was schief gelaufen ist und dich daran orientieren kannst !
    }
}

//Button{

//     If( HashSet.ContainsHashMonitorKey( MonitorKey) == true)
//    {

//    }
//}
