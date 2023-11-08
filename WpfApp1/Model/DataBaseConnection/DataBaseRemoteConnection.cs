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

        NpgsqlConnection DataBaseConnectionCall()
        {
            try
            {
                var ConnectionCallToDataBase = new NpgsqlConnection(ConnectionDatabaseInformation);
                ConnectionCallToDataBase.Open();
                return ConnectionCallToDataBase;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Verbinden mit der Database: " + ex.Message);
                return null;
            }
        }
        //NpgsqlCommand SelectCountCommand = new NpgsqlCommand("SELECT COUNT(*) FROM patients ", DataBaseConnector);
        //Int64 EntryCount = (Int64)SelectCountCommand.ExecuteScalar();
        void DataBaseEntryCall(List<MediTrack.Model.RemoteModel.Patient> DataBaseEntries)
        {
            var DataBaseConnector = DataBaseConnectionCall();

           
            NpgsqlCommand SelectEntriesCommand = new NpgsqlCommand("SELECT * FROM  ", DataBaseConnector);
            

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
        //object result = SelectPatientThroughPIDCommand.ExecuteScalar();
        // 
        void CallForPatientThroughID()
        {
            int test = 5;
            var DataBaseConnector = DataBaseConnectionCall();
            string SelectPatientThroughPIDString = $"SELECT * FROM patients WHERE PID = {test}";
            NpgsqlCommand SelectPatientThroughPIDCommand = new NpgsqlCommand("SelectPatientThroughPIDString", DataBaseConnector);


            NpgsqlDataReader PIDReader = SelectPatientThroughPIDCommand.ExecuteReader();

             if (PIDReader.Read())
                {
                    string name = PIDReader["Name"].ToString();
                    string vorname = PIDReader["Vorname"].ToString();
                    Console.WriteLine($"Name: {name}, Vorname: {vorname}");
                }

            else
            {
                Console.WriteLine($"No DataBase Entry found for PID {test}");
                Debug.WriteLine($"No DataBase Entry found for PID {test}");
            }


            DataBaseConnector.Close();
        }



    }

} 



    
    


