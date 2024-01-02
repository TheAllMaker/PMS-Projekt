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
        private MqttPublisher publisher;

        public SimulatorTimer() 
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
            //singleMonitor = monitor;
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
                publisher.SendVitalData(singleMonitor);
            }
        }

        public void ResetTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }

    }
}
