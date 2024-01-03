using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Vitaldatensimulator
{
    public partial class SimulatorUI : Window
    {
        private enum SimulationState
        {
            Stopped,
            Running,
            Paused
        }

        private SimulationState currentState = SimulationState.Stopped;
        private Dictionary<Slider, double> originalSliderValues = new Dictionary<Slider, double>();
        private SimulatorTimer mySimulatorTimer;
        private bool isValueChanged = false;
        private bool isAlreadyClosing = false;
        private bool isUUIDAlreadyCreated = false;
        private Guid identifier;
        private string UUID;
        private int zaehler;

        public SimulatorUI()
        {
            InitializeComponent();
            Loaded += MainCreatePatientWindow_Loaded;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            this.Closing += MainWindow_Closing;

            MqttPublisher.VitalDataUpdated += VitaldatenSimulator_VitalDataUpdated;
            mySimulatorTimer = new SimulatorTimer();
        }

        // Nur als Notfall
        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            // Ihr Code hier
            SetAliveStatusToZero();
        }

        private void MainCreatePatientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSliderValues();
            InitializeSliderOriginalValues();
            ConfirmChangesButton.IsEnabled = false;
        }

        private void VitaldatenSimulator_VitalDataUpdated(object sender, MonitorVitalDaten MonitorVitalDaten)
        {
            if (MonitorVitalDaten != null)
            {
                Dispatcher.Invoke(() =>
                {
                    HeartRateValueTextBlock.Text = MonitorVitalDaten.HeartRate.ToString();
                    RespirationRateValueTextBlock.Text = MonitorVitalDaten.RespirationRate.ToString();
                    OxygenLevelValueTextBlock.Text = MonitorVitalDaten.OxygenLevel.ToString();
                    BloodPressureSystolicValueTextBlock.Text = MonitorVitalDaten.BloodPressureSystolic.ToString();
                    BloodPressureDiastolicValueTextBlock.Text = MonitorVitalDaten.BloodPressureDiastolic.ToString();
                    double value = Math.Round(MonitorVitalDaten.Temperature, 1);
                    TemperatureValueTextBlock.Text = value.ToString("0.0");
                });
            }
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

        private void InitializeSliderOriginalValues()
        {
            originalSliderValues.Add(HeartRateSlider, HeartRateSlider.Value);
            originalSliderValues.Add(RespirationRateSlider, RespirationRateSlider.Value);
            originalSliderValues.Add(OxygenLevelSlider, OxygenLevelSlider.Value);
            originalSliderValues.Add(BloodPressureSystolicSlider, BloodPressureSystolicSlider.Value);
            originalSliderValues.Add(BloodPressureDiastolicSlider, BloodPressureDiastolicSlider.Value);
            originalSliderValues.Add(TemperatureSlider, TemperatureSlider.Value);

            HeartRateSlider.ValueChanged += Slider_ValueChanged;
            RespirationRateSlider.ValueChanged += Slider_ValueChanged;
            OxygenLevelSlider.ValueChanged += Slider_ValueChanged;
            BloodPressureSystolicSlider.ValueChanged += Slider_ValueChanged;
            BloodPressureDiastolicSlider.ValueChanged += Slider_ValueChanged;
            TemperatureSlider.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if (currentState != SimulationState.Stopped && originalSliderValues.ContainsKey(slider) && slider.Value != originalSliderValues[slider])
            {
                isValueChanged = true;
                // Aktiviere den Bestätigen-Button
                ConfirmChangesButton.IsEnabled = true;
            }
        }

        private void SetSliderToMiddleValue(Slider slider)
        {
            if (slider != null)
            {
                slider.Value = Math.Round((slider.Minimum + slider.Maximum) / 2);
            }
        }

        private void UpdateSliderAndTextBox(TextBox textBox, Slider slider)
        {
            if (textBox != null && slider != null)
            {
                if (slider.Name == "TemperatureSlider")
                {
                    double value = Math.Round(slider.Value, 1);
                    textBox.Text = value.ToString("0.0");
                }
                else
                {
                    int value = Convert.ToInt32(slider.Value);
                    textBox.Text = value.ToString();
                }
            }
        }

        private void HeartRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(HeartRateBox, HeartRateSlider);
        }

        private void HeartRateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(HeartRateBox, HeartRateSlider);
        }

        private void HeartRateBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void RespirationRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(RespirationRateBox, RespirationRateSlider);
        }

        private void RespirationRateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(RespirationRateBox, RespirationRateSlider);
        }

        private void RespirationRateBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void OxygenLevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(OxygenLevelBox, OxygenLevelSlider);
        }

        private void OxygenLevelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(OxygenLevelBox, OxygenLevelSlider);
        }

        private void OxygenLevelBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void BloodPressureSystolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureSystolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureSystolicBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void BloodPressureDiastolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void BloodPressureDiastolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void BloodPressureDiastolicBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TemperatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(TemperatureBox, TemperatureSlider);
        }

        private void TemperatureBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(TemperatureBox, TemperatureSlider);
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

        private void Button_Click_StartStop(object sender, RoutedEventArgs e)
        {
            switch (currentState)
            {
                case SimulationState.Stopped:
                    StartSimulation();
                    break;
                case SimulationState.Running:
                    StopSimulation();
                    StartStopButton.Content = "Continue"; // Änderung des Button-Texts auf "Continue"
                    currentState = SimulationState.Paused; // Zustand auf "Paused" setzen
                    StartStopButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF16FF06");
                    break;
                case SimulationState.Paused:
                    ContinueSimulation();
                    StartStopButton.Content = "Stop"; // Änderung des Button-Texts auf "Stop"
                    currentState = SimulationState.Running; // Zustand auf "Running" setzen
                    StartStopButton.Background = new SolidColorBrush(Colors.Yellow);
                    break;
            }
        }

        private void StartSimulation()
        {
            if (!ValidateMonitorID())
            {
                return;
            }

            MonitorVitalDaten newMonitor = CreateMonitorData();
            mySimulatorTimer.StartSimulator(newMonitor);

            if (currentState != SimulationState.Running) // Nur wenn der Zustand nicht bereits "Running" ist
            {
                currentState = SimulationState.Running; // Zustand auf "Running" setzen, wenn die Simulation gestartet wird
                StartStopButton.Content = "Stop";
                StartStopButton.Background = new SolidColorBrush(Colors.Yellow);
            }
            MessageBox.Show("Erfolgreich einen Monitor erstellt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StopSimulation()
        {
            MqttPublisher.isSendingData = false;
            mySimulatorTimer.ResetTimer();
            MessageBox.Show("Erfolgreich Generierung der Daten gestoppt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ContinueSimulation()
        {
            MqttPublisher.isSendingData = true;
            mySimulatorTimer.ResetTimer();
            MessageBox.Show("Generierung der Daten wird fortgesetzt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private MonitorVitalDaten CreateMonitorData()
        {
            if (isUUIDAlreadyCreated == false)
            {
                UUID = GenerateUUID();
                isUUIDAlreadyCreated = true;
            }
            //UUID = GenerateUUID();

            //Guid newUUID = identifier != Guid.Empty ? identifier : GenerateUUID();
            int HeartRate = Convert.ToInt32(HeartRateSlider.Value);
            int RespirationRate = Convert.ToInt32(RespirationRateSlider.Value);
            int OxygenLevel = Convert.ToInt32(OxygenLevelSlider.Value);
            int BloodPressureSystolic = Convert.ToInt32(BloodPressureSystolicSlider.Value);
            int BloodPressureDiastolic = Convert.ToInt32(BloodPressureDiastolicSlider.Value);
            double Temperature = TemperatureSlider.Value;
            int Alive = 1;

            MonitorVitalDaten newMonitor = new MonitorVitalDaten(
                MonitorIDBox.Text,
                HeartRate,
                RespirationRate,
                OxygenLevel,
                BloodPressureSystolic,
                BloodPressureDiastolic,
                Temperature,
                UUID,
                Alive
                );

            return newMonitor;
        }


        private string GenerateUUID()
        {
            identifier = Guid.NewGuid();
            return identifier.ToString();
        }

        private bool ValidateMonitorID()
        {
            if (string.IsNullOrEmpty(MonitorIDBox.Text) || !int.TryParse(MonitorIDBox.Text, out int monitorID) || monitorID <= 0)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Monitor-ID (positive Zahl) ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            ConfirmClose();
        }

        public void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isAlreadyClosing = true;
            e.Cancel = ConfirmClose();
        }

        private bool ConfirmClose()
        {
            zaehler++;

            if (zaehler == 1 && ConfirmCloseApplication())
            {
                SetAliveStatusToZero();
                if (!isAlreadyClosing)
                {
                    this.Close();
                }
                return false;
            }
            else if (zaehler == 2)
            {
                return false;
            }
            return true;
        }

        private bool ConfirmCloseApplication()
        {
            MessageBoxResult result = MessageBox.Show("Möchten Sie wirklich den Generator schließen?", "Schließen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                zaehler--;
            }

            return result == MessageBoxResult.Yes;
        }

        private void ConfirmChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentState != SimulationState.Stopped) // Überprüfung, ob die Simulation gestartet wurde
            {
                // Logik zur Bestätigung der Änderungen
                // Übernehme die geänderten Slider-Werte und setze sie als aktuelle Werte für die Übertragung oder Simulation
                if (isValueChanged)
                {
                    mySimulatorTimer.ResetTimer();

                    HeartRateSlider.Value = Convert.ToDouble(HeartRateBox.Text);
                    RespirationRateSlider.Value = Convert.ToDouble(RespirationRateBox.Text);
                    OxygenLevelSlider.Value = Convert.ToDouble(OxygenLevelBox.Text);
                    BloodPressureSystolicSlider.Value = Convert.ToDouble(BloodPressureSystolicBox.Text);
                    BloodPressureDiastolicSlider.Value = Convert.ToDouble(BloodPressureDiastolicBox.Text);
                    TemperatureSlider.Value = Convert.ToDouble(TemperatureBox.Text);

                    isValueChanged = false; // Setze isValueChanged zurück
                    ConfirmChangesButton.IsEnabled = false; // Deaktiviere den Bestätigen-Button wieder
                    UpdateVitalData();
                }
            }
            else
            {
                // Gib eine Meldung aus, dass die Simulation gestartet werden muss, um Änderungen zu bestätigen
                MessageBox.Show("Die Simulation muss gestartet werden, um Änderungen bestätigen zu können", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateVitalData()
        {
            MonitorVitalDaten updatedMonitor = CreateMonitorData();
            mySimulatorTimer.StartSimulator(updatedMonitor);
        }

        public void SetAliveStatusToZero()
        {
            // Setze Alive auf 0
            MonitorVitalDaten updatedAliveMonitor = new MonitorVitalDaten(MonitorIDBox.Text, 0, 0, 0, 0, 0, 0, UUID, 0);
            mySimulatorTimer.StartSimulator(updatedAliveMonitor);
        }
    }
}