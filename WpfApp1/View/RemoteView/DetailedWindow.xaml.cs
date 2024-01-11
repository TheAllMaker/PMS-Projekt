using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MediTrack.Model.RemoteModel;
//using System.Windows.Forms;

namespace MediTrack.View.RemoteView
{
    public partial class DetailedWindow : Window
    {
        private double respirationRateMin;
        private double respirationRateMax;
        private double oxygenLevelMin;
        private double oxygenLevelMax;
        private double temperatureMin;
        private double temperatureMax;
        private int heartRateMin;
        private int heartRateMax;
        private int systolicBloodPressureMin;
        private int systolicBloodPressureMax;
        private int diastolicBloodPressureMin;
        private int diastolicBloodPressureMax;

        private int heartRateMinThreshold = 40;
        private int heartRateMaxThreshold = 200;
        private int systolicBloodPressureMinThreshold = 90;
        private int systolicBloodPressureMaxThreshold = 220;
        private int diastolicBloodPressureMinThreshold = 60;
        private int diastolicBloodPressureMaxThreshold = 80;
        private double temperatureMinThreshold = 35;
        private double temperatureMaxThreshold = 39;
        private double oxygenLevelMinThreshold = 91;
        private double oxygenLevelMaxThreshold = 96;
        private double respirationRateMinThreshold = 8;
        private double respirationRateMaxThreshold = 25;

        private int _monitorId;

        private Threshold threshold;

        public DetailedWindow(int monitorID)
        {
            InitializeComponent();
            _monitorId = monitorID;

            RespirationRateTextBoxMin.PreviewTextInput += ValidateTextInput;
            RespirationRateTextBoxMax.PreviewTextInput += ValidateTextInput;
            OxygenLevelTextBoxMin.PreviewTextInput += ValidateTextInput;
            OxygenLevelTextBoxMax.PreviewTextInput += ValidateTextInput;
            TemperatureTextBoxMin.PreviewTextInput += ValidateTextInput;
            TemperatureTextBoxMax.PreviewTextInput += ValidateTextInput;
            HeartRateTextBoxMin.PreviewTextInput += ValidateTextInput;
            HeartRateTextBoxMax.PreviewTextInput += ValidateTextInput;

            DataObject.AddPastingHandler(RespirationRateTextBoxMin, OnPaste);
            DataObject.AddPastingHandler(RespirationRateTextBoxMax, OnPaste);
            DataObject.AddPastingHandler(OxygenLevelTextBoxMin, OnPaste);
            DataObject.AddPastingHandler(OxygenLevelTextBoxMax, OnPaste);
            DataObject.AddPastingHandler(TemperatureTextBoxMin, OnPaste);
            DataObject.AddPastingHandler(TemperatureTextBoxMax, OnPaste);
            DataObject.AddPastingHandler(HeartRateTextBoxMin, OnPaste);
            DataObject.AddPastingHandler(HeartRateTextBoxMax, OnPaste);

            threshold = Threshold.GetThresholdByMonitorID(_monitorId);
            if (threshold != null)
            {
                RespirationRateTextBoxMin.Text = threshold.GetRespirationRateMin().ToString();
                RespirationRateTextBoxMax.Text = threshold.GetRespirationRateMax().ToString();
                OxygenLevelTextBoxMin.Text = threshold.GetOxygenLevelMin().ToString();
                OxygenLevelTextBoxMax.Text = threshold.GetOxygenLevelMax().ToString();
                TemperatureTextBoxMin.Text = threshold.GetTemperatureMin().ToString();
                TemperatureTextBoxMax.Text = threshold.GetTemperatureMax().ToString();
                HeartRateTextBoxMin.Text = threshold.GetHeartRateMin().ToString();
                HeartRateTextBoxMax.Text = threshold.GetHeartRateMax().ToString();
                SystolicBloodPressureTextBoxMin.Text = threshold.GetSystolicBloodPressureMin().ToString();
                SystolicBloodPressureTextBoxMax.Text = threshold.GetSystolicBloodPressureMax().ToString();
                DiastolicBloodPressureTextBoxMin.Text = threshold.GetDiastolicBloodPressureMin().ToString();
                DiastolicBloodPressureTextBoxMax.Text = threshold.GetDiastolicBloodPressureMax().ToString();
            }
        }

        private void ValidateTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!Regex.IsMatch(text, "^[0-9]+$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void UpdateVariableFromTextbox(int minValue, int maxValue, int minThreshold, int maxThreshold)
        {
                // Zusätzlich: Stelle sicher, dass heartRateMin nicht größer ist als heartRateMax
                if (minValue > maxValue)
                {
                    //heartRateMax = heartRateMin+1;
                    //HeartRateTextBoxMax.Text = heartRateMax.ToString();
                    MessageBox.Show("Fehler bei der Eingabe. Min Wert ist größer als Max Wert", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (heartRateMax < heartRateMin)
                {
                    heartRateMin = variable;
                    HeartRateTextBoxMin.Text = variable.ToString();
                }

                // Erlaube Werte innerhalb des Bereichs von minThreshold bis maxThreshold
                if (value >= minThreshold && value <= maxThreshold)
                {
                    variable = value;
                }
                else
                {
                    // Setze den Wert auf den näheren Schwellenwert.
                    if (value < minThreshold)
                    {
                        variable = minThreshold;
                        textBox.Text = minThreshold.ToString();
                    }
                    else
                    {
                        variable = maxThreshold;
                        textBox.Text = maxThreshold.ToString();
                    }
                }
        }

        private void UpdateVariableFromTextbox(double minValue, double maxValue, double minThreshold, double maxThreshold)
        {
            if (double.TryParse(textBox.Text, out double value))
            {
                if (value >= minThreshold && value <= maxThreshold)
                {
                    variable = value;
                }
                else
                {
                    //MessageBox.Show("Der eingegebene Wert liegt außerhalb des gültigen Bereichs.");
                }
            }
        }


        private void Button_Click_SelectionConfirmed(object sender, RoutedEventArgs e)
        {
            respirationRateMin = double.Parse(RespirationRateTextBoxMin.Text);
            respirationRateMax = double.Parse(RespirationRateTextBoxMax.Text);
            if (respirationRateMin > respirationRateMax)
            {
                MessageBox.Show("Respiration min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UpdateVariableFromTextbox(respirationRateMin, respirationRateMin, respirationRateMinThreshold, respirationRateMaxThreshold);
            }

            oxygenLevelMin = double.Parse(OxygenLevelTextBoxMin.Text);
            oxygenLevelMax = double.Parse(OxygenLevelTextBoxMax.Text);
            if (oxygenLevelMin > oxygenLevelMax)
            {
                MessageBox.Show("Oxygen level min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UpdateVariableFromTextbox(oxygenLevelMin, oxygenLevelMin, oxygenLevelMinThreshold, oxygenLevelMaxThreshold);
            }

            temperatureMin = double.Parse(TemperatureTextBoxMin.Text);
            temperatureMax = double.Parse(TemperatureTextBoxMax.Text);
            if (temperatureMin > temperatureMax)
            {
                MessageBox.Show("Temperature min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UpdateVariableFromTextbox(temperatureMin, temperatureMin, temperatureMinThreshold, temperatureMaxThreshold);
            }

            heartRateMin = int.Parse(HeartRateTextBoxMin.Text);
            heartRateMax = int.Parse(HeartRateTextBoxMax.Text);
            if (heartRateMin > heartRateMax)
            {
                MessageBox.Show("Heart rate min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UpdateVariableFromTextbox(heartRateMin, heartRateMax, heartRateMinThreshold, heartRateMaxThreshold);
            }

            systolicBloodPressureMin = int.Parse(SystolicBloodPressureTextBoxMin.Text);
            systolicBloodPressureMax = int.Parse(SystolicBloodPressureTextBoxMax.Text);
            if (systolicBloodPressureMin > systolicBloodPressureMax)
            {
                MessageBox.Show("Systolic blood pressure min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UpdateVariableFromTextbox(systolicBloodPressureMin, systolicBloodPressureMax, systolicBloodPressureMinThreshold, systolicBloodPressureMaxThreshold);
            }

            diastolicBloodPressureMin = int.Parse(DiastolicBloodPressureTextBoxMin.Text);
            diastolicBloodPressureMax = int.Parse(DiastolicBloodPressureTextBoxMax.Text);
            if (diastolicBloodPressureMin > diastolicBloodPressureMax)
            {
                MessageBox.Show("Diastolic blood pressure min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UpdateVariableFromTextbox(diastolicBloodPressureMin, diastolicBloodPressureMax, diastolicBloodPressureMinThreshold, diastolicBloodPressureMaxThreshold);
            }

            threshold = new Threshold(_monitorId, heartRateMin, heartRateMax, respirationRateMin, respirationRateMax,
                oxygenLevelMin, oxygenLevelMax, temperatureMin, temperatureMax, systolicBloodPressureMin, systolicBloodPressureMax,
                diastolicBloodPressureMin, diastolicBloodPressureMax);

            this.Close();
        }



        private void Button_Click_SelectionClosed(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
 
    }
}



