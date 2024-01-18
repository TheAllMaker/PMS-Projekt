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
        private int _heartRateMin;
        private int _heartRateMax;
        private double _respirationRateMin;
        private double _respirationRateMax;
        private double _oxygenLevelMin;
        private double _oxygenLevelMax;
        private double _temperatureMin;
        private double _temperatureMax;
        private int _systolicBloodPressureMin;
        private int _systolicBloodPressureMax;
        private int _diastolicBloodPressureMin;
        private int _diastolicBloodPressureMax;


        private static Dictionary<int, Threshold> thresholdDictionary = new Dictionary<int, Threshold>();

        public Threshold(int monitorId, int heartRateMin, int heartRateMax, double respirationRateMin, double respirationRateMax, double oxygenLevelMin, double oxygenLevelMax,
                         double temperatureMin, double temperatureMax, int systolicBloodPressureMin, int systolicBloodPressureMax,
                         int diastolicBloodPressureMin, int diastolicBloodPressureMax)
        {
            var monitorId1 = monitorId;
            _heartRateMin = heartRateMin;
            _heartRateMax = heartRateMax;
            _oxygenLevelMin = oxygenLevelMin;
            _oxygenLevelMax = oxygenLevelMax;
            _temperatureMin = temperatureMin;
            _temperatureMax = temperatureMax;
            _systolicBloodPressureMin = systolicBloodPressureMin;
            _systolicBloodPressureMax = systolicBloodPressureMax;
            _diastolicBloodPressureMin = diastolicBloodPressureMin;
            _diastolicBloodPressureMax = diastolicBloodPressureMax;
            _respirationRateMin = respirationRateMin;
            _respirationRateMax = respirationRateMax;

            if (thresholdDictionary.ContainsKey(monitorId1))
            {
                // Update existing instance instead of adding a new one
                thresholdDictionary[monitorId1] = this;
            }
            else
            {
                thresholdDictionary.Add(monitorId1, this);
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
            bool isWithinThreshold = heartRateValue >= _heartRateMin && heartRateValue <= _heartRateMax &&
                                      oxygenLevelValue >= _oxygenLevelMin && oxygenLevelValue <= _oxygenLevelMax &&
                                      bloodPressureDiastolicValue >= _diastolicBloodPressureMin && bloodPressureDiastolicValue <= _diastolicBloodPressureMax &&
                                      respirationRateValue >= _respirationRateMin && respirationRateValue <= _respirationRateMax &&
                                      bloodPressureSystolicValue >= _systolicBloodPressureMin && bloodPressureSystolicValue <= _systolicBloodPressureMax &&
                                      temperatureValue >= _temperatureMin && temperatureValue <= _temperatureMax;

            return isWithinThreshold;
        }
        public bool CheckHeartRate(int heartRate)
        {
            if (_heartRateMin != 0 || _heartRateMax != 0)
            {
                return heartRate >= _heartRateMin && heartRate <= _heartRateMax;
            }
            return true;
        }

        public bool CheckOxygenLevel(int oxygenLevel)
        {
            if(_oxygenLevelMin != 0 || _oxygenLevelMax != 0)
            {
                return oxygenLevel >= _oxygenLevelMin && oxygenLevel <= _oxygenLevelMax;
            }
            return true;
        }

        public bool CheckBloodPressureDiastolic(int bloodPressureDiastolic)
        {
            if (_diastolicBloodPressureMin != 0 || _diastolicBloodPressureMax != 0)
            {
                return bloodPressureDiastolic >= _diastolicBloodPressureMin && bloodPressureDiastolic <= _diastolicBloodPressureMax;
            }
            return true;
        }

        public bool CheckRespirationRate(int respirationRate)
        {
            if (_respirationRateMin != 0 || _respirationRateMax != 0)
            {
                return respirationRate >= _respirationRateMin && respirationRate <= _respirationRateMax;
            }
            return true;
        }

        public bool CheckBloodPressureSystolic(int bloodPressureSystolic)
        {
            if (_systolicBloodPressureMin != 0 || _systolicBloodPressureMax != 0)
            {
                return bloodPressureSystolic >= _systolicBloodPressureMin && bloodPressureSystolic <= _systolicBloodPressureMax;
            }
            return true;
        }

        public bool CheckTemperature(int temperature)
        {
            if (_temperatureMin != 0 || _temperatureMax != 0)
            {
                return temperature >= _temperatureMin && temperature <= _temperatureMax;
            }
            return true;
        }


        public int GetHeartRateMin()
        {
            return _heartRateMin;
        }

        public int GetHeartRateMax()
        {
            return _heartRateMax;
        }

        public double GetRespirationRateMin()
        {
            return _respirationRateMin;
        }

        public double GetRespirationRateMax()
        {
            return _respirationRateMax;
        }

        public double GetOxygenLevelMin()
        {
            return _oxygenLevelMin;
        }

        public double GetOxygenLevelMax()
        {
            return _oxygenLevelMax;
        }

        public double GetTemperatureMin()
        {
            return _temperatureMin;
        }

        public double GetTemperatureMax()
        {
            return _temperatureMax;
        }

        public int GetSystolicBloodPressureMin()
        {
            return _systolicBloodPressureMin;
        }

        public int GetSystolicBloodPressureMax()
        {
            return _systolicBloodPressureMax;
        }

        public int GetDiastolicBloodPressureMin()
        {
            return _diastolicBloodPressureMin;
        }

        public int GetDiastolicBloodPressureMax()
        {
            return _diastolicBloodPressureMax;
        }

    }
}
