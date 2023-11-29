using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Vitaldatensimulator
{
    public class PatientVitalDaten
    {
        public string MonitorID { get; set; }
        public double HeartRate { get;  set; }
        public double RespirationRate { get;  set; }
        public double OxygenLevel { get;  set; }
        public double BloodPressureSystolic { get;  set; }
        public double BloodPressureDiastolic { get;  set; }
        public double Temperature { get; set; }

        private VitaldatenGenerator generator;

        public PatientVitalDaten(string MonitorID, double HeartRate, double RespirationRate, double OxygenLevel, double BloodPressureSystolic, double BloodPressureDiastolic, double Temperature)
        {
            this.MonitorID = MonitorID;
            this.HeartRate = HeartRate;
            this.RespirationRate = RespirationRate;
            this.OxygenLevel = OxygenLevel;
            this.BloodPressureSystolic = BloodPressureSystolic;
            this.BloodPressureDiastolic = BloodPressureDiastolic;
            this.Temperature = Temperature;

            generator = new VitaldatenGenerator();
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