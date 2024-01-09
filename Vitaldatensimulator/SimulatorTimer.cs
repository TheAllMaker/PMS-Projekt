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
        private VitalData singleMonitor;
        private readonly MqttPublisher mqttPublisher = MqttPublisher.GetInstance();

        public SimulatorTimer()
        {
            //timer = new Timer(1000);
            //timer.Elapsed += OnTimedEvent;
            //timer.AutoReset = true;
            //timer.Enabled = false;
            //timer.Stop();
        }

        public void StartSimulator(VitalData monitor)
        {
            if (timer == null)
            {
                StartTimer();
            }

            singleMonitor = monitor;
            mqttPublisher.SendVitalData(singleMonitor);
        }

        public void StartTimer()
        {
            timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
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
                timer = new Timer(500); // Neue Timer-Instanz erstellen
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
                timer.Enabled = true;
            }
        }

        public void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop(); // Stoppt den Timer
                timer.Elapsed -= OnTimedEvent; // Entfernt das Ereignis
                timer.Dispose(); // Gibt die Ressourcen frei
                timer = null; // Setzt den Timer auf null
            }
        }
    }
}
