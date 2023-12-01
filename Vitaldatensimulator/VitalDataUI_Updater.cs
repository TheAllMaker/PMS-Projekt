using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitaldatensimulator
{
    public class VitalDataEventArgs : EventArgs
    {
        public int HeartRate { get; }
        public int RespirationRate { get; }
        public int OxygenLevel { get; }
        public int BloodPressureSystolic { get; }
        public int BloodPressureDiastolic { get; }
        public double Temperature { get; }

        public VitalDataEventArgs(int HeartRate, int RespirationRate, int OxygenLevel, int BloodPressureSystolic, int BloodPressureDiastolic, double Temperature)
        {
            this.HeartRate = HeartRate;
            this.RespirationRate = RespirationRate;
            this.OxygenLevel = OxygenLevel;
            this.BloodPressureSystolic = BloodPressureSystolic;
            this.BloodPressureDiastolic = BloodPressureDiastolic;
            this.Temperature = Temperature;
        }
    }
}
