﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using Database;
using MediTrack.Model.RemoteModel;
using Npgsql;

// hier ist noch der Rückgabe Typ offen !
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
                Console.WriteLine("Error when connecting to the database:  " + ex.Message);
                return null;
            }
        }

        // SQL Call for the Selection Window

       public static void DataBaseEntireEntryCall()
        {
            var DataBaseConnector = DataBaseConnectionCall();
            string SelectEntireTable = "SELECT pid FROM public.patients";
            
            using (NpgsqlCommand SelectAllTableEntries = new NpgsqlCommand(SelectEntireTable, DataBaseConnector))
            {
                //SelectAllTableEntries.Parameters.AddWithValue();

                NpgsqlDataReader EntryReader = SelectAllTableEntries.ExecuteReader();

                while (EntryReader.Read())
                {
                    int pid = EntryReader.GetInt32(0);
                    DataBaseEntries.Add(pid);
                }

                DataBaseConnector.Close();
            }



            //MediTrack.Model.RemoteModel.Patient PatientSingleEntry = new RemoteModel.Patient
            //{

            //    PatientNumber = EntryReader.GetInt32(0),
            //    LastName = EntryReader.GetString(1),
            //    FirstName = EntryReader.GetString(2),
            //};

            //DataBaseEntries.Add(PatientSingleEntry);
        }



        public static int? CallMonitorIDtoPatientID(object MonitorIDSearchKey)
        {
            if (MonitorIDSearchKey == null)
            {
                return null;
            }

            var DataBaseConnector = DataBaseConnectionCall();
            string SelectString = "SELECT pid FROM public.belegung WHERE moid = @MonitorIDSearchKey";
            Console.WriteLine(SelectString);

            using (NpgsqlCommand SelectPatientIDThroughMonitorID = new NpgsqlCommand(SelectString, DataBaseConnector))
            {
                SelectPatientIDThroughMonitorID.Parameters.AddWithValue("MonitorIDSearchKey", MonitorIDSearchKey);

                NpgsqlDataReader PIDSearcher = SelectPatientIDThroughMonitorID.ExecuteReader();

                if (PIDSearcher.Read())
                {
                    Console.WriteLine("PidNummer " + PIDSearcher["pid"]);
                    return (int?)PIDSearcher["pid"];
                }
                else
                {
                    return null;
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

                Console.WriteLine($"Name: {sresult[0]}, Vorname: {sresult[1]}, Raumnummer: {sresult[2]}, Bettnummer: {sresult[3]}");
                DataBaseConnector.Close();
                return sresult;
            }
            else
            {
                Console.WriteLine($"No DataBase Entry found for PID {patientIdentifier}");
                DataBaseConnector.Close();
                return null;
            }
        }
    }
}



    
    


