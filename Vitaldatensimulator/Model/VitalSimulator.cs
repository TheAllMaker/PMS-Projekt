using System;
using System.Windows;

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