using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VitaldataSimulator.Model
{
    public partial class SimulatorUI
    {
        private enum SimulationState
        {
            Stopped,
            Running,
            Paused
        }

        private SimulationState _currentState = SimulationState.Stopped;
        private readonly Dictionary<Slider, double> _originalSliderValues = new Dictionary<Slider, double>();
        private SimulatorTimer _mySimulatorTimer;
        private PowerWindow _powerWindow;
        private bool _isValueChanged;
        private bool _isUuidAlreadyCreated;
        private Guid _identifier;
        private string _uuid;

        public SimulatorUI()
        {
            InitializeComponent();
            Loaded += SimulatorUI_Loaded;
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            this.Closing += SimulatorUI_Closing;
            MqttPublisher.UpdateSendVitaldata += UpdateSendVitaldata_UI;
            _mySimulatorTimer = new SimulatorTimer();
        }

        private void SimulatorUI_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSliderValues();
            InitializeSliderOriginalValues();
            ConfirmChangesButton.IsEnabled = false;
        }

        //Updaten der Anzeige der aktuell versendeten Vitalwerte
        private void UpdateSendVitaldata_UI(object sender, VitalData monitorVitalDaten)
        {
            if (monitorVitalDaten != null)
            {
                Dispatcher.Invoke(() =>
                {
                    HeartRateValueTextBlock.Text = monitorVitalDaten.HeartRate.ToString();
                    RespirationRateValueTextBlock.Text = Math.Round(monitorVitalDaten.RespirationRate, MidpointRounding.AwayFromZero).ToString();
                    OxygenLevelValueTextBlock.Text = Math.Round(monitorVitalDaten.OxygenLevel, MidpointRounding.AwayFromZero).ToString();
                    BloodPressureSystolicValueTextBlock.Text = monitorVitalDaten.BloodPressureSystolic.ToString();
                    BloodPressureDiastolicValueTextBlock.Text = monitorVitalDaten.BloodPressureDiastolic.ToString();
                    TemperatureValueTextBlock.Text = Math.Round(monitorVitalDaten.Temperature, 1).ToString("0.0");
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
            _originalSliderValues.Add(HeartRateSlider, HeartRateSlider.Value);
            _originalSliderValues.Add(RespirationRateSlider, RespirationRateSlider.Value);
            _originalSliderValues.Add(OxygenLevelSlider, OxygenLevelSlider.Value);
            _originalSliderValues.Add(BloodPressureSystolicSlider, BloodPressureSystolicSlider.Value);
            _originalSliderValues.Add(BloodPressureDiastolicSlider, BloodPressureDiastolicSlider.Value);
            _originalSliderValues.Add(TemperatureSlider, TemperatureSlider.Value);

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
            if (sender is Slider slider && _currentState != SimulationState.Stopped && _originalSliderValues.ContainsKey(slider) && slider.Value != _originalSliderValues[slider])
            {
                double previousValue = _originalSliderValues[slider];
                double currentValue = slider.Value;

                if (currentValue != previousValue)
                {
                    _originalSliderValues[slider] = currentValue;
                    _isValueChanged = true;
                    ConfirmChangesButton.IsEnabled = true;
                }
            }
        }

        //Slider werden beim Starten auf die mitte gestellt
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

        private void RespirationRateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(RespirationRateBox, RespirationRateSlider);
        }

        private void RespirationRateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(RespirationRateBox, RespirationRateSlider);
        }
        private void OxygenLevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(OxygenLevelBox, OxygenLevelSlider);
        }

        private void OxygenLevelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(OxygenLevelBox, OxygenLevelSlider);
        }

        private void BloodPressureSystolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureSystolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(BloodPressureSystolicBox, BloodPressureSystolicSlider);
        }

        private void BloodPressureDiastolicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void BloodPressureDiastolicBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(BloodPressureDiastolicBox, BloodPressureDiastolicSlider);
        }

        private void TemperatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSliderAndTextBox(TemperatureBox, TemperatureSlider);
        }

        private void TemperatureBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderAndTextBox(TemperatureBox, TemperatureSlider);
        }

        //Methode zum Wechseln zwischen den einzelnen Zuständen
        private void Button_Click_StartStop(object sender, RoutedEventArgs e)
        {
            switch (_currentState)
            {
                case SimulationState.Stopped:
                    StartSimulation();
                    break;
                case SimulationState.Running:
                    StopSimulation();
                    StartStopButton.Content = "Continue";
                    _currentState = SimulationState.Paused;
                    StartStopButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF16FF06");
                    break;
                case SimulationState.Paused:
                    ContinueSimulation();
                    StartStopButton.Content = "Stop";
                    _currentState = SimulationState.Running;
                    StartStopButton.Background = new SolidColorBrush(Colors.Yellow);
                    break;
            }
        }

        //Starten der PowerWindow UI um es schließen zu können
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            if (_powerWindow != null && _powerWindow.IsVisible) return;
            _powerWindow = new PowerWindow();
            _powerWindow.ConfirmClicked += PowerWindow_ConfirmClicked;
            _powerWindow.Show();
        }


        //Logik zur Änderung der versendeten Werte anhand der Slider
        private void Button_Click_ConfirmChanges(object sender, RoutedEventArgs e)
        {
            if (_currentState != SimulationState.Stopped) // Überprüfung, ob die Simulation gestartet wurde
            {
                if (_isValueChanged)
                {
                    HeartRateSlider.Value = Convert.ToDouble(HeartRateBox.Text);
                    RespirationRateSlider.Value = Convert.ToDouble(RespirationRateBox.Text);
                    OxygenLevelSlider.Value = Convert.ToDouble(OxygenLevelBox.Text);
                    BloodPressureSystolicSlider.Value = Convert.ToDouble(BloodPressureSystolicBox.Text);
                    BloodPressureDiastolicSlider.Value = Convert.ToDouble(BloodPressureDiastolicBox.Text);
                    TemperatureSlider.Value = Convert.ToDouble(TemperatureBox.Text);

                    _isValueChanged = false;
                    ConfirmChangesButton.IsEnabled = false;
                    UpdateVitalData();
                }
            }
        }

        //Starten der Simulation und erstellen des Monitorbjektes mit Vitaldaten
        private void StartSimulation()
        {
            if (!ValidateMonitorID()) return;

            VitalData newMonitor = CreateMonitorData();
            _mySimulatorTimer.StartSimulatorTimer(newMonitor);

            MonitorIdBox.IsEnabled = false;

            if (_currentState != SimulationState.Running)
            {
                StartStopButton.Content = "Stop";
                StartStopButton.Background = new SolidColorBrush(Colors.Yellow);
                _currentState = SimulationState.Running;
            }
        }

        //Simulation stoppen
        private void StopSimulation()
        {
            MqttPublisher.IsSendingData = false;
            _mySimulatorTimer.StopTimer();
        }

        //Simulation fortsetzen
        private void ContinueSimulation()
        {
            MqttPublisher.IsSendingData = true;
            _mySimulatorTimer.StartTimer();
        }

        //Erstellen von einem Monitor Objekt mit Vitaldaten
        private VitalData CreateMonitorData()
        {
            if (!_isUuidAlreadyCreated)
            {
                _uuid = GenerateUUID();
                _isUuidAlreadyCreated = true;
            }

            int heartRate = Convert.ToInt32(HeartRateSlider.Value);
            int bloodPressureSystolic = Convert.ToInt32(BloodPressureSystolicSlider.Value);
            int bloodPressureDiastolic = Convert.ToInt32(BloodPressureDiastolicSlider.Value);
            double temperature = TemperatureSlider.Value;
            double respirationRate = RespirationRateSlider.Value;
            double oxygenLevel = OxygenLevelSlider.Value;

            VitalData newMonitor = new VitalData(
                MonitorIdBox.Text,
                heartRate,
                respirationRate,
                oxygenLevel,
                bloodPressureSystolic,
                bloodPressureDiastolic,
                temperature,
                _uuid
                );

            return newMonitor;
        }

        //Aktualisiere die versendeten Vitaldaten nach der Änderung
        private void UpdateVitalData()
        {
            VitalData updatedMonitor = CreateMonitorData();
            _mySimulatorTimer.StartSimulatorTimer(updatedMonitor);
        }

        //Generiere eine UUID zur eindeutigen Identifizierung des
        private string GenerateUUID()
        {
            _identifier = Guid.NewGuid();
            return _identifier.ToString();
        }

        //Kontrolle, dass die MonitorID nur eine Zahl ist
        private bool ValidateMonitorID()
        {
            if (string.IsNullOrEmpty(MonitorIdBox.Text) || !int.TryParse(MonitorIdBox.Text, out int monitorID) || monitorID <= 0)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Monitor-ID (positive Zahl) ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        //Falls das fenster über "x" Knopf geschlossen wird soll trotzdem das PowerWindow gestartet werden
        public void SimulatorUI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_powerWindow == null || !_powerWindow.IsVisible)
            {
                e.Cancel = true;
                _powerWindow = new PowerWindow();
                _powerWindow.ConfirmClicked += PowerWindow_ConfirmClicked;
                _powerWindow.Show();
            }
        }

        //Schließen des Projekts nach 100ms, damit letzte Nachricht noch versendet werden kann
        private async void PowerWindow_ConfirmClicked(object sender, EventArgs e)
        {
            if (_currentState != SimulationState.Stopped)
            {
                SetAliveStatusToZero();
            }
            await Task.Delay(500);
            _mySimulatorTimer.StopTimer();
            Application.Current.Shutdown();
        }

        // Schicke als letzte Nachricht noch Alive = 0
        public void SetAliveStatusToZero()
        {
            VitalData killMonitor = new VitalData(MonitorIdBox.Text, 0, 0, 0, 0, 0, 0, _uuid, 0);
            _mySimulatorTimer.StartSimulatorTimer(killMonitor);
        }
    }
}