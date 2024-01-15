using System;
using System.Windows;

namespace VitaldataSimulator
{
    class VitaldataRunSimulator
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