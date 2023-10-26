using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitaldaten_Simulator
{
    internal class Vitaldaten
    {
        static void Main(string[] args)
        {


            Random r = new Random();
            int Herzschlag = r.Next(60, 80);
            Console.WriteLine("Herzschlag Test:" + Herzschlag);
            Console.ReadLine();

            Console.WriteLine("Verbinde mit MQTT");
            string hostname = "mqtt.inftech.hs-mannheim.de";
            int port = 8883
            // Erstellen Sie eine Instanz der Mqtt-Klasse
            Mqtt Client = new Mqtt(hostname, port, "MyClient");

            //Verbinden
            string user = "23pms01";
            string pwd = "c3c242ff";
            Client.Connect(Guid.NewGuid().ToString,user, pwd);

        }
    }
}
