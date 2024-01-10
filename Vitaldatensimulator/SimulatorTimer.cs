using System;
using System.Timers;


namespace Vitaldatensimulator
{
    public class SimulatorTimer
    {
        private Timer _timer;
        private VitalData _singleMonitor;
        private readonly MqttPublisher _mqttPublisher = MqttPublisher.GetInstance();

        public void StartSimulator(VitalData monitor)
        {
            if (_timer == null)
            {
                StartTimer();
            }

            _singleMonitor = monitor;
            //Kann eingebaut werden wenn man die Daten direkt beim Start senden will
            //_mqttPublisher.SendVitalData(_singleMonitor);
        }

        public void StartTimer()
        {
            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (!MqttPublisher.IsSendingData) return;
            _singleMonitor.GenerateAllVitaldata();
            _mqttPublisher.SendVitalData(_singleMonitor);
        }

        public void ResetTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = new Timer(500); // Neue Timer-Instanz erstellen
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
                _timer.Enabled = true;
            }
        }

        public void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop(); // Stoppt den Timer
                _timer.Elapsed -= OnTimedEvent; // Entfernt das Ereignis
                _timer.Dispose(); // Gibt die Ressourcen frei
                _timer = null; // Setzt den Timer auf null
            }
        }
    }
}
