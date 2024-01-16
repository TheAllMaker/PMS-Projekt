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
    public partial class DisconnectWindow : Window
    {
        private static string connString = "Host=db.inftech.hs-mannheim.de;Username=pms1;Password=pms1;Database=pms1";

        public DisconnectWindow()
        {
            InitializeComponent();
            FillConnectionComboBox();
        }
        private static void FillConnectionComboBox()
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

                                // Create a ComboBoxItem
                                ComboBoxItem comboBoxItem = new ComboBoxItem
                                {
                                    Content = $"{pid}: {name}, {vorname}",
                                    Tag = pid
                                };

                               
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
        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
