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
using System.Windows.Shapes;

namespace Patientenverwaltung
{
    /// <summary>
    /// Interaktionslogik für NewMonitorWindow.xaml
    /// </summary>
    public partial class NewMonitorWindow : Window
    {
        public NewMonitorWindow()
        {
            InitializeComponent();
        }
        private void MonitorAnlegen_Click(object sender, RoutedEventArgs e)
        {
            // Öffne ConnectPatientMonitor
            MessageBox.Show("Monitor wurde angelegt!");
        }

        private void Abbruch_Click(object sender, RoutedEventArgs e)
        {
            // Anwendung schließen
            this.Close();

        }
    }
}
