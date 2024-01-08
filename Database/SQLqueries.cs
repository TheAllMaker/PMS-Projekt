using System;
using Npgsql;
using System.Windows;

namespace Database
{
    public class SQLqueries
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";

        static void Main(string[] args)
        {
            DeleteTable();
            CreateTable();

        }

        public static void CreateTable()
        {
            var conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();

                var sql = $@"
                    CREATE TABLE IF NOT EXISTS patients (
                        pid SERIAL PRIMARY KEY,
                        name VARCHAR(80) NOT NULL,
                        vorname VARCHAR(80) NOT NULL,
                        sex CHAR(6),
                        bd DATE,
                        rn INT,
                        bn INT
                    );

                    CREATE TABLE IF NOT EXISTS monitors (
                        moid SERIAL PRIMARY KEY,
                        mf VARCHAR(80) NOT NULL,
                        sn VARCHAR(80) NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS belegung (
                        moid INT REFERENCES monitors(moid),
                        pid INT REFERENCES patients(pid)
                    );";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Alle Tabellen erstellt.");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Fehler beim Erstellen der Tabellen: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteTable()
        {
            var conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();

                var sql = $@"DROP TABLE patients, monitors, belegung";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine($"Alle Tabellen gelöscht.");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Fehler beim Löschen der Tabellen: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}





