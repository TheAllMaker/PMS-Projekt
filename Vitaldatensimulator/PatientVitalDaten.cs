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
        public Dictionary<string, double> Vitaldaten { get; set; }

        private VitaldatenGenerator generator;

        public PatientVitalDaten(string MonitorID, double HeartRate, double RespirationRate, double OxygenLevel, double BloodPressureSystolic, double BloodPressureDiastolic)
        {
            this.MonitorID = MonitorID;
            this.HeartRate = HeartRate;
            this.RespirationRate = RespirationRate;
            this.OxygenLevel = OxygenLevel;
            this.BloodPressureSystolic = BloodPressureSystolic;
            this.BloodPressureDiastolic = BloodPressureDiastolic;
            generator = new VitaldatenGenerator();
            Vitaldaten = new Dictionary<string, double>();
        }

        public void GenerateAllVitaldata()
        {
            this.HeartRate = generator.GenerateHeartRate(HeartRate); ;
            this.RespirationRate = generator.GenerateRespirationRate(RespirationRate); ;
            this.OxygenLevel = generator.GenerateOxygenSaturation(OxygenLevel); ;
            this.BloodPressureSystolic = generator.GenerateSystolicBloodPressure(BloodPressureSystolic); ;
            this.BloodPressureDiastolic = generator.GenerateDiastolicBloodPressure(BloodPressureDiastolic); ;

            Vitaldaten["Heart Rate"] = this.HeartRate;
            Vitaldaten["Respiration Rate"] = this.RespirationRate;
            Vitaldaten["Oxygen Level"] = this.OxygenLevel;
            Vitaldaten["Blood Pressure Systolic"] = this.BloodPressureSystolic;
            Vitaldaten["Blood Pressure Diastolic"] = this.BloodPressureDiastolic;
        }
    }
}