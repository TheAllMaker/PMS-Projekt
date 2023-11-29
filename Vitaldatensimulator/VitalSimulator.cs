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
            MainCreatePatientWindow mainWindow = new MainCreatePatientWindow();
            app.Run(mainWindow);
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

            PatientVitalDaten patient = new PatientVitalDaten(MonitorID, HeartRate, RespirationRate, OxygenLevel, BloodPressureSystolic, BloodPressureDiastolic);
            var vitaldaten = new
            {
                patient.MonitorID,
                patient.HeartRate,
                patient.RespirationRate,
                patient.OxygenLevel,
                patient.BloodPressureSystolic,
                patient.BloodPressureDiastolic
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(vitaldaten);
            publisher.PublishVitaldataJSON(topic, json);

        }

    }
}