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
                double value;
                if (double.TryParse(textBox.Text, out value))
                {
                    if (value >= slider.Minimum && value <= slider.Maximum)
                    {
                        slider.Value = value;
                    }
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

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            string monitorID = MonitorIDBox.Text;
            double heartRate = HeartRateSlider.Value;
            double respirationRate = RespirationRateSlider.Value;
            double oxygenLevel = OxygenLevelSlider.Value;
            double bloodPressureSystolic = BloodPressureSystolicSlider.Value;
            double bloodPressureDiastolic = BloodPressureDiastolicSlider.Value;

            VitaldatenSimulator.DoMqttAndDataOperations(monitorID, heartRate, respirationRate, oxygenLevel, bloodPressureSystolic, bloodPressureDiastolic);
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            // Hier kannst du den Code für das Abbrechen implementieren
        }
    }
}