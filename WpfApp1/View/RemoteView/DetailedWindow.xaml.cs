using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

        private double temperatureMinThreshold = 35;
        private double temperatureMaxThreshold = 39;
        private double oxygenLevelMinThreshold = 91;
        private double oxygenLevelMaxThreshold = 96;
        private double respirationRateMinThreshold = 8;
        private double respirationRateMaxThreshold = 25;
        private int heartRateMinThreshold = 40;
        private int heartRateMaxThreshold = 200;
        private int systolicBloodPressureMinThreshold = 90;
        private int systolicBloodPressureMaxThreshold = 220;
        private int diastolicBloodPressureMinThreshold = 60;
        private int diastolicBloodPressureMaxThreshold = 80;


        private int _monitorId;

        private Threshold threshold;

        bool _hasError;

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

        private void Button_Click_SelectionConfirmed(object sender, RoutedEventArgs e)
        {
            _hasError = false;
            UpdateVitalSigns("Respiration", ref respirationRateMin, ref respirationRateMax, respirationRateMinThreshold, respirationRateMaxThreshold, RespirationRateTextBoxMin, RespirationRateTextBoxMax);
            UpdateVitalSigns("Oxygen Level", ref oxygenLevelMin,ref  oxygenLevelMax, oxygenLevelMinThreshold, oxygenLevelMaxThreshold, OxygenLevelTextBoxMin, OxygenLevelTextBoxMax);
            UpdateVitalSigns("Temperature", ref temperatureMin, ref temperatureMax, temperatureMinThreshold, temperatureMaxThreshold, TemperatureTextBoxMin, TemperatureTextBoxMax);
            UpdateVitalSigns("Heart Rate", ref heartRateMin, ref heartRateMax, heartRateMinThreshold, heartRateMaxThreshold, HeartRateTextBoxMin, HeartRateTextBoxMax);
            UpdateVitalSigns("Systolic Blood Pressure", ref systolicBloodPressureMin, ref systolicBloodPressureMax, systolicBloodPressureMinThreshold, systolicBloodPressureMaxThreshold, SystolicBloodPressureTextBoxMin, SystolicBloodPressureTextBoxMax);
            UpdateVitalSigns("Diastolic Blood Pressure", ref diastolicBloodPressureMin,ref  diastolicBloodPressureMax, diastolicBloodPressureMinThreshold, diastolicBloodPressureMaxThreshold, DiastolicBloodPressureTextBoxMin, DiastolicBloodPressureTextBoxMax);

            if (!_hasError)
            {
                threshold = new Threshold(_monitorId, heartRateMin, heartRateMax, respirationRateMin, respirationRateMax,
                    oxygenLevelMin, oxygenLevelMax, temperatureMin, temperatureMax, systolicBloodPressureMin, systolicBloodPressureMax,
                    diastolicBloodPressureMin, diastolicBloodPressureMax);

                this.Close();
            }
        }

        private void UpdateVitalSigns(string vitalSignName, ref double minValue, ref double maxValue, double minThreshold, double maxThreshold, TextBox minTextBox, TextBox maxTextBox)
        {
            if (!string.IsNullOrWhiteSpace(minTextBox.Text) && !string.IsNullOrWhiteSpace(maxTextBox.Text) && double.Parse(minTextBox.Text) != 0 && double.Parse(maxTextBox.Text) != 0)
            {
                minValue = double.Parse(minTextBox.Text);
                maxValue = double.Parse(maxTextBox.Text);

                if (minValue > maxValue)
                {
                    _hasError = true;
                    MessageBox.Show($"{vitalSignName} min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    UpdateVariableFromTextbox(ref minValue, minThreshold, maxThreshold, minTextBox);
                    UpdateVariableFromTextbox(ref maxValue, minThreshold, maxThreshold, maxTextBox);
                }
            }
        }

        private void UpdateVitalSigns(string vitalSignName, ref int minValue, ref int maxValue, int minThreshold, int maxThreshold, TextBox minTextBox, TextBox maxTextBox)
        {
            if (!string.IsNullOrWhiteSpace(minTextBox.Text) && !string.IsNullOrWhiteSpace(maxTextBox.Text) && int.Parse(minTextBox.Text) != 0 && int.Parse(maxTextBox.Text) != 0)
            {
                minValue = int.Parse(minTextBox.Text);
                maxValue = int.Parse(maxTextBox.Text);

                if (minValue > maxValue)
                {
                    _hasError = true;
                    MessageBox.Show($"{vitalSignName} min value is smaller than max value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    UpdateVariableFromTextbox(ref minValue, minThreshold, maxThreshold, minTextBox);
                    UpdateVariableFromTextbox(ref maxValue, minThreshold, maxThreshold, maxTextBox);
                }
            }
        }

        private void UpdateVariableFromTextbox(ref double variable, double minThreshold, double maxThreshold, TextBox textBox)
        {
            // Erlaube Werte innerhalb des Bereichs von minThreshold bis maxThreshold
            if (variable < minThreshold)
            {
                variable = minThreshold;
            }
            else if (variable > maxThreshold)
            {
                variable = maxThreshold;
            }

            // Setze den TextBox-Wert auf den aktualisierten Wert.
            textBox.Text = variable.ToString();
        }

        private void UpdateVariableFromTextbox(ref int variable, int minThreshold, int maxThreshold, TextBox textBox)
        {
            // Erlaube Werte innerhalb des Bereichs von minThreshold bis maxThreshold
            if (variable < minThreshold)
            {
                variable = minThreshold;
            }
            else if (variable > maxThreshold)
            {
                variable = maxThreshold;
            }

            // Setze den TextBox-Wert auf den aktualisierten Wert.
            textBox.Text = variable.ToString();
        }



        private void Button_Click_SelectionClosed(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
 
    }
}



