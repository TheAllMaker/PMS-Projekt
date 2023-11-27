using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitaldatensimulator;

namespace Vitaldatensimulator
{
    internal class VitaldatenGenerator
    {
        private static Random r = new Random();
        private int currentHeartRate;
        private int currentRespirationRate;
        private int currentOxygenSaturation;
        private int currentSystolicBloodPressure;
        private int currentDiastolicBloodPressure;

        public int GenerateHeartRate()
        {
            return GenerateRealisticValue(ref currentHeartRate, 60, 100, -2, 2);
        }

        public int GenerateRespirationRate()
        {
            return GenerateRealisticValue(ref currentRespirationRate, 12, 20, -2, 2);
        }

        public int GenerateOxygenSaturation()
        {
            return GenerateRealisticValue(ref currentOxygenSaturation, 95, 100, -1, 1);
        }

        public int GenerateSystolicBloodPressure()
        {
            return GenerateRealisticValue(ref currentSystolicBloodPressure, 110, 130, -3, 3);
        }

        public int GenerateDiastolicBloodPressure()
        {
            return GenerateRealisticValue(ref currentDiastolicBloodPressure, 70, 90, -3, 3);
        }

        public int GenerateRealisticValue(ref int currentValue, int minValue, int maxValue, int minChange, int maxChange)
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

        public void GenerateVitaldata(PatientVitalDaten patient)
        {
            patient.HeartRate = GenerateHeartRate();
            patient.RespirationRate = GenerateRespirationRate();
            patient.OxygenLevel = GenerateOxygenSaturation();
            patient.BloodPressureSystolic = GenerateSystolicBloodPressure();
            patient.BloodPressureDiastolic = GenerateDiastolicBloodPressure();
        }
    }
}