using System;

namespace Vitaldatensimulator
{
    public class VitalData
    {
        public string MonitorID { get; set; }
        public int HeartRate { get;  set; }
        public double RespirationRate { get;  set; }
        public double OxygenLevel { get;  set; }
        public int BloodPressureSystolic { get;  set; }
        public int BloodPressureDiastolic { get;  set; }
        public double Temperature { get; set; }
        public int Alive { get; set; }
        public string UUID { get; set; }

        private readonly VitalDataGenerator generator;

        public VitalData(string monitorId, int heartRate, double respirationRate, double oxygenLevel, int bloodPressureSystolic, int bloodPressureDiastolic, double temperature, string uuid, int alive = 1)
        {
            this.MonitorID = monitorId;
            this.HeartRate = heartRate;
            this.RespirationRate = respirationRate;
            this.OxygenLevel = oxygenLevel;
            this.BloodPressureSystolic = bloodPressureSystolic;
            this.BloodPressureDiastolic = bloodPressureDiastolic;
            this.Temperature = temperature;
            this.Alive = alive;
            this.UUID = uuid;

            generator = new VitalDataGenerator();
        }

        public string GetVitalData()
        {
            var vitaldaten = new
            {
                MonitorID,
                HeartRate,
                RespirationRate = Math.Round(RespirationRate, 3),
                OxygenLevel = Math.Round(OxygenLevel, 3),
                BloodPressureSystolic,
                BloodPressureDiastolic,
                Temperature = Math.Round(Temperature, 3),
                Alive,
                UUID
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(vitaldaten);
        }

        public void GenerateAllVitaldata()
        {
            this.HeartRate = generator.GenerateHeartRate(HeartRate);
            this.RespirationRate = generator.GenerateRespirationRate(RespirationRate);
            this.OxygenLevel = generator.GenerateOxygenLevel(OxygenLevel);
            this.BloodPressureSystolic = generator.GenerateSystolicBloodPressure(BloodPressureSystolic);
            this.BloodPressureDiastolic = generator.GenerateDiastolicBloodPressure(BloodPressureDiastolic);
            this.Temperature = generator.GenerateTemperature(Temperature);
        }
    }
}