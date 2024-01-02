using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;

namespace Vitaldatensimulator
{
    class VitaldatenSimulator
    {
        private static Timer timer;
        private static MonitorVitalDaten singleMonitor;
        public static bool isSendingData = true;
        public static event EventHandler<MonitorVitalDaten> VitalDataUpdated;
        private static MqttPublisher mqttPublisher = new MqttPublisher();

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            SimulatorUI simulator = new SimulatorUI();
            app.Run(simulator);

            //MainMenu mainMenu = new MainMenu();
            //app.Run(mainMenu);
        }

        public static void DoMqttAndDataOperations(MonitorVitalDaten newMonitor)
        {
            singleMonitor = newMonitor;
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (isSendingData)
            {
                singleMonitor.GenerateAllVitaldata();
                SendVitalData(singleMonitor);
            }
        }

        public static void SendVitalData(MonitorVitalDaten singleMonitor)
        {
            var vitaldaten = singleMonitor.GetVitalData();
            if (singleMonitor.Alive == 0)
            {
                mqttPublisher.PublishVitaldataJSON(vitaldaten);
                isSendingData = false;
            }
            else
            {
                VitalDataUpdated?.Invoke(null, singleMonitor);
                mqttPublisher.PublishVitaldataJSON(vitaldaten);
            }
        }
        public static void ResetTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}