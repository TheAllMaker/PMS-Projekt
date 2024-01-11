

namespace MediTrack.Model.RemoteModel
{
    internal class ThresholdGuard
    {


        //public static void NetWorkButtonSwitch ()
        //{
        //    // Zugriff auf die Button-Ressourcen
        //    ResourceDictionary buttonResources = PatientNetworkIcon.Resources as ResourceDictionary;

        //    if (buttonResources != null)
        //    {
        //        // Überprüfen und ändern Sie die Farben der SolidColorBrushes
        //        if (buttonResources.Contains("light-lightblue-10"))
        //        {
        //            SolidColorBrush lightLightBlue10Brush = buttonResources["light-lightblue-10"] as SolidColorBrush;
        //            if (lightLightBlue10Brush != null)
        //            {
        //                lightLightBlue10Brush.Color = Colors.Green; // Setzen Sie die Farbe auf Grün
        //            }
        //        }

        //        if (buttonResources.Contains("light-teal-10"))
        //        {
        //            SolidColorBrush lightTeal10Brush = buttonResources["light-teal-10"] as SolidColorBrush;
        //            if (lightTeal10Brush != null)
        //            {
        //                lightTeal10Brush.Color = Colors.Green; // Setzen Sie die Farbe auf Grün
        //            }
        //        }

        //        if (buttonResources.Contains("light-teal"))
        //        {
        //            SolidColorBrush lightTealBrush = buttonResources["light-teal"] as SolidColorBrush;
        //            if (lightTealBrush != null)
        //            {
        //                lightTealBrush.Color = Colors.Green; // Setzen Sie die Farbe auf Grün
        //            }
        //        }

        //        if (buttonResources.Contains("light-lightblue"))
        //        {
        //            SolidColorBrush lightLightBlueBrush = buttonResources["light-lightblue"] as SolidColorBrush;
        //            if (lightLightBlueBrush != null)
        //            {
        //                lightLightBlueBrush.Color = Colors.Green; // Setzen Sie die Farbe auf Grün
        //            }
        //        }
        //    }





        //}

        //public static void ThresholdCheck(List<object> mqttValues)
        //{
        //    double respirationRateMinValue = GetRespirationRateMin();
        //    double respirationRateMaxValue = detailedWindowInstance.GetRespirationRateMax();

        //    double oxygenLevelMinValue = detailedWindowInstance.GetOxygenLevelMin();
        //    double oxygenLevelMaxValue = detailedWindowInstance.GetOxygenLevelMax();

        //    double temperatureMinValue = detailedWindowInstance.GetTemperatureMin();
        //    double temperatureMaxValue = detailedWindowInstance.GetTemperatureMax();

        //    int heartRateMinValue = detailedWindowInstance.GetHeartRateMin();
        //    int heartRateMaxValue = detailedWindowInstance.GetHeartRateMax();

        //    int systolicBloodPressureMinValue = detailedWindowInstance.GetSystolicBloodPressureMin();
        //    int systolicBloodPressureMaxValue = detailedWindowInstance.GetSystolicBloodPressureMax();

        //    int diastolicBloodPressureMinValue = detailedWindowInstance.GetDiastolicBloodPressureMin();
        //    int diastolicBloodPressureMaxValue = detailedWindowInstance.GetDiastolicBloodPressureMax();

        //    double currentRespiration = Convert.ToDouble(mqttValues[1]);
        //    double currentOxygenLevel = Convert.ToDouble(mqttValues[2]);
        //    double currentTemperature = Convert.ToDouble(mqttValues[5]);
        //    int currentHeartRate = Convert.ToInt32(mqttValues[0]);
        //    int currentSystolicBloodPressure = Convert.ToInt32(mqttValues[3]);
        //    int currentDiastolicBloodPressure = Convert.ToInt32(mqttValues[4]);

        //    if (respirationRateMinValue != 0 || respirationRateMaxValue != 0 && IsValueOutOfRange(currentRespiration, respirationRateMinValue, respirationRateMaxValue))
        //    {
        //        Console.WriteLine("Geklappt?");
        //    }

        //    if (oxygenLevelMinValue != 0 || oxygenLevelMaxValue != 0 && IsValueOutOfRange(currentOxygenLevel, oxygenLevelMinValue, oxygenLevelMaxValue))
        //    {
        //        // handle out of range for oxygen level
        //    }

        //    if (temperatureMinValue != 0 || temperatureMaxValue != 0 && IsValueOutOfRange(currentTemperature, temperatureMinValue, temperatureMaxValue))
        //    {
        //        // handle out of range for temperature
        //    }

        //    if (heartRateMinValue != 0 || heartRateMaxValue != 0 && IsValueOutOfRange(currentHeartRate, heartRateMinValue, heartRateMaxValue))
        //    {
        //        Console.WriteLine("Geklappt?");
        //    }

        //    if (systolicBloodPressureMinValue != 0 || systolicBloodPressureMaxValue != 0 && IsValueOutOfRange(currentSystolicBloodPressure, systolicBloodPressureMinValue, systolicBloodPressureMaxValue))
        //    {
        //        // handle out of range for systolic blood pressure
        //    }

        //    if (diastolicBloodPressureMinValue != 0 || diastolicBloodPressureMaxValue != 0 && IsValueOutOfRange(currentDiastolicBloodPressure, diastolicBloodPressureMinValue, diastolicBloodPressureMaxValue))
        //    {
        //        // handle out of range for diastolic blood pressure
        //    }


        //}

        //private bool IsValueOutOfRange(double value, double minValue, double maxValue)
        //{
        //    return value < minValue || value > maxValue;
        //}

        //private bool IsValueOutOfRange(int value, int minValue, int maxValue)
        //{
        //    return value < minValue || value > maxValue;
        //}



    }
}
