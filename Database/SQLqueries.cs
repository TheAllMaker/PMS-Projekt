using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Database
{
    public class SQLqueries
    {
        private static String connString = "Host=db.inftech.hs-mannheim.de;Username=n1921233;Password=123456;Database=n1921233_meditrack";



        static void Main(string[] args)
        {



            var conn = new NpgsqlConnection(connString);

            Patient p = new Patient(prename: "Hans", surname: "Müller", birthday: new DateTime(1994, 06, 14));
            addPatient(p);


        }
       public static void addPatient(Patient p) {

            var conn = new NpgsqlConnection(connString);
            try
            {
            conn.Open();
            var sql = "INSERT INTO patients(prename, surname, birthday) VALUES(@prename,@surname,@birthday) RETURNING patienten_id";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("prename", p.prename);
                cmd.Parameters.AddWithValue("surname", p.surname);
                cmd.Parameters.AddWithValue("birthday", p.birthday);
                cmd.Prepare();
                int id = (int)cmd.ExecuteScalar();
                p.id = id;
                Console.WriteLine(p.id);
            }
            Console.WriteLine("row inserted");
        }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Fehler beim Einfügen des Patienten: " + ex.Message);
            }
            conn.Close();
            }

    }
}
