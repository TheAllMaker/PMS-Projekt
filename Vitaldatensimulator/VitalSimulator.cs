using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
//JSON
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

        public static void DoMqttAndDataOperations(string MonitorID, double HeartRate, double RespirationRate, double OxygenLevel, double BloodPressureSystolic, double BloodPressureDiastolic)
        {
            PatientVitalDaten newPatient = new PatientVitalDaten(MonitorID, HeartRate, RespirationRate, OxygenLevel, BloodPressureSystolic, BloodPressureDiastolic);
            newPatient.GenerateAllVitaldata();
            patients.Add(newPatient);
            Console.WriteLine("HeartRate: " + newPatient.HeartRate);
            Console.WriteLine("RespirationRate: " + newPatient.RespirationRate);
            Console.WriteLine("OxygenLevel: " + newPatient.OxygenLevel);
            Console.WriteLine("BloodPressureSystolic: " + newPatient.BloodPressureSystolic);
            Console.WriteLine("BloodPressureDiastolic: " + newPatient.BloodPressureDiastolic);
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
            foreach (var patient in patients)
            {
                patient.GenerateAllVitaldata();
                Console.WriteLine("Patient - MonitorID: " + patient.MonitorID);
                Console.WriteLine("HeartRate: " + patient.HeartRate);
                Console.WriteLine("RespirationRate: " + patient.RespirationRate);
                Console.WriteLine("OxygenLevel: " + patient.OxygenLevel);
                Console.WriteLine("BloodPressureSystolic: " + patient.BloodPressureSystolic);
                Console.WriteLine("BloodPressureDiastolic: " + patient.BloodPressureDiastolic);
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
                patient.BloodPressureDiastolic
            };
            Mqtt(vitaldaten);
        }

        public static void Mqtt(object vitaldaten)
        {
            MqttPublisher publisher = new MqttPublisher();
            Console.WriteLine("connected: " + publisher.IsConnected);
            Console.WriteLine();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(vitaldaten);
            publisher.PublishVitaldataJSON(json);
        }
    }
}