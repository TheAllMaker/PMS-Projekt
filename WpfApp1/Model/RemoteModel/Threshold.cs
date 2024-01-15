using System;
using System.Collections.Generic;


/*
 * Overview:
 * The Threshold class represents a set of threshold values for vital health parameters associated with a monitor. It allows defining the acceptable range of values for various vital signs, such as Heart Rate, Respiration Rate, Oxygen Level, Blood Pressure (Systolic and Diastolic), and Temperature. Threshold instances are stored and managed in a dictionary for easy retrieval.
 * 
 * Functionality:
 * - Threshold(int MonitorID, int HeartRateMin, int HeartRateMax, double RespirationRateMin, double RespirationRateMax, 
 *              double OxygenLevelMin, double OxygenLevelMax, double TemperatureMin, double TemperatureMax, 
 *              int SystolicBloodPressureMin, int SystolicBloodPressureMax, int DiastolicBloodPressureMin, 
 *              int DiastolicBloodPressureMax): Constructor to create a new Threshold instance and associate it with a MonitorID. If an instance with the same MonitorID exists, it updates the existing instance.
 * - GetThresholdByMonitorID(int monitorID): Retrieves a Threshold instance based on the provided monitorID.
 * - RemoveThresholdByMonitorID(int monitorID): Removes the Threshold instance associated with the provided monitorID.
 * - CheckVitalDataAgainstThreshold(...): Checks if vital sign values are within the defined threshold ranges.
 * - CheckHeartRate(int heartRate): Checks if a Heart Rate value is within the defined range.
 * - CheckOxygenLevel(int oxygenLevel): Checks if an Oxygen Level value is within the defined range.
 * - CheckBloodPressureDiastolic(int bloodPressureDiastolic): Checks if a Diastolic Blood Pressure value is within the defined range.
 * - CheckRespirationRate(int respirationRate): Checks if a Respiration Rate value is within the defined range.
 * - CheckBloodPressureSystolic(int bloodPressureSystolic): Checks if a Systolic Blood Pressure value is within the defined range.
 * - CheckTemperature(int temperature): Checks if a Temperature value is within the defined range.
 * - Various getter methods to retrieve the threshold values for specific vital signs.
 * 
 * Usage:
 * - Create Threshold instances using the constructor, e.g., `new Threshold(MonitorID, HeartRateMin, HeartRateMax, ...)`.
 * - Retrieve a Threshold instance associated with a monitorID using `GetThresholdByMonitorID(monitorID)`.
 * - Remove a Threshold instance associated with a monitorID using `RemoveThresholdByMonitorID(monitorID)`.
 * - Use the provided methods to check if vital sign values are within the defined threshold ranges.
 */



namespace MediTrack.Model.RemoteModel
{
    public class Threshold
    {
        private int monitorID;
        private int heartRateMin;
        private int heartRateMax;
        private double respirationRateMin;
        private double respirationRateMax;
        private double oxygenLevelMin;
        private double oxygenLevelMax;
        private double temperatureMin;
        private double temperatureMax;
        private int systolicBloodPressureMin;
        private int systolicBloodPressureMax;
        private int diastolicBloodPressureMin;
        private int diastolicBloodPressureMax;


        private static Dictionary<int, Threshold> thresholdDictionary = new Dictionary<int, Threshold>();

        public Threshold(int MonitorID, int HeartRateMin, int HeartRateMax, double RespirationRateMin, double RespirationRateMax, double OxygenLevelMin, double OxygenLevelMax,
                         double TemperatureMin, double TemperatureMax, int SystolicBloodPressureMin, int SystolicBloodPressureMax,
                         int DiastolicBloodPressureMin, int DiastolicBloodPressureMax)
        {
            monitorID = MonitorID;
            heartRateMin = HeartRateMin;
            heartRateMax = HeartRateMax;
            oxygenLevelMin = OxygenLevelMin;
            oxygenLevelMax = OxygenLevelMax;
            temperatureMin = TemperatureMin;
            temperatureMax = TemperatureMax;
            systolicBloodPressureMin = SystolicBloodPressureMin;
            systolicBloodPressureMax = SystolicBloodPressureMax;
            diastolicBloodPressureMin = DiastolicBloodPressureMin;
            diastolicBloodPressureMax = DiastolicBloodPressureMax;
            respirationRateMin = RespirationRateMin;
            respirationRateMax = RespirationRateMax;

            if (thresholdDictionary.ContainsKey(monitorID))
            {
                // Update existing instance instead of adding a new one
                thresholdDictionary[monitorID] = this;
            }
            else
            {
                thresholdDictionary.Add(monitorID, this);
            }
        }

        public static Threshold GetThresholdByMonitorID(int monitorID)
        {
            // Rückgabe der Instanz basierend auf der monitorID
            if (thresholdDictionary.TryGetValue(monitorID, out Threshold threshold))
            {
                return threshold;
            }
            return null;
        }

        public static void RemoveThresholdByMonitorID(int monitorID)
        {
            if (thresholdDictionary.ContainsKey(monitorID))
            {
                thresholdDictionary.Remove(monitorID);
            }
        }
        public bool CheckVitalDataAgainstThreshold(int heartRateValue, int oxygenLevelValue, int bloodPressureDiastolicValue,
                                          int respirationRateValue, int bloodPressureSystolicValue, int temperatureValue)
        {
            bool isWithinThreshold = heartRateValue >= heartRateMin && heartRateValue <= heartRateMax &&
                                      oxygenLevelValue >= oxygenLevelMin && oxygenLevelValue <= oxygenLevelMax &&
                                      bloodPressureDiastolicValue >= diastolicBloodPressureMin && bloodPressureDiastolicValue <= diastolicBloodPressureMax &&
                                      respirationRateValue >= respirationRateMin && respirationRateValue <= respirationRateMax &&
                                      bloodPressureSystolicValue >= systolicBloodPressureMin && bloodPressureSystolicValue <= systolicBloodPressureMax &&
                                      temperatureValue >= temperatureMin && temperatureValue <= temperatureMax;

            return isWithinThreshold;
        }
        public bool CheckHeartRate(int heartRate)
        {
            if (heartRateMin != 0 || heartRateMax != 0)
            {
                return heartRate >= heartRateMin && heartRate <= heartRateMax;
            }
            return true;
        }

        public bool CheckOxygenLevel(int oxygenLevel)
        {
            if(oxygenLevelMin != 0 || oxygenLevelMax != 0)
            {
                return oxygenLevel >= oxygenLevelMin && oxygenLevel <= oxygenLevelMax;
            }
            return true;
        }

        public bool CheckBloodPressureDiastolic(int bloodPressureDiastolic)
        {
            if (diastolicBloodPressureMin != 0 || diastolicBloodPressureMax != 0)
            {
                return bloodPressureDiastolic >= diastolicBloodPressureMin && bloodPressureDiastolic <= diastolicBloodPressureMax;
            }
            return true;
        }

        public bool CheckRespirationRate(int respirationRate)
        {
            if (respirationRateMin != 0 || respirationRateMax != 0)
            {
                return respirationRate >= respirationRateMin && respirationRate <= respirationRateMax;
            }
            return true;
        }

        public bool CheckBloodPressureSystolic(int bloodPressureSystolic)
        {
            if (systolicBloodPressureMin != 0 || systolicBloodPressureMax != 0)
            {
                return bloodPressureSystolic >= systolicBloodPressureMin && bloodPressureSystolic <= systolicBloodPressureMax;
            }
            return true;
        }

        public bool CheckTemperature(int temperature)
        {
            if (temperatureMin != 0 || temperatureMax != 0)
            {
                return temperature >= temperatureMin && temperature <= temperatureMax;
            }
            return true;
        }


        public int GetHeartRateMin()
        {
            return heartRateMin;
        }

        public int GetHeartRateMax()
        {
            return heartRateMax;
        }

        public double GetRespirationRateMin()
        {
            return respirationRateMin;
        }

        public double GetRespirationRateMax()
        {
            return respirationRateMax;
        }

        public double GetOxygenLevelMin()
        {
            return oxygenLevelMin;
        }

        public double GetOxygenLevelMax()
        {
            return oxygenLevelMax;
        }

        public double GetTemperatureMin()
        {
            return temperatureMin;
        }

        public double GetTemperatureMax()
        {
            return temperatureMax;
        }

        public int GetSystolicBloodPressureMin()
        {
            return systolicBloodPressureMin;
        }

        public int GetSystolicBloodPressureMax()
        {
            return systolicBloodPressureMax;
        }

        public int GetDiastolicBloodPressureMin()
        {
            return diastolicBloodPressureMin;
        }

        public int GetDiastolicBloodPressureMax()
        {
            return diastolicBloodPressureMax;
        }

    }
}
