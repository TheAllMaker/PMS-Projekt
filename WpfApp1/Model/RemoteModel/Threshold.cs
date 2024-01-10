using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else
            {
                throw new ArgumentException("Threshold für die gegebene monitorID wurde nicht gefunden.");
            }
        }
        public static void RemoveThresholdByMonitorID(int monitorID)
        {
            if (thresholdDictionary.ContainsKey(monitorID))
            {
                thresholdDictionary.Remove(monitorID);
            }
            else
            {
                throw new ArgumentException("Threshold für die gegebene monitorID wurde nicht gefunden.");
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
