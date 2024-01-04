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
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            SimulatorUI simulator = new SimulatorUI();
            app.Run(simulator);
        }
    }
}