using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediTrack.View.RemoteView
{
    public partial class DetailedWindow : Window
    {
        private int respirationRateMin;
        private int respirationRateMax;
        private int oxygenLevelMin;
        private int oxygenLevelMax;
        private int temperatureMin;
        private int temperatureMax;
        private int heartRateMin;
        private int heartRateMax;
        private int systolicBloodPressureMin;
        private int systolicBloodPressureMax;
        private int diastolicBloodPressureMin;
        private int diastolicBloodPressureMax;


        public DetailedWindow()
        {
            InitializeComponent();
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

        private void UpdateVariableFromTextbox(TextBox textBox, ref int variable)
        {
            if (int.TryParse(textBox.Text, out int value))
            {
                variable = value;
            }
        }

        private void RespirationRateTextBoxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(RespirationRateTextBoxMin, ref respirationRateMin);
        }

        private void RespirationRateTextBoxMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(RespirationRateTextBoxMax, ref respirationRateMax);
        }

        private void OxygenLevelTextBoxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(OxygenLevelTextBoxMin, ref oxygenLevelMin);
        }

        private void OxygenLevelTextBoxMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(OxygenLevelTextBoxMax, ref oxygenLevelMax);
        }

        private void TemperatureTextBoxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(TemperatureTextBoxMin, ref temperatureMin);
        }

        private void TemperatureTextBoxMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(TemperatureTextBoxMax, ref temperatureMax);
        }

        private void HeartRateTextBoxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(HeartRateTextBoxMin, ref heartRateMin);
        }

        private void HeartRateTextBoxMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(HeartRateTextBoxMax, ref heartRateMax);
        }

        private void SystolicBloodPressureTextBoxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(SystolicBloodPressureTextBoxMin, ref systolicBloodPressureMin);
        }

        private void SystolicBloodPressureTextBoxMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(SystolicBloodPressureTextBoxMax, ref systolicBloodPressureMax);
        }

        private void DiastolicBloodPressureTextBoxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(DiastolicBloodPressureTextBoxMin, ref diastolicBloodPressureMin);
        }

        private void DiastolicBloodPressureTextBoxMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVariableFromTextbox(DiastolicBloodPressureTextBoxMax, ref diastolicBloodPressureMax);
        }



        public int GetRespirationRateMin()
        {
            return respirationRateMin;
        }

        public int GetRespirationRateMax()
        {
            return respirationRateMax;
        }

        public int GetOxygenLevelMin()
        {
            return oxygenLevelMin;
        }

        public int GetOxygenLevelMax()
        {
            return oxygenLevelMax;
        }

        public int GetTemperatureMin()
        {
            return temperatureMin;
        }

        public int GetTemperatureMax()
        {
            return temperatureMax;
        }

        public int GetHeartRateMin()
        {
            return heartRateMin;
        }

        public int GetHeartRateMax()
        {
            return heartRateMax;
        }


        public int GetSystolicBloodPressureMin()
        {
            return systolicBloodPressureMin;
        }

        public int GetSystolicBloodPressureMax()
        {
            return systolicBloodPressureMax;
        }

        public int GetDiastolicBloodPressureMin()
        {
            return diastolicBloodPressureMin;
        }

        public int GetDiastolicBloodPressureMax()
        {
            return diastolicBloodPressureMax;
        }
    }
}

