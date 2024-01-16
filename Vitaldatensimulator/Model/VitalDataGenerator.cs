using System;

namespace VitaldataSimulator.Model
{
    internal class VitalDataGenerator
    {
        private static readonly Random R = new Random();
        private const int MinHeartRate = 40;
        private const int MaxHeartRate = 200;
        private const double MinRespirationRate = 8;
        private const double MaxRespirationRate = 25;
        private const double MinOxygenLevel = 91;
        private const double MaxOxygenLevel = 96;
        private const int MinSystolicBloodPressure = 90;
        private const int MaxSystolicBloodPressure = 220;
        private const int MinDiastolicBloodPressure = 60;
        private const int MaxDiastolicBloodPressure = 80;
        private const double MinTemperature = 35;
        private const double MaxTemperature = 39;

        // Generiert eine realistische Herzfrequenz basierend auf der aktuellen Herzfrequenz im Bereich von 40 bis 200 mit einer Änderung von -1 bis +1
        public int GenerateHeartRate(int heartRate)
        {
            heartRate = GenerateRealisticValue(heartRate, MinHeartRate, MaxHeartRate, -1, 1);
            return heartRate;
        }

        // Generiert eine realistische Atemfrequenz basierend auf der aktuellen Atemfrequenz im Bereich von 8 bis 25 mit einer Änderung von -1 bis +1
        public double GenerateRespirationRate(double respirationRate)
        {
            return GenerateRealisticValueDouble(respirationRate, MinRespirationRate, MaxRespirationRate, -0.2, 0.2);
        }

        // Generiert einen realistischen Sauerstoffgehalt basierend auf dem aktuellen Wert im Bereich von 91 bis 96 mit einer Änderung von -1 bis +1
        public double GenerateOxygenLevel(double oxygenLevel)
        {
            return GenerateRealisticValueDouble(oxygenLevel, MinOxygenLevel, MaxOxygenLevel, -0.1, 0.1);
        }

        // Generiert einen realistischen systolischen Blutdruck basierend auf dem aktuellen Wert im Bereich von 90 bis 220 mit einer Änderung von -1 bis +1
        public int GenerateSystolicBloodPressure(int bloodPressureSystolic)
        {
            return GenerateRealisticValue(bloodPressureSystolic, MinSystolicBloodPressure, MaxSystolicBloodPressure, -1, 1);
        }

        // Generiert einen realistischen diastolischen Blutdruck basierend auf dem aktuellen Wert im Bereich von 60 bis 80 mit einer Änderung von -1 bis +1
        public int GenerateDiastolicBloodPressure(int bloodPressureDiastolic)
        {
            return GenerateRealisticValue(bloodPressureDiastolic, MinDiastolicBloodPressure, MaxDiastolicBloodPressure, -1, 1);
        }

        // Generiert eine realistische Temperatur basierend auf dem aktuellen Wert im Bereich von 35 bis 39 mit einer Änderung von -0.05 bis 0.05
        public double GenerateTemperature(double temperature)
        {
            return GenerateRealisticValueDouble(temperature, MinTemperature, MaxTemperature, -0.05, 0.05);
        }

        private double GenerateRealisticValueDouble(double currentValue, double minValue, double maxValue, double minChange, double maxChange)
        {
            double change = R.NextDouble() * (maxChange - minChange) + minChange;
            currentValue += change;

            // Wertebereich überprüfen und auf Mindest- oder Höchstwert setzen, falls überschritten
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


        // Hilfsfunktion zur Generierung realistischer Werte innerhalb eines bestimmten Bereichs mit einer zufälligen Änderung
        private int GenerateRealisticValue(int currentValue, int minValue, int maxValue, int minChange, int maxChange)
        {
            int change = R.Next(minChange, maxChange + 1);
            currentValue += change;

            // Wertebereich überprüfen und auf Mindest- oder Höchstwert setzen, falls überschritten
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
    }
}
