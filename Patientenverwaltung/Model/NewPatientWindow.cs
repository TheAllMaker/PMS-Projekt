using System;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;

namespace Patientenverwaltung
{


    public partial class NewPatientWindow
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
            string room = txtRoom.Text;
            string bed = txtBed.Text;
            if (AreFieldsFilled(vorname, nachname, sex, geburtsdatum, room, bed))
            {
                // Überprüfen, ob das Room-Feld eine gültige Ganzzahl ist und ob das Bed-Feld eine gültige Ganzzahl ist
                if (IsValidInput(vorname, nachname, sex, geburtsdatum) && IsValidInput2(room, bed, out int roomValue, out int bedValue))
                {
                    

                    // Verbindungszeichenfolge zur Datenbank
                    string connectionString = connString;


                    try
                    {
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();

                            // SQL-Abfrage zum Einfügen von Daten
                            string insertQuery =
                                "INSERT INTO patients (name, vorname, sex, bd, rn, bn) VALUES (@Nachname, @Vorname, @Sex, @Birthdate, @Room, @Bed)";

                            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                            {
                                // Parameter setzen, um SQL Injection zu vermeiden
                                command.Parameters.AddWithValue("@Vorname", vorname);
                                command.Parameters.AddWithValue("@Nachname", nachname);
                                command.Parameters.AddWithValue("@Sex", sex);
                                command.Parameters.AddWithValue("@Birthdate", geburtsdatum);
                                command.Parameters.AddWithValue("@Room", roomValue);
                                command.Parameters.AddWithValue("@Bed", bedValue);
                                // Ausführen der SQL-Abfrage
                                command.ExecuteNonQuery();
                            }
                        }

                        // Show the status (checkmark) with fade-out animation
                        DisconnectStatus.Visibility = Visibility.Visible;
                        DoubleAnimation
                            animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.8)); // x seconds fade-out
                        DisconnectStatus.BeginAnimation(TextBlock.OpacityProperty, animation);
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

            }

        }

        private static bool AreFieldsFilled(string vorname, string nachname, string sex, DateTime geburtsdatum, string room, string bed)
        {
            if (string.IsNullOrEmpty(vorname) || string.IsNullOrEmpty(nachname) || string.IsNullOrEmpty(sex) ||
                geburtsdatum == DateTime.MinValue || string.IsNullOrEmpty(room) || string.IsNullOrEmpty(bed))
            {
                MessageBox.Show("Bitte füllen Sie alle erforderlichen Felder aus.");
                return false;
            }

            return true;
        }


        private bool IsValidInput(string vorname, string nachname, string sex, DateTime geburtsdatum)
        {
            if (vorname.Length < 3 || nachname.Length < 3)
            {
                MessageBox.Show("Name und Vorname müssen mindestens drei Buchstaben enthalten.");
                return false;
            }

            // Überprüfen, ob Vorname und Nachname nur Buchstaben enthalten
            if (!IsOnlyLetters(vorname) || !IsOnlyLetters(nachname))
            {
                MessageBox.Show("Name und Vorname dürfen nur Buchstaben enthalten.");
                return false;
            }

            // Hier können Sie zusätzliche Validierungen hinzufügen
            return !string.IsNullOrEmpty(vorname) &&
                   !string.IsNullOrEmpty(nachname) &&
                   !string.IsNullOrEmpty(sex) &&
                   geburtsdatum != DateTime.MinValue;
        }

        private static bool IsValidInput2(string room, string bed, out int roomValue, out int bedValue)
        {
            // Initialize out parameters
            roomValue = 0;
            bedValue = 0;

            // Überprüfen, ob room und bed positive Zahlen sind
            if (!int.TryParse(room, out roomValue) || !int.TryParse(bed, out bedValue) || roomValue <= 0 || bedValue <= 0)
            {
                MessageBox.Show("Room und Bed müssen positive Zahlen sein.");
                return false;
            }

            // Überprüfen, ob room und bed nicht größer als 4-stellig sind
            if (roomValue > 9999 || bedValue > 9999)
            {
                MessageBox.Show("Room und Bed dürfen nicht größer als 4-stellig sein.");
                return false;
            }

            // Hier können Sie zusätzliche Validierungen hinzufügen
            return true;
        }




        private bool IsOnlyLetters(string value)
        {
            return !string.IsNullOrEmpty(value) && value.All(char.IsLetter);
        }


        private void Abbruch_Click(object sender, RoutedEventArgs e)
        {
            // Anwendung schließen 
            this.Close();
        }
    }
}
