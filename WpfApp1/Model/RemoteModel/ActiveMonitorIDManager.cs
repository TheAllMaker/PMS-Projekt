﻿using System.Collections.Generic;


namespace MediTrack.Model.RemoteModel
{
    public static class ActiveMonitorIDManager
    {
     private static HashSet<int> MonitorHashSet = new HashSet<int>();
         
        public static bool InsertActiveMonitor (int MonitorKey)
        {
         return MonitorHashSet.Add(MonitorKey);
        }

        public static bool DeactivateMonitor (int MonitorKey)
        {
          return MonitorHashSet.Remove(MonitorKey);
        }

        public static bool IsThisAnActiveMonitor (int MonitorKey)
        {
           return MonitorHashSet.Contains(MonitorKey);
        }
    }
}


