using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// mqtt
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

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
            Console.WriteLine("connected: " + publisher.IsConnected);

            // Hinzufügen von Patienten mit dictionary
            //Dictionary<string, Patient> patients = new Dictionary<string, Patient>();
            //patients["Patient1"] = new Patient("Patient1");

            List<Patient> patients = new List<Patient>();

            // Hinzufügen von Patienten
            patients.Add(new Patient("Patient1"));
            patients.Add(new Patient("Patient2"));


            await Task.Delay(5000);

            while (true)
            {
                foreach (var patient in patients)
                {
                    patient.GeneriereAlleVitaldaten();
                    Console.WriteLine($"{patient.Name} - Vitaldaten:");
                    Console.WriteLine($"Herzschlag: {patient.Herzschlag}");
                    Console.WriteLine($"Atemfrequenz: {patient.Atemfrequenz}");
                    Console.WriteLine($"Sauerstoffsättigung: {patient.Sauerstoffsättigung}");
                    Console.WriteLine($"Systolischer Blutdruck: {patient.SystolischerBlutdruck}");
                    Console.WriteLine($"Diastolischer Blutdruck: {patient.DiastolischerBlutdruck}");
                }

               // Warten für 1 Sekunde
               await Task.Delay(5000);
            }
            //publisher.Disconnect();
        }
    }
}
