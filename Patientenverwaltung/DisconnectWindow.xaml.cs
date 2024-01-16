using Npgsql;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


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
                    int pid, moid;
                    if (TryParseIdsFromComboBoxItem(selectedItemText, out pid, out moid))
                    {
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
                                    // Show the status (checkmark) with fade-out animation
                                    DisconnectStatus.Visibility = Visibility.Visible;
                                    DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.8)); // x seconds fade-out
                                    DisconnectStatus.BeginAnimation(TextBlock.OpacityProperty, animation);

                                    // Remove the item from the ComboBox
                                    CmbConnection.Items.Remove(selectedItemText);

                                    // You may also need to refresh the ComboBox if you have a data source
                                    // CmbConnection.ItemsSource = GetUpdatedDataSource(); // Update this line with your actual data source method
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
                        MessageBox.Show("Fehler beim Parsen von pid und moid.");
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




        private bool TryParseIdsFromComboBoxItem(string itemText, out int pid, out int moid)
        {
            pid = moid = 0; // Set default values

            try
            {
                // Use regular expressions to extract numeric values
                var matches = Regex.Matches(itemText, @"\d+");

                if (matches.Count >= 2)
                {
                    pid = int.Parse(matches[0].Value);
                    moid = int.Parse(matches[1].Value);

                    return true; // Parsing successful
                }
                else
                {
                    return false; // Unexpected format
                }
            }
            catch (Exception)
            {
                return false; // Parsing failed
            }
        }





        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
