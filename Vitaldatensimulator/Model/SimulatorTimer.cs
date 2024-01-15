using System;
using System.Timers;


namespace Vitaldatensimulator
{
    public class SimulatorTimer
    {
        private Timer _timer;
        private const int _timerIntervalMilliseconds = 750;
        private VitalData _currentMonitor;
        private readonly MqttPublisher _mqttPublisher = MqttPublisher.GetInstance();

        public void StartSimulator(VitalData monitor)
        {
            if (_timer == null)
            {
                StartTimer();
            }

            _currentMonitor = monitor;
            //Kann eingebaut werden wenn man die Daten direkt beim Start senden will
            //_mqttPublisher.SendVitalData(_singleMonitor);
        }

        public void StartTimer()
        {
            _timer = new Timer(_timerIntervalMilliseconds);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (!MqttPublisher.IsSendingData) return;
            _currentMonitor.GenerateAllVitaldata();
            _mqttPublisher.SendVitalData(_currentMonitor);
        }

        public void ResetTimer()
        {
            if (_timer == null) return;
            _timer.Stop();
            _timer.Dispose();
            _timer = new Timer(_timerIntervalMilliseconds);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            if (_timer == null) return;
            _timer.Stop();
            _timer.Elapsed -= OnTimedEvent;
            _timer.Dispose();
            _timer = null;
        }
    }
}
