using System;

internal class VitaldatenGenerator
{
    private static readonly Random r = new Random();

    // Generiert eine realistische Herzfrequenz basierend auf der aktuellen Herzfrequenz im Bereich von 40 bis 131 mit einer Änderung von -2 bis +2
    public double GenerateHeartRate(double HeartRate)
    {
        HeartRate = GenerateRealisticValue(HeartRate, 50, 120, -2, 2);
        return HeartRate;
    }

    // Generiert eine realistische Atemfrequenz basierend auf der aktuellen Atemfrequenz im Bereich von 8 bis 25 mit einer Änderung von -2 bis +2
    public double GenerateRespirationRate(double RespirationRate)
    {
        return GenerateRealisticValue(RespirationRate, 8, 25, -2, 2);
    }

    // Generiert einen realistischen Sauerstoffgehalt basierend auf dem aktuellen Wert im Bereich von 91 bis 96 mit einer Änderung von -1 bis +1
    public double GenerateOxygenLevel(double OxygenLevel)
    {
        return GenerateRealisticValue(OxygenLevel, 91, 96, -1, 1);
    }

    // Generiert einen realistischen systolischen Blutdruck basierend auf dem aktuellen Wert im Bereich von 110 bis 130 mit einer Änderung von -3 bis +3
    public double GenerateSystolicBloodPressure(double BloodPressureSystolic)
    {
        return GenerateRealisticValue(BloodPressureSystolic, 90, 220, -3, 3);
    }

    // Generiert einen realistischen diastolischen Blutdruck basierend auf dem aktuellen Wert im Bereich von 70 bis 90 mit einer Änderung von -3 bis +3
    public double GenerateDiastolicBloodPressure(double BloodPressureDiastolic)
    {
        return GenerateRealisticValue(BloodPressureDiastolic, 60, 80, -3, 3);
    }

    // Generiert eine realistische Temperatur basierend auf dem aktuellen Wert im Bereich von 35 bis 39.1 mit einer Änderung von -3 bis +3
    public double GenerateTemperature(double Temperature)
    {
        return GenerateRealisticValue(Temperature, 35, 39.1, -3, 3);
    }

    // Hilfsfunktion zur Generierung realistischer Werte innerhalb eines bestimmten Bereichs mit einer zufälligen Änderung
    public double GenerateRealisticValue(double currentValue, int minValue, double maxValue, int minChange, int maxChange)
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
