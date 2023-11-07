using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitaldataSimulator
{
    internal class VitaldataGenerator
    {
        private static Random r = new Random();
        private int StartHeartRate = 80; // Startwert
        private int StartRespirationRate = 16; // Startwert
        private int StartOxygenLevel = 98; // Startwert
        private int StartBloodPressureSystolic = 120; // Startwert
        private int StartBloodPressureDiastolic = 80; // Startwert

        public int GenerateHeartbeat()
        {
            return GenerateRealisticValue(ref StartHeartRate, 60, 100, -2, 2);
        }

        public int GenerateRespirationRate()
        {
            return GenerateRealisticValue(ref StartRespirationRate, 12, 20, -2, 2);
        }

        public int GenerateOxygenLevel()
        {
            return GenerateRealisticValue(ref StartOxygenLevel, 95, 100, -1, 1);
        }

        public int GenerateBloodPressureSystolic()
        {
            return GenerateRealisticValue(ref StartBloodPressureSystolic, 110, 130, -3, 3);
        }

        public int GenerateBloodPressureDiastolic()
        {
            return GenerateRealisticValue(ref StartBloodPressureDiastolic, 70, 90, -3, 3);
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



        public void GenerateVitaldata(VitaldataPatient patient)
        {
            patient.HeartRate = GenerateHeartbeat();
            patient.RespirationRate = GenerateRespirationRate();
            patient.OxygenLevel = GenerateOxygenLevel();
            patient.BloodPressureSystolic = GenerateBloodPressureSystolic();
            patient.BloodPressureDiastolic = GenerateBloodPressureDiastolic();
        }
    }
}
