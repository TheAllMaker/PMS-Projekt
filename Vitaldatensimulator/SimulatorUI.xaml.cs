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
using MediTrack.View.RemoteView;

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
        private PowerWindow powerWindow;
        private bool isValueChanged;
        private bool isUUIDAlreadyCreated;
        private Guid identifier;
        private string UUID;

        public SimulatorUI()
        {
            InitializeComponent();
            Loaded += SimulatorUI_Loaded;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            this.Closing += SimulatorUI_Closing;
            MqttPublisher.VitalDataUpdated += VitaldatenSimulator_VitalDataUpdated;
            mySimulatorTimer = new SimulatorTimer();
        }

        // Nur als Notfall
        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            SetAliveStatusToZero();
        }

        private void SimulatorUI_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSliderValues();
            InitializeSliderOriginalValues();
            ConfirmChangesButton.IsEnabled = false;
        }

        //Updaten der Anzeige der aktuell versendeten Vitalwerte
        private void VitaldatenSimulator_VitalDataUpdated(object sender, VitalData MonitorVitalDaten)
        {
            if (MonitorVitalDaten != null)
            {
                Dispatcher.Invoke(() =>
                {
                    HeartRateValueTextBlock.Text = MonitorVitalDaten.HeartRate.ToString();
                    RespirationRateValueTextBlock.Text = Math.Round(MonitorVitalDaten.RespirationRate, MidpointRounding.AwayFromZero).ToString();
                    OxygenLevelValueTextBlock.Text = Math.Round(MonitorVitalDaten.OxygenLevel, MidpointRounding.AwayFromZero).ToString();
                    BloodPressureSystolicValueTextBlock.Text = MonitorVitalDaten.BloodPressureSystolic.ToString();
                    BloodPressureDiastolicValueTextBlock.Text = MonitorVitalDaten.BloodPressureDiastolic.ToString();
                    double value = Math.Round(MonitorVitalDaten.Temperature, 1);
                    TemperatureValueTextBlock.Text = Math.Round(MonitorVitalDaten.Temperature, 1).ToString("0.0");
                });
            }
        }

        //Initialisierung der Slider auf den mittleren Wert
        private void InitializeSliderValues()
        {
            SetSliderToMiddleValue(HeartRateSlider);
            SetSliderToMiddleValue(RespirationRateSlider);
            SetSliderToMiddleValue(OxygenLevelSlider);
            SetSliderToMiddleValue(BloodPressureSystolicSlider);
            SetSliderToMiddleValue(BloodPressureDiastolicSlider);
            SetSliderToMiddleValue(TemperatureSlider);
        }

        //Abspeichern der Originalen Slider werte zum Vergleich
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

        //Kontrolle ob Slider geändert wurden für den Änderungs button
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if (currentState != SimulationState.Stopped && originalSliderValues.ContainsKey(slider) && slider.Value != originalSliderValues[slider])
            {
                double previousValue = originalSliderValues[slider];
                double currentValue = slider.Value;

                if (currentValue != previousValue)
                {
                    originalSliderValues[slider] = currentValue;
                    isValueChanged = true;
                    // Aktiviere den Bestätigen-Button
                    ConfirmChangesButton.IsEnabled = true;
                }
            }
        }

        //Slider werden beim Startup auf die mitte gestellt
        private void SetSliderToMiddleValue(Slider slider)
        {
            if (slider != null)
            {
                slider.Value = Math.Round((slider.Minimum + slider.Maximum) / 2);
            }
        }

        //Updaten der Slider und Textboxen
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

        //Methodenaufrufe für Slider und Textboxen zum Updaten der Slider/Textboxen
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

        //Kontrolle, dass nur Zahlen eingefügt werden können
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // Erlaubt nur Zahlen
            return !regex.IsMatch(text);
        }

        //Methode zum Wechseln zwischen den einzelnen Zuständen
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

        //Starten der Simulation und erstellen des Monitor Objektes mit Vitaldaten
        private void StartSimulation()
        {
            if (!ValidateMonitorID()) return;

            VitalData newMonitor = CreateMonitorData();
            mySimulatorTimer.StartSimulator(newMonitor);

            if (currentState != SimulationState.Running)
            {
                currentState = SimulationState.Running;
            }
            MessageBox.Show("Erfolgreich einen Monitor erstellt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Simulation stoppen
        private void StopSimulation()
        {
            MqttPublisher.isSendingData = false;
            mySimulatorTimer.ResetTimer();
            MessageBox.Show("Erfolgreich Generierung der Daten gestoppt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Simulation fortsetzen
        private void ContinueSimulation()
        {
            MqttPublisher.isSendingData = true;
            mySimulatorTimer.ResetTimer();
            MessageBox.Show("Generierung der Daten wird fortgesetzt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Érstellen von einem Monitor Objekt mit Vitaldaten
        private VitalData CreateMonitorData()
        {
            if (!isUUIDAlreadyCreated)
            {
                UUID = GenerateUUID();
                isUUIDAlreadyCreated = true;
            }

            int HeartRate = Convert.ToInt32(HeartRateSlider.Value);
            int BloodPressureSystolic = Convert.ToInt32(BloodPressureSystolicSlider.Value);
            int BloodPressureDiastolic = Convert.ToInt32(BloodPressureDiastolicSlider.Value);
            double Temperature = TemperatureSlider.Value;
            double RespirationRate = RespirationRateSlider.Value;
            double OxygenLevel = OxygenLevelSlider.Value;

            VitalData newMonitor = new VitalData(
                MonitorIDBox.Text,
                HeartRate,
                RespirationRate,
                OxygenLevel,
                BloodPressureSystolic,
                BloodPressureDiastolic,
                Temperature,
                UUID
                );

            return newMonitor;
        }

        //Generiere eine UUID
        private string GenerateUUID()
        {
            identifier = Guid.NewGuid();
            return identifier.ToString();
        }

        //Kontrolle, dass die MonitorID nur eine Zahl ist
        private bool ValidateMonitorID()
        {
            if (string.IsNullOrEmpty(MonitorIDBox.Text) || !int.TryParse(MonitorIDBox.Text, out int monitorID) || monitorID <= 0)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Monitor-ID (positive Zahl) ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        //Starten der PowerWindow UI um es schließen zu können
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            if (powerWindow == null || !powerWindow.IsVisible)
            {
                powerWindow = new PowerWindow();
                powerWindow.ConfirmClicked += PowerWindow_ConfirmClicked;
                powerWindow.Show();

            }
        }

        //Falls das fenster über "x" Knopf geschlossen wird soll trotzdem das PowerWindow gestartet werden
        public void SimulatorUI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (powerWindow == null || !powerWindow.IsVisible)
            {
                e.Cancel = true;
                powerWindow = new PowerWindow();
                powerWindow.ConfirmClicked += PowerWindow_ConfirmClicked;
                powerWindow.Show();
            }
        }

        //Schließen des Projekts nach 100ms, damit letzte Nachricht noch versendet werden kann
        private async void PowerWindow_ConfirmClicked(object sender, EventArgs e)
        {
            if (currentState == SimulationState.Running)
            {
                SetAliveStatusToZero();
            }
            await Task.Delay(300);
            mySimulatorTimer.StopTimer();
            Application.Current.Shutdown();
        }

        //Logik zur Änderung der versendeten Werte anhand der Slider
        private void Button_Click_ConfirmChanges(object sender, RoutedEventArgs e)
        {
            if (currentState != SimulationState.Stopped) // Überprüfung, ob die Simulation gestartet wurde
            {
                if (isValueChanged)
                {
                    HeartRateSlider.Value = Convert.ToDouble(HeartRateBox.Text);
                    RespirationRateSlider.Value = Convert.ToDouble(RespirationRateBox.Text);
                    OxygenLevelSlider.Value = Convert.ToDouble(OxygenLevelBox.Text);
                    BloodPressureSystolicSlider.Value = Convert.ToDouble(BloodPressureSystolicBox.Text);
                    BloodPressureDiastolicSlider.Value = Convert.ToDouble(BloodPressureDiastolicBox.Text);
                    TemperatureSlider.Value = Convert.ToDouble(TemperatureBox.Text);

                    isValueChanged = false;
                    ConfirmChangesButton.IsEnabled = false;
                    mySimulatorTimer.ResetTimer();
                    UpdateVitalData();
                }
            }
            else
            {
                // Gib eine Meldung aus, dass die Simulation gestartet werden muss, um Änderungen zu bestätigen
                MessageBox.Show("Die Simulation muss gestartet werden, um Änderungen bestätigen zu können", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Update die versendeten Vitaldaten nach der Änderung
        private void UpdateVitalData()
        {
            VitalData updatedMonitor = CreateMonitorData();
            mySimulatorTimer.StartSimulator(updatedMonitor);
        }

        // Schicke als letzte Nachricht noch Alive = 0
        public void SetAliveStatusToZero()
        {
            VitalData updatedAliveMonitor = new VitalData(MonitorIDBox.Text, 0, 0, 0, 0, 0, 0, UUID, 0);
            mySimulatorTimer.StartSimulator(updatedAliveMonitor);
        }
    }
}