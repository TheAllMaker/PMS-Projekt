using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Vitaldatensimulator;

namespace Vitaldatensimulator
{
    internal class VitaldatenGenerator
    {
        private static Random r = new Random();
        public double GenerateHeartRate(double HeartRate)
        {
            HeartRate = GenerateRealisticValue(HeartRate, 50, 120, -2, 2);
            return HeartRate;
        }

        public double GenerateRespirationRate(double RespirationRate)
        {
            return GenerateRealisticValue(RespirationRate, 12, 20, -2, 2);
        }

        public double GenerateOxygenSaturation(double OxygenLevel) // NOCH NAME ÄNDERN!
        {
            return GenerateRealisticValue(OxygenLevel, 95, 100, -1, 1);
        }

        public double GenerateSystolicBloodPressure(double BloodPressureSystolic)
        {
            return GenerateRealisticValue(BloodPressureSystolic, 110, 130, -3, 3);
        }

        public double GenerateDiastolicBloodPressure(double BloodPressureDiastolic)
        {
            return GenerateRealisticValue(BloodPressureDiastolic, 70, 90, -3, 3);
        }

        public double GenerateRealisticValue(double currentValue, int minValue, int maxValue, int minChange, int maxChange)
        {
            int change = r.Next(minChange, maxChange + 1);
            currentValue += change;

            if (currentValue < minValue)
            {
                currentValue = minValue;
            }
            else if (currentValue > maxValue)
            {
                currentValue = maxValue;
            }

            return currentValue;
        }

        //public void GenerateVitaldata(PatientVitalDaten patient)
        //{
          //  patient._HeartRate = GenerateHeartRate(patient._HeartRate);
           // patient._RespirationRate = GenerateRespirationRate();
            //patient._OxygenLevel = GenerateOxygenSaturation();
            //patient._BloodPressureSystolic = GenerateSystolicBloodPressure();
            //patient._BloodPressureDiastolic = GenerateDiastolicBloodPressure();
        //}
    }
}