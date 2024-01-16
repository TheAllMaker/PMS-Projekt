using System;

namespace VitaldataSimulator.Model
{
    public class VitalData
    {
        public string MonitorID { get; set; }
        public int HeartRate { get; set; }
        public double RespirationRate { get; set; }
        public double OxygenLevel { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }
        public double Temperature { get; set; }
        public int Alive { get; set; }
        public string UUID { get; set; }

        private readonly VitalDataGenerator _generator;

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

            _generator = new VitalDataGenerator();
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
            this.HeartRate = _generator.GenerateHeartRate(HeartRate);
            this.RespirationRate = _generator.GenerateRespirationRate(RespirationRate);
            this.OxygenLevel = _generator.GenerateOxygenLevel(OxygenLevel);
            this.BloodPressureSystolic = _generator.GenerateSystolicBloodPressure(BloodPressureSystolic);
            this.BloodPressureDiastolic = _generator.GenerateDiastolicBloodPressure(BloodPressureDiastolic);
            this.Temperature = _generator.GenerateTemperature(Temperature);
        }
    }
}