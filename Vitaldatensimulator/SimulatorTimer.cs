using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Vitaldatensimulator
{
    internal class SimulatorTimer
    {
        public static Timer timer;

        public SimulatorTimer(int duration) 
        {
            timer = new Timer(duration);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (VitaldatenSimulator.isSendingData)
            {
                singleMonitor.GenerateAllVitaldata();
                SendVitalData(singleMonitor);
            }
        }

        public static void ResetTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }

    }
}
