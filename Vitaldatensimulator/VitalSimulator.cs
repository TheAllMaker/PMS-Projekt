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
    public class VitalDataEventArgs : EventArgs
    {
        public int HeartRate { get; }
        public int RespirationRate { get; }
        public int OxygenLevel { get; }
        public int BloodPressureSystolic { get; }
        public int BloodPressureDiastolic { get; }
        public double Temperature { get; }

        public VitalDataEventArgs(int HeartRate, int RespirationRate, int OxygenLevel, int BloodPressureSystolic, int BloodPressureDiastolic, double Temperature)
        {
            this.HeartRate = HeartRate;
            this.RespirationRate = RespirationRate;
            this.OxygenLevel = OxygenLevel;
            this.BloodPressureSystolic = BloodPressureSystolic;
            this.BloodPressureDiastolic = BloodPressureDiastolic;
            this.Temperature = Temperature;
        }
    }

    class VitaldatenSimulator
    {
        private static Timer timer;
        public static List<MonitorVitalDaten> patients = new List<MonitorVitalDaten>();

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            MainCreatePatientWindow mainWindow = new MainCreatePatientWindow();
            app.Run(mainWindow);
        }

        public static void DoMqttAndDataOperations(MonitorVitalDaten newMonitor)
        {
            newMonitor.GenerateAllVitaldata();
            patients.Add(newMonitor);
            SendVitalData(newMonitor);

            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent; 
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timer.Stop();
            var patientsCopy = new List<MonitorVitalDaten>(patients);
            foreach (var patient in patientsCopy)
            {
                patient.GenerateAllVitaldata();
                SendVitalData(patient);
            }
            timer.Start();
        }

        public static event EventHandler<VitalDataEventArgs> VitalDataUpdated;

        public static void SendVitalData(MonitorVitalDaten patient)
        {
            var vitaldaten = new
            {
                patient.MonitorID,
                patient.HeartRate,
                patient.RespirationRate,
                patient.OxygenLevel,
                patient.BloodPressureSystolic,
                patient.BloodPressureDiastolic,
                patient.Temperature
            };
            VitalDataUpdated?.Invoke(null, new VitalDataEventArgs(patient.HeartRate, patient.RespirationRate, patient.OxygenLevel, patient.BloodPressureSystolic, patient.BloodPressureDiastolic, patient.Temperature));
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