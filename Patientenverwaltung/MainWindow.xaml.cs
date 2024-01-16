using System.Windows;

namespace Patientenverwaltung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool isAlreadyClosing = false;
        private PowerWindow powerWindow;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }
        private void Button_Click_AddPatient(object sender, RoutedEventArgs e)
        {
            // Öffne NewPatientWindow als modales Dialogfeld
            NewPatientWindow newPatientWindow = new NewPatientWindow
            {
                Title = "Add New Patient",
                Width = 800,
                Height = 450,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Zeige das Fenster als modales Dialogfeld an
            newPatientWindow.ShowDialog();
        }



        private void Button_Click_AddMonitor(object sender, RoutedEventArgs e)
        {
            // Öffne NewMonitorWindow
            Window NewMonitorWindow = new NewMonitorWindow
            {
                Title = "Add New Monitor",
                Width = 800,
                Height = 450,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            NewMonitorWindow.ShowDialog();

        }

        private void Button_Click_ConnectPM(object sender, RoutedEventArgs e)
        {
            // Öffne ConnectPatientMonitor
            Window ConnectWindow = new ConnectWindow
            {
                Title = "Connect Patient with Monitor",
                Width = 800,
                Height = 450,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            ConnectWindow.ShowDialog();

        }

        private void Button_Click_DisconnectPM(object sender, RoutedEventArgs e)
        {
            // Öffne ConnectPatientMonitor
            Window DisconnectWindow = new DisconnectWindow
            {
                Title = "Connect Patient with Monitor",
                Width = 800,
                Height = 450,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            DisconnectWindow.ShowDialog();

        }

        private void Button_Click_PowerOff(object sender, RoutedEventArgs e)
        {
            // Hier wird eine Instanz von PowerWindow erstellt
            Window PowerWindow = new PowerWindow
            {
                Title = "Connect Patient with Monitor",
                Width = 800,
                Height = 450,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Das Fenster wird geöffnet
            PowerWindow.Show();

            // Hier wird das Hauptfenster als Owner für das PowerWindow festgelegt
            PowerWindow.Owner = this;
        }



        public void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isAlreadyClosing)
            {
                if (powerWindow == null || !powerWindow.IsVisible)
                {
                    // If the PowerWindow instance doesn't exist or is not visible, create and show it.
                    powerWindow = new PowerWindow
                    {
                        Title = "Connect Patient with Monitor",
                        Width = 800,
                        Height = 450,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };

                    powerWindow.Owner = this;
                    powerWindow.Show();
                }
                else
                {
                    // If the PowerWindow is already visible, just bring it to the front.
                    powerWindow.Activate();
                }

                // Cancel the main window closing to handle it in PowerWindow.
                e.Cancel = true;
            }
        }


    }
}
