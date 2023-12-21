﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Vitaldatensimulator
{
    public class MonitorVitalDaten
    {
        public string MonitorID { get; set; }
        public int HeartRate { get;  set; }
        public int RespirationRate { get;  set; }
        public int OxygenLevel { get;  set; }
        public int BloodPressureSystolic { get;  set; }
        public int BloodPressureDiastolic { get;  set; }
        public double Temperature { get; set; }
        public int Alive { get; set; }
        public string UUID { get; set; }

        private VitaldatenGenerator generator;

        public MonitorVitalDaten(string MonitorID, int HeartRate, int RespirationRate, int OxygenLevel, int BloodPressureSystolic, int BloodPressureDiastolic, double Temperature, string UUID, int Alive = 1)
        {
            this.MonitorID = MonitorID;
            this.HeartRate = HeartRate;
            this.RespirationRate = RespirationRate;
            this.OxygenLevel = OxygenLevel;
            this.BloodPressureSystolic = BloodPressureSystolic;
            this.BloodPressureDiastolic = BloodPressureDiastolic;
            this.Temperature = Temperature;
            this.Alive = Alive;
            this.UUID = UUID;

            generator = new VitaldatenGenerator();
        }

        public string GetVitalData()
        {
            var vitaldaten = new
            {
                MonitorID,
                HeartRate,
                RespirationRate,
                OxygenLevel,
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
            this.HeartRate = generator.GenerateHeartRate(HeartRate); ;
            this.RespirationRate = generator.GenerateRespirationRate(RespirationRate); ;
            this.OxygenLevel = generator.GenerateOxygenLevel(OxygenLevel); ;
            this.BloodPressureSystolic = generator.GenerateSystolicBloodPressure(BloodPressureSystolic); ;
            this.BloodPressureDiastolic = generator.GenerateDiastolicBloodPressure(BloodPressureDiastolic);
            this.Temperature = generator.GenerateTemperature(Temperature);
        }
    }
}