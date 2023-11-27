using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
//JSON
using Newtonsoft.Json;

namespace Vitaldatensimulator
{
    class VitaldatenSimulator
    {

        [STAThread]
        static void Main()
        {
            Application app = new Application();
            MainCreatePatientWindow mainWindow = new MainCreatePatientWindow(); // Erstelle die Instanz des MainWindow
            app.Run(mainWindow);

            // Andere Funktionen wie MQTT-Verbindung und Datenaktualisierung
            //DoMqttAndDataOperations();
        }

        public static void DoMqttAndDataOperations(string MonitorID, double HeartRate, double RespirationRate, double OxygenLevel, double BloodPressureSystolic, double BloodPressureDiastolic)
        {
            //MQTT Verbindung
            string hostName = "mqtt.inftech.hs-mannheim.de";
            string user = "23pms01";
            string pwd = "c3c242ff";
            string topic = "23pms01/test";
            int port = 8883;
            MqttPublisher publisher = new MqttPublisher(hostName, port, user, pwd);
            Console.WriteLine("connected: " + publisher.IsConnected);
            Console.WriteLine();

            // Hinzufügen von Patienten mit dictionary
            //Dictionary<string, Patient> patients = new Dictionary<string, Patient>();
            //patients["Patient1"] = new Patient("Patient1");

            List<PatientVitalDaten> patients = new List<PatientVitalDaten>();

            // Hinzufügen von Patienten
            patients.Add(new PatientVitalDaten(MonitorID));

            while (true)
            {
                foreach (var patient in patients)
                {
                    //patient.GenerateAllVitaldata();

                    var vitaldaten = new
                    {
                        MonitorID = patient.MonitorID,
                        HeartRate = patient.HeartRate,
                        RespirationRate = patient.RespirationRate,
                        OxygenLevel = patient.OxygenLevel,
                        BloodPressureSystolic = patient.BloodPressureSystolic,
                        BloodPressureDiastolic = patient.BloodPressureDiastolic
                    };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(vitaldaten);
                    publisher.PublishVitaldataJSON(topic, json);
                }
                // Warten für 3 Sekunde
                //await Task.Delay(3000);
            }
        }

    }
}


//Console.WriteLine($"{patient.MonitorID} - Vitaldaten:");
//Console.WriteLine($"Herzschlag: {patient.HeartRate}");
//Console.WriteLine($"Atemfrequenz: {patient.RespirationRate}");
//Console.WriteLine($"Sauerstoffsättigung: {patient.OxygenLevel}");
//Console.WriteLine($"Systolischer Blutdruck: {patient.BloodPressureSystolic}");
//Console.WriteLine($"Diastolischer Blutdruck: {patient.BloodPressureDiastolic}");
//Console.WriteLine();