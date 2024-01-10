//using System;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MediTrack.Model.RemoteModel;

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

        private int iD;

        private Threshold threshold;

        public DetailedWindow(int ID)
        {
            InitializeComponent();
            iD = ID;

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

            threshold = Threshold.GetThresholdByMonitorID(iD);
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

        private void UpdateVariableFromTextbox(TextBox textBox, ref int variable, int minThreshold, int maxThreshold)
        {
            if (int.TryParse(textBox.Text, out int value))
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

        private void UpdateVariableFromTextbox(TextBox textBox, ref double variable, double minThreshold, double maxThreshold)
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
            UpdateVariableFromTextbox(RespirationRateTextBoxMin, ref respirationRateMin, respirationRateMinThreshold, respirationRateMaxThreshold);
            UpdateVariableFromTextbox(RespirationRateTextBoxMax, ref respirationRateMax, respirationRateMinThreshold, respirationRateMaxThreshold);

            UpdateVariableFromTextbox(OxygenLevelTextBoxMin, ref oxygenLevelMin, oxygenLevelMinThreshold, oxygenLevelMaxThreshold);
            UpdateVariableFromTextbox(OxygenLevelTextBoxMax, ref oxygenLevelMax, oxygenLevelMinThreshold, oxygenLevelMaxThreshold);

            UpdateVariableFromTextbox(TemperatureTextBoxMin, ref temperatureMin, temperatureMinThreshold, temperatureMaxThreshold);
            UpdateVariableFromTextbox(TemperatureTextBoxMax, ref temperatureMax, temperatureMinThreshold, temperatureMaxThreshold);

            UpdateVariableFromTextbox(HeartRateTextBoxMin, ref heartRateMin, heartRateMinThreshold, heartRateMaxThreshold);
            UpdateVariableFromTextbox(HeartRateTextBoxMax, ref heartRateMax, heartRateMinThreshold, heartRateMaxThreshold);

            UpdateVariableFromTextbox(SystolicBloodPressureTextBoxMin, ref systolicBloodPressureMin, systolicBloodPressureMinThreshold, systolicBloodPressureMaxThreshold);
            UpdateVariableFromTextbox(SystolicBloodPressureTextBoxMax, ref systolicBloodPressureMax, systolicBloodPressureMinThreshold, systolicBloodPressureMaxThreshold);

            UpdateVariableFromTextbox(DiastolicBloodPressureTextBoxMin, ref diastolicBloodPressureMin, diastolicBloodPressureMinThreshold, diastolicBloodPressureMaxThreshold);
            UpdateVariableFromTextbox(DiastolicBloodPressureTextBoxMax, ref diastolicBloodPressureMax, diastolicBloodPressureMinThreshold, diastolicBloodPressureMaxThreshold);

            threshold = new Threshold(iD, heartRateMin,heartRateMax, respirationRateMin, respirationRateMax, 
                oxygenLevelMin, oxygenLevelMax, temperatureMin, temperatureMax, systolicBloodPressureMin, systolicBloodPressureMax,
                diastolicBloodPressureMin, diastolicBloodPressureMax);

            this.Close();
        }


        private void Button_Click_SelectionClosed(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public double GetRespirationRateMin()
        {
            return respirationRateMin;
        }

        public double GetRespirationRateMax()
        {
            return respirationRateMax;
        }

        public double GetOxygenLevelMin()
        {
            return oxygenLevelMin;
        }

        public double GetOxygenLevelMax()
        {
            return oxygenLevelMax;
        }

        public double GetTemperatureMin()
        {
            return temperatureMin;
        }

        public double GetTemperatureMax()
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



