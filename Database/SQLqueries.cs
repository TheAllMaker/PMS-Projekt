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
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";



        static void Main(string[] args)
        {



            var conn = new NpgsqlConnection(connString);
            conn.Open();


            Patient p1 = new Patient(firstName: "Hans", lastName: "Müller", birthday: new DateTime(1994, 06, 14));
            addPatient(p1);


        }
       public static void addPatient(Patient p) {

            var conn = new NpgsqlConnection(connString);
            try
            {
            conn.Open();
            var sql = "INSERT INTO patients(lastName, firstName, birthday) VALUES(@firstName,@lastName,@birthday) RETURNING patienten_id";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("firstName", p.firstName);
                cmd.Parameters.AddWithValue("lastName", p.lastName);
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
