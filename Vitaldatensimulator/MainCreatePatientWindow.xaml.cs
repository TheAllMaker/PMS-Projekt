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

namespace Vitaldatensimulator
{
    public partial class MainCreatePatientWindow : Window
    {
        public MainCreatePatientWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            // Beispiel: Beim Bestätigen alle aktuellen Werte der Steuerelemente abrufen
            string MonitorID = MonitorIDBox.Text;
            double HeartRate = HeartRateSlider.Value;
            double RespirationRate = RespirationRateSlider.Value;
            double OxygenLevel = OxygenLevelSlider.Value;
            double BloodPressureSystolic = BloodPressureSystolicSlider.Value;
            double BloodPressureDiastolic = BloodPressureDiastolicSlider.Value;
            // Weitere Werte von anderen Steuerelementen hier abrufen

            // Funktion aufrufen, um alle aktuellen Werte zu verwenden
            VitaldatenSimulator.DoMqttAndDataOperations(MonitorID, HeartRate, RespirationRate, OxygenLevel, BloodPressureSystolic, BloodPressureDiastolic);
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {

        }
    }
}