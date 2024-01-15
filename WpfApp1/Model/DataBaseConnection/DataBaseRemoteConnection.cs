using System;
using System.Collections.Generic;
using Npgsql;


/*
 * DataBaseRemoteConnection Class in MediTrack.Model.DataBaseModelConnection Namespace
 * 
 * Overview:
 * The DataBaseRemoteConnection class is a static class that manages the database connections and data retrieval 
 * for the MediTrack system. It utilizes Npgsql to connect to a PostgreSQL database, performing various queries 
 * and operations to support the application's data management requirements.
 * 
 * Key Functionalities:
 * - DataBaseConnectionCall(): Establishes a connection to the database using Npgsql. Handles any exceptions 
 *   during connection and returns either a valid connection object or null.
 * - DataBaseEntireEntryCall(): Executes a SQL query to fetch all patient IDs from the 'patients' table, 
 *   storing them in a public list. This method is essential for retrieving a complete list of patient identifiers.
 * - CallMonitorIDtoPatientID(object MonitorIDSearchKey): Takes a monitor ID and retrieves the corresponding 
 *   patient ID from the 'belegung' table. Useful for linking monitor data to specific patients.
 * - CallForPatientThroughID(object patientIdentifier): Retrieves detailed patient information (name, room 
 *   number, bed number) based on the patient ID. This method is critical for obtaining patient-specific 
 *   details for health monitoring and management.
 * 
 * Usage:
 * These methods are utilized within the MediTrack system to interact with the database for various purposes, 
 * such as obtaining patient details, monitoring IDs, and managing health data. The class serves as an 
 * intermediary for all database interactions within the application.
 * 
 * Example:
 * To retrieve patient data for a specific monitor ID, use `CallMonitorIDtoPatientID(monitorID)` method, and 
 * to fetch detailed information for a patient, use `CallForPatientThroughID(patientID)`.
 
 */



namespace MediTrack.Model.DataBaseModelConnection
{
    public static class DataBaseRemoteConnection
    {
        private static string ConnectionDatabaseInformation = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";
        public static List<int> DataBaseEntries = new List<int>();

        private static NpgsqlConnection DataBaseConnectionCall()
        {
            try
            {
                var ConnectionCallToDataBase = new NpgsqlConnection(ConnectionDatabaseInformation);
                ConnectionCallToDataBase.Open();
                return ConnectionCallToDataBase;
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error when connecting to the database:  " + ex.Message);
                return null;
            }
        }

       public static void DataBaseEntireEntryCall()
        {
            var DataBaseConnector = DataBaseConnectionCall();
            string SelectEntireTable = "SELECT pid FROM public.patients";
            
            using (NpgsqlCommand SelectAllTableEntries = new NpgsqlCommand(SelectEntireTable, DataBaseConnector))
            {
                NpgsqlDataReader EntryReader = SelectAllTableEntries.ExecuteReader();

                while (EntryReader.Read())
                {
                    int pid = EntryReader.GetInt32(0);
                    DataBaseEntries.Add(pid);
                }

                DataBaseConnector.Close();
            }
        }



        public static int? CallMonitorIDtoPatientID(object MonitorIDSearchKey)
        {
            if (MonitorIDSearchKey == null)
            {
                return null;
            }

            var DataBaseConnector = DataBaseConnectionCall();
            string SelectString = "SELECT pid FROM public.belegung WHERE moid = @MonitorIDSearchKey";

            using (NpgsqlCommand SelectPatientIDThroughMonitorID = new NpgsqlCommand(SelectString, DataBaseConnector))
            {
                try
                {
                    SelectPatientIDThroughMonitorID.Parameters.AddWithValue("MonitorIDSearchKey", MonitorIDSearchKey);
                    NpgsqlDataReader PIDSearcher = SelectPatientIDThroughMonitorID.ExecuteReader();

                    if (PIDSearcher.Read())
                    {

                        return (int?)PIDSearcher["pid"];
                    }

                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    throw new ArgumentException("Connection to Database failed");
                }
            }
        }

        public static string[] CallForPatientThroughID(object patientIdentifier)
        {
            if (patientIdentifier == null)
            {
                return null;
            }
            var DataBaseConnector = DataBaseConnectionCall();
            string SelectPatientThroughPIDString = "SELECT Name, Vorname, Rn, Bn FROM public.patients WHERE pid = @PatientID";
            NpgsqlCommand SelectPatientThroughPIDCommand = new NpgsqlCommand(SelectPatientThroughPIDString, DataBaseConnector);
            SelectPatientThroughPIDCommand.Parameters.AddWithValue("@PatientID", patientIdentifier);
            NpgsqlDataReader PIDReader = SelectPatientThroughPIDCommand.ExecuteReader();

            if (PIDReader.Read())
            {
                string[] sresult = new string[4];
                sresult[0] = PIDReader["Name"].ToString();
                sresult[1] = PIDReader["Vorname"].ToString();
                sresult[2] = PIDReader["Rn"].ToString();
                sresult[3] = PIDReader["Bn"].ToString();

                DataBaseConnector.Close();
                return sresult;
            }
            else
            {
                DataBaseConnector.Close();
                return null;
            }
        }
    }
}



    
    


