using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            VitaldatenSimulator.VitalDataUpdated += VitaldatenSimulator_VitalDataUpdated;
        }

        private void VitaldatenSimulator_VitalDataUpdated(object sender, VitalDataEventArgs e)
        {
            // Hier kannst du die Werte aus e.* verwenden, um die Slider-Werte in der UI zu aktualisieren
            Dispatcher.Invoke(() =>
            {
                HeartRateValueTextBlock.Text = e.HeartRate.ToString();
                RespirationRateValueTextBlock.Text = e.RespirationRate.ToString();
                OxygenLevelValueTextBlock.Text = e.OxygenLevel.ToString();
                BloodPressureSystolicValueTextBlock.Text = e.BloodPressureSystolic.ToString();
                BloodPressureDiastolicValueTextBlock.Text = e.BloodPressureDiastolic.ToString();
                double value = Math.Round(e.Temperature, 1);
                TemperatureValueTextBlock.Text = value.ToString("0.0");

            });
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

        private void HeartRateBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void RespirationRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(RespirationRateBox, RespirationRateSlider);
        }

        private void RespirationRateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(RespirationRateBox, RespirationRateSlider);
        }

        private void RespirationRateBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void OxygenLevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(OxygenLevelBox, OxygenLevelSlider);
        }

        private void OxygenLevelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(OxygenLevelBox, OxygenLevelSlider);
        }

        private void OxygenLevelBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void BloodPressureSystolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureSystolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureSystolicBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void BloodPressureDiastolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void BloodPressureDiastolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void BloodPressureDiastolicBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TemperatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTextBoxFromSlider(TemperatureBox, TemperatureSlider);
        }

        private void TemperatureBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(TemperatureBox, TemperatureSlider);
        }

        private void TemperatureBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // Erlaubt nur Zahlen
            return !regex.IsMatch(text);
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Button confirmButton = sender as Button;
            if (confirmButton != null)
            {
                if (confirmButton.Content.ToString() == "Start")
                {
                    if (MonitorIDBox != null && int.TryParse(MonitorIDBox.Text, out int monitorID))
                    {
                        // Überprüfung, ob MonitorID eine positive Zahl ist
                        if (monitorID > 0)
                        {
                            confirmButton.Content = "Continue";
                            int HeartRate = Convert.ToInt32(HeartRateSlider.Value);
                            int RespirationRate = Convert.ToInt32(RespirationRateSlider.Value);
                            int OxygenLevel = Convert.ToInt32(OxygenLevelSlider.Value);
                            int BloodPressureSystolic = Convert.ToInt32(BloodPressureSystolicSlider.Value);
                            int BloodPressureDiastolic = Convert.ToInt32(BloodPressureDiastolicSlider.Value);
                            double Temperature = TemperatureSlider.Value;

                            MonitorVitalDaten newPatient = new MonitorVitalDaten(monitorID, HeartRate, RespirationRate, OxygenLevel, BloodPressureSystolic, BloodPressureDiastolic, Temperature);

                            VitaldatenSimulator.DoMqttAndDataOperations(newPatient);

                            MessageBox.Show("Erfolgreich einen Monitor erstellt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Bitte geben Sie eine gültige Monitor-ID (positive Zahl) ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bitte geben Sie eine gültige Monitor-ID (Zahl) ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    VitaldatenSimulator.isSendingData = true;
                }
            }
        }


        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            VitaldatenSimulator.isSendingData = false;
            MessageBox.Show("Erfolgreich Generierung der Daten gestoppt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

