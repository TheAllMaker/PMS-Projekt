using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Vitaldatensimulator
{
    public class SimulatorTimer
    {
        private Timer timer;
        private MonitorVitalDaten singleMonitor;
        private MqttPublisher mqttPublisher = new MqttPublisher();

        public SimulatorTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
        }

        public void StartSimulator(MonitorVitalDaten monitor)
        {
            singleMonitor = monitor;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (MqttPublisher.isSendingData)
            {
                singleMonitor.GenerateAllVitaldata();
                mqttPublisher.SendVitalData(singleMonitor);
            }
        }

        public void ResetTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = new Timer(1000); // Neue Timer-Instanz erstellen
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
            }
        }
    }
}
