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
        //private double currentHeartRate;
        private double currentRespirationRate;
        private double currentOxygenLevel;
        private double currentSystolicBloodPressure;
        private double currentDiastolicBloodPressure;

        public int GenerateHeartRate(double HeartRate)
        {
            //currentHeartRate = HeartRate;
            return GenerateRealisticValue(ref HeartRate, 60, 100, -2, 2);
        }

        public int GenerateRespirationRate(double RespirationRate)
        {
            return GenerateRealisticValue(ref currentRespirationRate, 12, 20, -2, 2);
        }

        public int GenerateOxygenSaturation(double OxygenLevel) // NOCH NAME ÄNDERN!
        {
            return GenerateRealisticValue(ref currentOxygenSaturation, 95, 100, -1, 1);
        }

        public int GenerateSystolicBloodPressure(double BloodPressureSystolic)
        {
            return GenerateRealisticValue(ref currentSystolicBloodPressure, 110, 130, -3, 3);
        }

        public int GenerateDiastolicBloodPressure(double BloodPressureDiastolic)
        {
            return GenerateRealisticValue(ref currentDiastolicBloodPressure, 70, 90, -3, 3);
        }

        public int GenerateRealisticValue(ref double currentValue, int minValue, int maxValue, int minChange, int maxChange)
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