using System.Collections.Generic;


/*
 *
 * Overview:
 * The ActiveMonitorIDManager class is a static utility class designed to manage a collection of active monitor IDs received in the MQTT queue.
 * It allows for the insertion, deactivation, and checking of monitor IDs to determine if a particular monitor from the Vitaldatasimulator is currently active and sending data.
 *
 * Functionality:
 * - MonitorHashSet: A HashSet<int> that stores active monitor IDs.
 * - InsertActiveMonitor(int MonitorKey): Inserts a monitor ID into the collection of active monitors. Returns true if the insertion is successful, indicating that the monitor is now active.
 * - DeactivateMonitor(int MonitorKey): Removes a monitor ID from the collection of active monitors. Returns true if the removal is successful, indicating that the monitor is no longer active.
 * - IsThisAnActiveMonitor(int MonitorKey): Checks if a monitor ID is present in the collection of active monitors. Returns true if the monitor is active and sending data.
 *
 * Usage:
 * - Use `ActiveMonitorIDManager.InsertActiveMonitor(MonitorKey)` to add a monitor ID to the collection of active monitors when data from that monitor is received.
 * - Use `ActiveMonitorIDManager.DeactivateMonitor(MonitorKey)` to remove a monitor ID from the collection when the monitor is no longer sending data.
 * - Use `ActiveMonitorIDManager.IsThisAnActiveMonitor(MonitorKey)` to check if a monitor ID is active and sending data.
 */



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


