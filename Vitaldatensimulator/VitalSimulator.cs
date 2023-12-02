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
        public static event EventHandler<VitalDataEventArgs> VitalDataUpdated;

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
            //else
            //{
                //timer.Stop();
            //}
        }

        // Anstatt var vitaldaten direkt monitor per MQTT versenden
        // DATENBANK ABFRAGE OB ID SCHON VORHANDEN IST
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