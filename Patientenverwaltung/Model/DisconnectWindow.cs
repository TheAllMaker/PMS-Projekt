using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
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
                            // Erstelle eine Liste für die sortierten Einträge
                            var sortedEntries = new List<string>();

                            while (reader.Read())
                            {
                                int pid = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string vorname = reader.GetString(2);
                                int moid = reader.GetInt32(3);
                                string mf = reader.GetString(4);

                                // Erstelle eine string-Repräsentation des Eintrags
                                string comboBoxItemText = $"{pid}: {name}, {vorname} - Monitor {moid}: {mf}";

                                // Füge den Eintrag zur sortierten Liste hinzu
                                sortedEntries.Add(comboBoxItemText);
                            }

                            // Sortiere die Liste nach der PID
                            sortedEntries = sortedEntries.OrderBy(entry =>
                                int.Parse(Regex.Match(entry, @"\d+").Value)).ToList();

                            // Füge die sortierten Einträge zur ComboBox hinzu
                            foreach (var sortedEntry in sortedEntries)
                            {
                                CmbConnection.Items.Add(sortedEntry);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients and monitors: {ex.Message}");
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
                                    MessageBox.Show("Disconnect failed.");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error when parsing pid and moid.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select an entry from the ComboBox.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Disconnect failed: {ex.Message}");
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
