using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Patientenverwaltung
{
    /// <summary>
    /// Interaktionslogik für DisconnectWindow.xaml
    /// </summary>
    public partial class DisconnectWindow
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";

        public DisconnectWindow()
        {
            InitializeComponent();
            FillConnectionComboBox();
        }
        private void FillConnectionComboBox()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    connection.Open();

                    string query = "SELECT b.pid, p.name, p.vorname, b.moid, m.mf " +
                                   "FROM belegung b " +
                                   "JOIN patients p ON b.pid = p.pid " +
                                   "JOIN monitors m ON b.moid = m.moid";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pid = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string vorname = reader.GetString(2);
                                int moid = reader.GetInt32(3);
                                string mf = reader.GetString(4);

                                // Create a string representation of the item
                                string comboBoxItemText = $"{pid}: {name}, {vorname} - Monitor {moid}: {mf}";

                                // Add the string to the ComboBox
                                CmbConnection.Items.Add(comboBoxItemText);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Patienten und Monitore: {ex.Message}");
            }
        }


        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CmbConnection.SelectedItem != null)
                {
                    string selectedItemText = CmbConnection.SelectedItem.ToString();

                    // Parse the selected item text to extract pid and moid
                    int pid = int.Parse(selectedItemText.Split(':')[0].Trim());
                    int moid = int.Parse(selectedItemText.Split('-')[1].Split(':')[0].Trim());

                    // Perform the deletion in the belegung table
                    using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM belegung WHERE pid = @pid AND moid = @moid";

                        using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@pid", pid);
                            command.Parameters.AddWithValue("@moid", moid);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Verbindung erfolgreich getrennt.");
                                // You may want to refresh the ComboBox or take other actions after successful deletion.
                            }
                            else
                            {
                                MessageBox.Show("Verbindung konnte nicht getrennt werden.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bitte wählen Sie einen Eintrag aus der ComboBox aus.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Trennen der Verbindung: {ex.Message}");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
