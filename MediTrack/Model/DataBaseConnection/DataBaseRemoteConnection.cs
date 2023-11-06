using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Database;
using Npgsql;


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

        void DataBaseEntryCall(List<MediTrack.Model.RemoteModel.Patient> DataBaseEntries)
        {
            var DataBaseConnector = DataBaseConnectionCall();

            //NpgsqlCommand SelectCountCommand = new NpgsqlCommand("SELECT COUNT(*) FROM patients ", DataBaseConnector);
            NpgsqlCommand SelectEntriesCommand = new NpgsqlCommand("SELECT * FROM  ", DataBaseConnector);
            //Int64 EntryCount = (Int64)SelectCountCommand.ExecuteScalar();

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


        void CallForPatientID()
        {
            var DataBaseConnector = DataBaseConnectionCall();


            DataBaseConnector.Close();
        }



    }

} 



    
    


