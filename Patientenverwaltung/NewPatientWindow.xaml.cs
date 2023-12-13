using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patientenverwaltung
{
    /// <summary>
    /// Interaktionslogik für NewPatientWindow.xaml
    /// </summary>
    public partial class NewPatientWindow : Window
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";


        public NewPatientWindow()
        {
            InitializeComponent();
        }

        private void PatientAnlegen_Click(object sender, RoutedEventArgs e)
        {
            // Verbindungszeichenfolge zur Datenbank
            string connectionString = "DeineConnectionString";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Patientendaten aus den Textfeldern abrufen
                    string vorname = txtVorname.Text;
                    string nachname = txtNachname.Text;
                    DateTime geburtsdatum = dpGeburtstag.SelectedDate ?? DateTime.MinValue;

                    // SQL-Abfrage zum Einfügen von Daten
                    string insertQuery = "INSERT INTO Patienten (Vorname, Nachname, Geburtsdatum) VALUES (@Vorname, @Nachname, @Geburtsdatum)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Parameter setzen, um SQL Injection zu vermeiden
                        command.Parameters.AddWithValue("@Vorname", vorname);
                        command.Parameters.AddWithValue("@Nachname", nachname);
                        command.Parameters.AddWithValue("@Geburtsdatum", geburtsdatum);

                        // Ausführen der SQL-Abfrage
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Daten erfolgreich in die Datenbank eingefügt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Einfügen der Daten: {ex.Message}");
            }
        }

        private void Abbruch_Click(object sender, RoutedEventArgs e)
        {
            // Anwendung schließen
            this.Close();
            
        }
    }
}
