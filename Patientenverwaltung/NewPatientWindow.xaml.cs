using System;
using System.Windows;
using Npgsql;

namespace Patientenverwaltung
{
    public class Patient
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Geschlecht { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public int Room { get; set; }
        public int Bed { get; set; }
    }

    public partial class NewPatientWindow : Window
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";

        public NewPatientWindow()
        {
            InitializeComponent();
            dpGeburtstag.Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-US");
        }



        private void PatientAnlegen_Click(object sender, RoutedEventArgs e)
        {
            string vorname = txtVorname.Text;
            string nachname = txtNachname.Text;
            string sex = cmbSex.Text;

            DateTime geburtsdatum = dpGeburtstag.SelectedDate ?? DateTime.MinValue;

            // Überprüfen, ob das Room-Feld eine gültige Ganzzahl ist
            if (int.TryParse(txtRoom.Text, out int room) && int.TryParse(txtBed.Text, out int bed))
            {
                // Überprüfen, ob das Bed-Feld eine gültige Ganzzahl ist
                
                    // Überprüfen der Eingabe
                    if (IsValidInput(vorname, nachname, sex, geburtsdatum, room, bed))
                    {
                        // Verbindungszeichenfolge zur Datenbank
                        string connectionString = connString;

                    
                        try
                        {
                            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                            {
                                connection.Open();

                                // SQL-Abfrage zum Einfügen von Daten
                                string insertQuery = "INSERT INTO patients (name, vorname, sex, bd, rn, bn) VALUES (@Nachname, @Vorname, @Sex, @Birthdate, @Room, @Bed)";

                                using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                                {
                                    // Parameter setzen, um SQL Injection zu vermeiden
                                    command.Parameters.AddWithValue("@Vorname", vorname);
                                    command.Parameters.AddWithValue("@Nachname", nachname);
                                    command.Parameters.AddWithValue("@Sex", sex);
                                    command.Parameters.AddWithValue("@Birthdate", geburtsdatum);
                                    command.Parameters.AddWithValue("@Room", room);
                                    command.Parameters.AddWithValue("@Bed", bed);
                                    // Ausführen der SQL-Abfrage
                                    command.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Daten erfolgreich in die Datenbank eingefügt.");
                            cmbSex.SelectedItem = null;
                            txtVorname.Text = null;
                            txtNachname.Text = null;
                            txtRoom.Text = null;
                            txtBed.Text = null;
                            dpGeburtstag.SelectedDate = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Einfügen der Daten: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ungültige Eingabe. Stellen Sie sicher, dass alle Felder ausgefüllt sind.");
                    }
                }
            else
            {
                MessageBox.Show("Ungültige Eingabe. Bitte geben Sie eine gültige Ganzzahl ein für Bed und Room ein.");
            }

            
        }

        private bool IsValidInput(string vorname, string nachname, string sex, DateTime geburtsdatum, int room, int bed)
        {
            if (vorname.Length < 3 || nachname.Length < 3)
            {
                MessageBox.Show("Name und Vorname müssen mindestens drei Buchstaben enthalten.");
                return false;
            }
            // Hier können Sie zusätzliche Validierungen hinzufügen
            return !string.IsNullOrEmpty(vorname) &&
                   !string.IsNullOrEmpty(nachname) &&
                   !string.IsNullOrEmpty(sex) &&
                   geburtsdatum != DateTime.MinValue;
        }

        private void Abbruch_Click(object sender, RoutedEventArgs e)
        {
            // Anwendung schließen 
            this.Close();
        }
    }
}
