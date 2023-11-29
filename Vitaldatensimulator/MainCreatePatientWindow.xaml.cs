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
            InitializeSliderValues();
        }

        private void InitializeSliderValues()
        {
            SetSliderToMiddleValue(HeartRateSlider);
            SetSliderToMiddleValue(RespirationRateSlider);
            SetSliderToMiddleValue(OxygenLevelSlider);
            SetSliderToMiddleValue(BloodPressureSystolicSlider);
            SetSliderToMiddleValue(BloodPressureDiastolicSlider);
            SetSliderToMiddleValue(TemperatureSlider);
        }

        private void SetSliderToMiddleValue(Slider slider)
        {
            if (slider != null)
            {
                slider.Value = (slider.Minimum + slider.Maximum) / 2;
            }
        }

        private void UpdateTextBoxFromSlider(TextBox textBox, Slider slider)
        {
            if (textBox != null && slider != null)
            {
                int value = Convert.ToInt32(slider.Value);
                textBox.Text = value.ToString();
            }
        }

        private void UpdateSliderFromTextBox(TextBox textBox, Slider slider)
        {
            if (textBox != null && slider != null)
            {
                if (slider.Name == "TemperatureSlider") // Überprüfung für den Temperatur-Slider
                {
                    double value = Math.Round(slider.Value, 1);
                    textBox.Text = value.ToString("0.0"); // Formatierung auf eine Dezimalstelle
                }
                else // Für andere Slider
                {
                    int value = Convert.ToInt32(slider.Value);
                    textBox.Text = value.ToString(); // Standardmäßige Anzeige als Ganzzahl
                }
            }
        }

        private void HeartRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(HeartRateBox, HeartRateSlider);
        }

        private void HeartRateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(HeartRateBox, HeartRateSlider);
        }

        private void RespirationRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(RespirationRateBox, RespirationRateSlider);
        }

        private void RespirationRateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(RespirationRateBox, RespirationRateSlider);
        }

        private void OxygenLevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(OxygenLevelBox, OxygenLevelSlider);
        }

        private void OxygenLevelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(OxygenLevelBox, OxygenLevelSlider);
        }

        private void BloodPressureSystolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureSystolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureDiastolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void BloodPressureDiastolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void TemperatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(TemperatureBox, TemperatureSlider);
        }

        private void TemperatureBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(TemperatureBox, TemperatureSlider);
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            //Zu int ändern?
            string MonitorID = MonitorIDBox.Text;
            double HeartRate = HeartRateSlider.Value;
            double RespirationRate = RespirationRateSlider.Value;
            double OxygenLevel = OxygenLevelSlider.Value;
            double BloodPressureSystolic = BloodPressureSystolicSlider.Value;
            double BloodPressureDiastolic = BloodPressureDiastolicSlider.Value;
            double Temperature = TemperatureSlider.Value;

            VitaldatenSimulator.DoMqttAndDataOperations(MonitorID, HeartRate, RespirationRate, OxygenLevel, BloodPressureSystolic, BloodPressureDiastolic, Temperature);

            MessageBox.Show("Erfolgreich einen Patienten erstellt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            // Hier kannst du den Code für das Abbrechen implementieren
        }
    }
}