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
        public static List<MonitorVitalDaten> Monitor = new List<MonitorVitalDaten>();
        public static bool isSendingData = true;

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            MainCreatePatientWindow mainWindow = new MainCreatePatientWindow();
            app.Run(mainWindow);
        }

        public static void DoMqttAndDataOperations(MonitorVitalDaten newMonitor)
        {
            Monitor.Add(newMonitor);

            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent; 
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (isSendingData)
            {
                var MonitorCopy = new List<MonitorVitalDaten>(Monitor);
                foreach (var monitor in MonitorCopy)
                {
                    monitor.GenerateAllVitaldata();
                    SendVitalData(monitor);
                }
            }
            else
            {
                timer.Stop();
            }
        }

        public static event EventHandler<VitalDataEventArgs> VitalDataUpdated;

        // Anstatt var vitaldaten direkt monitor per MQTT versenden
        public static void SendVitalData(MonitorVitalDaten monitor)
        {
            var vitaldaten = new
            {
                monitor.MonitorID,
                monitor.HeartRate,
                monitor.RespirationRate,
                monitor.OxygenLevel,
                monitor.BloodPressureSystolic,
                monitor.BloodPressureDiastolic,
                monitor.Temperature
            };
            VitalDataUpdated?.Invoke(null, new VitalDataEventArgs(monitor.HeartRate, monitor.RespirationRate, monitor.OxygenLevel, monitor.BloodPressureSystolic, monitor.BloodPressureDiastolic, monitor.Temperature));
            Mqtt(vitaldaten);
        }

        public static void Mqtt(object vitaldaten)
        {
            MqttPublisher publisher = new MqttPublisher();
           // Console.WriteLine("connected: " + publisher.IsConnected);
            Console.WriteLine();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(vitaldaten);
            publisher.PublishVitaldataJSON(json);
        }
    }
}