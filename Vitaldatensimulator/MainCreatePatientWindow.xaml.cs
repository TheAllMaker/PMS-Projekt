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
            Loaded += MainCreatePatientWindow_Loaded;
        }

        private void MainCreatePatientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Setze den Initialwert der Slider in die Mitte
            HeartRateSlider.Value = (HeartRateSlider.Minimum + HeartRateSlider.Maximum) / 2;
            RespirationRateSlider.Value = (RespirationRateSlider.Minimum + RespirationRateSlider.Maximum) / 2;
            OxygenLevelSlider.Value = (OxygenLevelSlider.Minimum + OxygenLevelSlider.Maximum) / 2;
            BloodPressureSystolicSlider.Value = (BloodPressureSystolicSlider.Minimum + BloodPressureSystolicSlider.Maximum) / 2;
            BloodPressureDiastolicSlider.Value = (BloodPressureDiastolicSlider.Minimum + BloodPressureDiastolicSlider.Maximum) / 2;

            // Eventhandler für die Änderungen der Slider-Werte
//            HeartRateSlider.ValueChanged += Slider_ValueChanged;
//            RespirationRateSlider.ValueChanged += Slider_ValueChanged;
//            OxygenLevelSlider.ValueChanged += Slider_ValueChanged;
//            BloodPressureSystolicSlider.ValueChanged += Slider_ValueChanged;
//            BloodPressureDiastolicSlider.ValueChanged += Slider_ValueChanged;
        }

        // Methode zum Aktualisieren der Textboxen mit dem Slider-Wert
        //       private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //       {
        //           var slider = sender as Slider;
        //           if (slider != null)
        //           {
        // Finde die zugehörige TextBox
        //               TextBox textBox = FindName($"{slider.Name}Box") as TextBox;
        //               if (textBox != null)
        //                {
        //                    // Aktualisiere den Wert der TextBox mit dem Slider-Wert
        //                    textBox.Text = slider.Value.ToString();
        //                }
        //            }
        //        }

        private void HeartRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HeartRateBox.Text = HeartRateSlider.Value.ToString();
        }

        // Ereignisbehandlungsroutine für die Atemfrequenz
        private void RespirationRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RespirationRateBox.Text = RespirationRateSlider.Value.ToString();
        }

        // Ereignisbehandlungsroutine für den Sauerstoffgehalt
        private void OxygenLevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OxygenLevelBox.Text = OxygenLevelSlider.Value.ToString();
        }

        // Ereignisbehandlungsroutine für den systolischen Blutdruck
        private void BloodPressureSystolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SystolicBloodPressureBox.Text = BloodPressureSystolicSlider.Value.ToString();
        }

        // Ereignisbehandlungsroutine für den diastolischen Blutdruck
        private void BloodPressureDiastolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DiastolicBloodPressureBox.Text = BloodPressureDiastolicSlider.Value.ToString();
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