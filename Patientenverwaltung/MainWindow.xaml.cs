using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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


    }
}
