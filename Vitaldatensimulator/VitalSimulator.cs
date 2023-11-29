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
        public static List<PatientVitalDaten> patients = new List<PatientVitalDaten>();

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            MainCreatePatientWindow mainWindow = new MainCreatePatientWindow();
            app.Run(mainWindow);
        }

        public static void DoMqttAndDataOperations(string MonitorID, double HeartRate, double RespirationRate, double OxygenLevel, double BloodPressureSystolic, double BloodPressureDiastolic, double Temperature)
        {
            PatientVitalDaten newPatient = new PatientVitalDaten(MonitorID, HeartRate, RespirationRate, OxygenLevel, BloodPressureSystolic, BloodPressureDiastolic, Temperature);
            newPatient.GenerateAllVitaldata();
            patients.Add(newPatient);
            SendVitalData(newPatient);

            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent; 
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timer.Stop();
            var patientsCopy = new List<PatientVitalDaten>(patients);
            foreach (var patient in patientsCopy)
            {
                patient.GenerateAllVitaldata();
                SendVitalData(patient);
            }
            timer.Start();
        }

        public static void SendVitalData(PatientVitalDaten patient)
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