using System;

namespace Vitaldatensimulator
{
    internal class VitaldatenGenerator
    {
        private static readonly Random r = new Random();

        // Generiert eine realistische Herzfrequenz basierend auf der aktuellen Herzfrequenz im Bereich von 40 bis 131 mit einer Änderung von -2 bis +2
        public int GenerateHeartRate(int HeartRate)
        {
            HeartRate = GenerateRealisticValue(HeartRate, 40, 200, -1, 1);
            return HeartRate;
        }

        // Generiert eine realistische Atemfrequenz basierend auf der aktuellen Atemfrequenz im Bereich von 8 bis 25 mit einer Änderung von -2 bis +2
        public int GenerateRespirationRate(int RespirationRate)
        {
            return GenerateRealisticValue(RespirationRate, 8, 25, -1, 1);
        }

        // Generiert einen realistischen Sauerstoffgehalt basierend auf dem aktuellen Wert im Bereich von 91 bis 96 mit einer Änderung von -1 bis +1
        public int GenerateOxygenLevel(int OxygenLevel)
        {
            return GenerateRealisticValue(OxygenLevel, 91, 96, -1, 1);
        }

        // Generiert einen realistischen systolischen Blutdruck basierend auf dem aktuellen Wert im Bereich von 110 bis 130 mit einer Änderung von -3 bis +3
        public int GenerateSystolicBloodPressure(int BloodPressureSystolic)
        {
            return GenerateRealisticValue(BloodPressureSystolic, 90, 220, -1, 1);
        }

        // Generiert einen realistischen diastolischen Blutdruck basierend auf dem aktuellen Wert im Bereich von 70 bis 90 mit einer Änderung von -3 bis +3
        public int GenerateDiastolicBloodPressure(int BloodPressureDiastolic)
        {
            return GenerateRealisticValue(BloodPressureDiastolic, 60, 80, -1, 1);
        }

        // Generiert eine realistische Temperatur basierend auf dem aktuellen Wert im Bereich von 35 bis 39.1 mit einer Änderung von -0.1 bis 0.1
        public double GenerateTemperature(double Temperature)
        {
            return GenerateRealisticTemperature(Temperature, 35, 39, -0.05, 0.05);
        }

        private double GenerateRealisticTemperature(double currentValue, double minValue, double maxValue, double minChange, double maxChange)
        {
            double change = r.NextDouble() * (maxChange - minChange) + minChange;
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
        public int GenerateRealisticValue(int currentValue, int minValue, int maxValue, int minChange, int maxChange)
        {
            int change = r.Next(minChange, maxChange + 1);
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
