using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {

            var connString = "Host=db.inftech.hs-mannheim.de;Username=n1921233;Password=123456;Database=n1921233_meditrack";


            var conn = new NpgsqlConnection(connString);
            conn.Open();


            // Verbindung schließen
            conn.Close();


        }
        static void addPatient(MediTrack.Patient p) { p.Patient = p; }

    }
}
