using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Database;
using Npgsql;

// hier ist noch der Rückgabe Typ offen !
namespace MediTrack.Model.DataBaseModelConnection
{
    class DataBaseRemoteConnection
    {

        private static string ConnectionDatabaseInformation = "Host=db.inftech.hs-mannheim.de;Username=n1921233;Password=123456;Database=n1921233_meditrack";
        public static List<MediTrack.Model.RemoteModel.Patient> DataBaseEntries = new List<MediTrack.Model.RemoteModel.Patient>();




      private NpgsqlConnection DataBaseConnectionCall()
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







        //NpgsqlCommand SelectCountCommand = new NpgsqlCommand("SELECT COUNT(*) FROM patients ", DataBaseConnector);
        //Int64 EntryCount = (Int64)SelectCountCommand.ExecuteScalar();


        void DataBaseEntireEntryCall(List<MediTrack.Model.RemoteModel.Patient> DataBaseEntries)
        {
            var DataBaseConnector = DataBaseConnectionCall();
            NpgsqlCommand SelectEntriesCommand = new NpgsqlCommand("SELECT * FROM ", DataBaseConnector);
            

            NpgsqlDataReader EntryReader = SelectEntriesCommand.ExecuteReader();
            while (EntryReader.Read())
            {
                MediTrack.Model.RemoteModel.Patient PatientSingleEntry = new RemoteModel.Patient
                {

                    PatientNumber = EntryReader.GetInt32(0),
                    LastName = EntryReader.GetString(1),
                    FirstName = EntryReader.GetString(2),
                };

                DataBaseEntries.Add(PatientSingleEntry);
            }
            DataBaseConnector.Close();


        }


        public int CallMonitorIDtoPatientID(string MonitorIDSearchKey)
        {
            var DataBaseConnector = DataBaseConnectionCall();
            NpgsqlCommand SelectPatientIDThroughMonitorID = new NpgsqlCommand($"SELECT * FROM public.belegung WHERE moid = {MonitorIDSearchKey}", DataBaseConnector);
            NpgsqlDataReader PIDSearcher = SelectPatientIDThroughMonitorID.ExecuteReader();

            if (PIDSearcher.Read())
            {
                return (int) PIDSearcher["pid"];
            }
            else
            {
                return 0;
            }
        }

        public string[] CallForPatientThroughID( int patientIdentifier)
        {
            
            var DataBaseConnector = DataBaseConnectionCall();
            string SelectPatientThroughPIDString = $"SELECT * FROM public.patients WHERE pid = {patientIdentifier}";
            NpgsqlCommand SelectPatientThroughPIDCommand = new NpgsqlCommand(SelectPatientThroughPIDString, DataBaseConnector);
            NpgsqlDataReader PIDReader = SelectPatientThroughPIDCommand.ExecuteReader();

             if (PIDReader.Read())
                {
                        string[] sresult = new string[2];
                        sresult[0] = PIDReader["Name"].ToString();
                        sresult[1] = PIDReader["Vorname"].ToString();
                
                    Console.WriteLine($"Name: {sresult[0]}, Vorname: {sresult[1]}");
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


        //public void AddPatientIntoDataBase(int pid, string FirstName, string LastName)
        //{
        //    var DataBaseConnector = DataBaseConnectionCall();
        //    string InsertPatientCommandString = $"INSERT INTO patients VALUES ({pid},'{FirstName}','{LastName}')";
        //    NpgsqlCommand InsertPatientCommand = new NpgsqlCommand("InsertPatientCommandString", DataBaseConnector);
        //}
    }

} 



    
    


