using System.Data;
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
        private double _respirationRateMin;
        private double _respirationRateMax;
        private double _oxygenLevelMin;
        private double _oxygenLevelMax;
        private double _temperatureMin;
        private double _temperatureMax;
        private int _heartRateMin;
        private int _heartRateMax;
        private int _systolicBloodPressureMin;
        private int _systolicBloodPressureMax;
        private int _diastolicBloodPressureMin;
        private int _diastolicBloodPressureMax;

        private const double TemperatureMinThreshold = 35;
        private const double TemperatureMaxThreshold = 39;
        private const double OxygenLevelMinThreshold = 91;
        private const double OxygenLevelMaxThreshold = 96;
        private const double RespirationRateMinThreshold = 8;
        private const double RespirationRateMaxThreshold = 25;
        private const int HeartRateMinThreshold = 40;
        private const int HeartRateMaxThreshold = 200;
        private const int SystolicBloodPressureMinThreshold = 90;
        private const int SystolicBloodPressureMaxThreshold = 220;
        private const int DiastolicBloodPressureMinThreshold = 60;
        private const int DiastolicBloodPressureMaxThreshold = 80;


        private readonly int _monitorId;

        private Threshold _threshold;

        private bool _hasError;

        public DetailedWindow(int monitorId)
        {
            InitializeComponent();
            _monitorId = monitorId;

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

            _threshold = Threshold.GetThresholdByMonitorID(_monitorId);
            if (_threshold != null)
            {
                RespirationRateTextBoxMin.Text = _threshold.GetRespirationRateMin().ToString();
                RespirationRateTextBoxMax.Text = _threshold.GetRespirationRateMax().ToString();
                OxygenLevelTextBoxMin.Text = _threshold.GetOxygenLevelMin().ToString();
                OxygenLevelTextBoxMax.Text = _threshold.GetOxygenLevelMax().ToString();
                TemperatureTextBoxMin.Text = _threshold.GetTemperatureMin().ToString();
                TemperatureTextBoxMax.Text = _threshold.GetTemperatureMax().ToString();
                HeartRateTextBoxMin.Text = _threshold.GetHeartRateMin().ToString();
                HeartRateTextBoxMax.Text = _threshold.GetHeartRateMax().ToString();
                SystolicBloodPressureTextBoxMin.Text = _threshold.GetSystolicBloodPressureMin().ToString();
                SystolicBloodPressureTextBoxMax.Text = _threshold.GetSystolicBloodPressureMax().ToString();
                DiastolicBloodPressureTextBoxMin.Text = _threshold.GetDiastolicBloodPressureMin().ToString();
                DiastolicBloodPressureTextBoxMax.Text = _threshold.GetDiastolicBloodPressureMax().ToString();
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
            UpdateVitalSigns("Respiration", ref _respirationRateMin, ref _respirationRateMax, RespirationRateMinThreshold, RespirationRateMaxThreshold, RespirationRateTextBoxMin, RespirationRateTextBoxMax);
            UpdateVitalSigns("Oxygen Level", ref _oxygenLevelMin,ref  _oxygenLevelMax, OxygenLevelMinThreshold, OxygenLevelMaxThreshold, OxygenLevelTextBoxMin, OxygenLevelTextBoxMax);
            UpdateVitalSigns("Temperature", ref _temperatureMin, ref _temperatureMax, TemperatureMinThreshold, TemperatureMaxThreshold, TemperatureTextBoxMin, TemperatureTextBoxMax);
            UpdateVitalSigns("Heart Rate", ref _heartRateMin, ref _heartRateMax, HeartRateMinThreshold, HeartRateMaxThreshold, HeartRateTextBoxMin, HeartRateTextBoxMax);
            UpdateVitalSigns("Systolic Blood Pressure", ref _systolicBloodPressureMin, ref _systolicBloodPressureMax, SystolicBloodPressureMinThreshold, SystolicBloodPressureMaxThreshold, SystolicBloodPressureTextBoxMin, SystolicBloodPressureTextBoxMax);
            UpdateVitalSigns("Diastolic Blood Pressure", ref _diastolicBloodPressureMin,ref  _diastolicBloodPressureMax, DiastolicBloodPressureMinThreshold, DiastolicBloodPressureMaxThreshold, DiastolicBloodPressureTextBoxMin, DiastolicBloodPressureTextBoxMax);

            if (!_hasError)
            {
                _threshold = new Threshold(_monitorId, _heartRateMin, _heartRateMax, _respirationRateMin, _respirationRateMax,
                    _oxygenLevelMin, _oxygenLevelMax, _temperatureMin, _temperatureMax, _systolicBloodPressureMin, _systolicBloodPressureMax,
                    _diastolicBloodPressureMin, _diastolicBloodPressureMax);

                this.Close();
            }
        }

        private void UpdateVitalSigns(string vitalSignName, ref double minValue, ref double maxValue, double minThreshold, 
            double maxThreshold, TextBox minTextBox, TextBox maxTextBox)
        {
            if (!string.IsNullOrWhiteSpace(minTextBox.Text) && !string.IsNullOrWhiteSpace(maxTextBox.Text) && double.TryParse(minTextBox.Text, out minValue) && minValue > 0 && double.TryParse(maxTextBox.Text, out maxValue) && maxValue > 0)
            {
                //minValue = double.Parse(minTextBox.Text);
                //maxValue = double.Parse(maxTextBox.Text);

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

        private void UpdateVitalSigns(string vitalSignName, ref int minValue, ref int maxValue, int minThreshold,
            int maxThreshold, TextBox minTextBox, TextBox maxTextBox)
        {

            //if (!string.IsNullOrWhiteSpace(minTextBox.Text) && !string.IsNullOrWhiteSpace(maxTextBox.Text) && int.Parse(minTextBox.Text) != 0 && int.Parse(maxTextBox.Text) != 0)
            //{
            if (!string.IsNullOrWhiteSpace(minTextBox.Text) && int.TryParse(minTextBox.Text, out minValue) && minValue > 0)
            {
                //minValue = int.Parse(minTextBox.Text);
                maxValue = int.Parse(maxTextBox.Text);

                if (minValue > maxValue && maxValue != 0)
                {
                    _hasError = true;
                    MessageBox.Show($"{vitalSignName} min value is bigger than max value", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    UpdateVariableFromTextbox(ref minValue, minThreshold, maxThreshold, minTextBox);
                    //UpdateVariableFromTextbox(ref maxValue, minThreshold, maxThreshold, maxTextBox);
                }
            }

            if (!string.IsNullOrWhiteSpace(maxTextBox.Text) && double.Parse(maxTextBox.Text) != 0)
            {
                maxValue = int.Parse(maxTextBox.Text);
                UpdateVariableFromTextbox(ref maxValue, minThreshold, maxThreshold, maxTextBox);
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
            Close();
        }
 
    }
}



