using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
//JSON
using Newtonsoft.Json;

namespace Vitaldatensimulator
{
    class VitaldatenSimulator
    {
        static async Task Main()
        {
            //MQTT Verbindung
            string hostName = "mqtt.inftech.hs-mannheim.de";
            string user = "23pms01";
            string pwd = "c3c242ff";
            string topic = "Vitaldaten";
            int port = 8883;
            MqttPublisher publisher = new MqttPublisher(hostName, port, user, pwd);
            await Task.Delay(1000);
            Console.WriteLine("connected: " + publisher.IsConnected);
            Console.WriteLine();

            // Hinzufügen von Patienten mit dictionary
            //Dictionary<string, Patient> patients = new Dictionary<string, Patient>();
            //patients["Patient1"] = new Patient("Patient1");

            List<PatientVitalDaten> patients = new List<PatientVitalDaten>();

            // Hinzufügen von Patienten
            patients.Add(new PatientVitalDaten("Patient1"));
            patients.Add(new PatientVitalDaten("Patient2"));

            while (true)
            {
                foreach (var patient in patients)
                {
                    patient.GenerateAllVitaldata();
                    Console.WriteLine($"{patient.Name} - Vitaldaten:");
                    Console.WriteLine($"Herzschlag: {patient.HeartRate}");
                    Console.WriteLine($"Atemfrequenz: {patient.RespirationRate}");
                    Console.WriteLine($"Sauerstoffsättigung: {patient.OxygenLevel}");
                    Console.WriteLine($"Systolischer Blutdruck: {patient.BloodPressureSystolic}");
                    Console.WriteLine($"Diastolischer Blutdruck: {patient.BloodPressureDiastolic}");
                    Console.WriteLine();

                    var vitaldaten = new
                    {
                        Name = patient.Name,
                        HeartRate = patient.HeartRate,
                        RespirationRate = patient.RespirationRate,
                        OxygenLevel = patient.OxygenLevel,
                        BloodPressureSystolic = patient.BloodPressureSystolic,
                        BloodPressureDiastolic = patient.BloodPressureDiastolic
                    };

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(vitaldaten);
                    publisher.PublishVitaldata(topic, json);
                }
                // Warten für 1 Sekunde
                await Task.Delay(3000);
            }
        }
    }
}