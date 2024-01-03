using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Patientenverwaltung
{
    public partial class ConnectWindow : Window
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";

        public ConnectWindow()
        {
            InitializeComponent();
            FillPatientsComboBox();
            FillMonitorsComboBox();
        }

        private void FillPatientsComboBox()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    connection.Open();

                    string query = "SELECT pid, name, vorname FROM patients";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pid = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string vorname = reader.GetString(2);
                                cmbPatient.Items.Add(new ComboBoxItem { Content = $"{pid}: {name}, {vorname}", Tag = pid });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Patienten: {ex.Message}");
            }
        }

        private void FillMonitorsComboBox()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    connection.Open();

                    string query = "SELECT moid, mf, sn FROM monitors";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int moid = reader.GetInt32(0);
                                string mf = reader.GetString(1);
                                string sn = reader.GetString(2);
                                cmbMonitor.Items.Add(new ComboBoxItem { Content = $"{moid}: {mf}, {sn}", Tag = moid });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Monitore: {ex.Message}");
            }
        }

        private void Verbinden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbPatient.SelectedItem != null && cmbMonitor.SelectedItem != null)
                {
                    int patientId = (int)((ComboBoxItem)cmbPatient.SelectedItem).Tag;
                    int monitorId = (int)((ComboBoxItem)cmbMonitor.SelectedItem).Tag;

                    using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                    {
                        connection.Open();

                        // Überprüfen, ob die Verbindung bereits existiert
                        string checkQuery = "SELECT COUNT(*) FROM belegung WHERE pid = @PatientId";

                        using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@PatientId", patientId);

                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                // Update durchführen, wenn der Patient bereits in der belegung existiert
                                string updateQuery = "DELETE FROM belegung WHERE moid = @MonitorId; " +
                                                 "UPDATE belegung SET moid = @MonitorId WHERE pid = @PatientId";


                                using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@MonitorId", monitorId);
                                    updateCommand.Parameters.AddWithValue("@PatientId", patientId);
                                    updateCommand.ExecuteNonQuery();

                                    MessageBox.Show("Verbindung erfolgreich aktualisiert.");
                                }
                            }
                            else
                            {
                                // Insert durchführen, wenn der Patient noch nicht in der belegung existiert
                                string insertQuery = "INSERT INTO belegung (moid, pid) VALUES (@MonitorId, @PatientId)";

                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@MonitorId", monitorId);
                                    insertCommand.Parameters.AddWithValue("@PatientId", patientId);
                                    insertCommand.ExecuteNonQuery();

                                    MessageBox.Show("Verbindung erfolgreich erstellt.");
                                    // Zurücksetzen der Auswahl in den ComboBoxen
                                    cmbPatient.SelectedItem = null;
                                    cmbMonitor.SelectedItem = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bitte wählen Sie Patient und Monitor aus, um eine Verbindung herzustellen.");
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen der Verbindung: {ex.Message}");
            }
        }



        private void Abbruch_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
