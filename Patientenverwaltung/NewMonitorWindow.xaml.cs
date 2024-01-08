using Npgsql;
using System;
using System.Windows;

namespace Patientenverwaltung
{
    public class Monitor
    {
        public string Manufacturer { get; set; }
        public string Serialnumber { get; set; }
    }

    public partial class NewMonitorWindow : Window
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";

        public NewMonitorWindow()
        {
            InitializeComponent();
        }

        private void MonitorAnlegen_Click(object sender, RoutedEventArgs e)
        {
            string manu = cmbManu.Text;
            string serial = txtSeriennummer.Text;

            if (IsValidInput(serial)) {
                try
                {
                    // Verbindung zur Datenbank öffnen
                    using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                    {
                        connection.Open();

                        // Überprüfen, ob die Serialnumber bereits existiert
                        string checkQuery = "SELECT COUNT(*) FROM monitors WHERE sn = @Serial";

                        using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@Serial", serial);

                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("Die Serialnummer existiert bereits in der Datenbank.");
                                return; // Beenden, wenn die Serialnummer bereits vorhanden ist
                            }
                        }

                        // Serialnumber ist eindeutig, daher kann der Monitor hinzugefügt werden
                        string insertQuery = "INSERT INTO monitors (mf, sn) VALUES (@Manu, @Serial)";

                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                        {
                            // Parameter setzen, um SQL Injection zu vermeiden
                            command.Parameters.AddWithValue("@Manu", manu);
                            command.Parameters.AddWithValue("@Serial", "S/N" + serial); // Concatenate "S/N" before the serial number
                            // Ausführen der SQL-Abfrage
                            command.ExecuteNonQuery();

                            MessageBox.Show("Monitor wurde erfolgreich angelegt.");
                            // Zurücksetzen der Auswahl in den ComboBoxen
                            cmbManu.SelectedItem = null;
                            txtSeriennummer.Text = null;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Einfügen der Daten: {ex.Message}");
                }
            }
        }
        private bool IsValidInput(string serial)
        {
            if ( serial.Length < 3)
            {
                MessageBox.Show("Seriennummer muss mindestens drei Zeichen enthalten.");
                return false;
            }
            // Hier können Sie zusätzliche Validierungen hinzufügen
            return !string.IsNullOrEmpty(serial);                   
        }


        private void Abbruch_Click(object sender, RoutedEventArgs e)
        {
            // Anwendung schließen
            this.Close();
        }
    }
}
