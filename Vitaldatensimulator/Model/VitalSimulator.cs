using System;
using System.Windows;

namespace VitaldataSimulator.Model
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