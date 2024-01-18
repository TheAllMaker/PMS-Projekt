using System.Collections.Generic;


/*
 *
 * Overview:
 * The ActiveMonitorIDManager class is a static utility class designed to manage a collection of active monitor IDs received in the MQTT queue.
 * It allows for the insertion, deactivation, and checking of monitor IDs to determine if a particular monitor from the Vitaldatasimulator is currently active and sending data.
 *
 * Functionality:
 * - MonitorHashSet: A HashSet<int> that stores active monitor IDs.
 * - InsertActiveMonitor(int MonitorIdentifierKey): Inserts a monitor ID into the collection of active monitors. Returns true if the insertion is successful, indicating that the monitor is now active.
 * - DeactivateMonitor(int MonitorIdentifierKey): Removes a monitor ID from the collection of active monitors. Returns true if the removal is successful, indicating that the monitor is no longer active.
 * - IsThisAnActiveMonitor(int MonitorIdentifierKey): Checks if a monitor ID is present in the collection of active monitors. Returns true if the monitor is active and sending data.
 *
 * Usage:
 * - Use `ActiveMonitorIDManager.InsertActiveMonitor(MonitorIdentifierKey)` to add a monitor ID to the collection of active monitors when data from that monitor is received.
 * - Use `ActiveMonitorIDManager.DeactivateMonitor(MonitorIdentifierKey)` to remove a monitor ID from the collection when the monitor is no longer sending data.
 * - Use `ActiveMonitorIDManager.IsThisAnActiveMonitor(MonitorIdentifierKey)` to check if a monitor ID is active and sending data.
 */



namespace MediTrack.Model.RemoteModel
{
    public static class ActiveMonitorIDManager
    {
     private static HashSet<object> MonitorHashSet = new HashSet<object>();
         
        public static bool InsertActiveMonitor (object MonitorIdentifierKey)
        {
         return MonitorHashSet.Add(MonitorIdentifierKey);
        }

        public static bool DeactivateMonitor (object MonitorIdentifierKey)
        {
          return MonitorHashSet.Remove(MonitorIdentifierKey);
        }

        public static bool IsThisAnActiveMonitor (object MonitorIdentifierKey)
        {
           return MonitorHashSet.Contains(MonitorIdentifierKey);
        }
    }
}


