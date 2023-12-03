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
            MainCreatePatientWindow mainWindow = new MainCreatePatientWindow();
            app.Run(mainWindow);
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

        // Anstatt var vitaldaten direkt monitor per MQTT versenden
        // DATENBANK ABFRAGE OB ID SCHON VORHANDEN IST
        public static void SendVitalData(MonitorVitalDaten monitor)
        {
            var vitaldaten = monitor.GetVitalData();
            //VitalDataUpdated?.Invoke(null, new VitalDataEventArgs(monitor.HeartRate, monitor.RespirationRate, monitor.OxygenLevel, monitor.BloodPressureSystolic, monitor.BloodPressureDiastolic, monitor.Temperature));
            VitalDataUpdated?.Invoke(null, monitor);
            mqttPublisher.PublishVitaldataJSON(vitaldaten);
        }
    }
}