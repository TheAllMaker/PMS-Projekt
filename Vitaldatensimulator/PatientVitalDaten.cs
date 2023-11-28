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
        public double _BloodPressureDiastolic { get;  set; }
        public Dictionary<string, double> Vitaldaten { get; set; }

        private VitaldatenGenerator generator;

        public PatientVitalDaten(string MonitorID, double HeartRate, double RespirationRate, double OxygenLevel, double BloodPressureSystolic, double BloodPressureDiastolic)
        {
            this.MonitorID = MonitorID;
            this.HeartRate = HeartRate;
            this.RespirationRate = RespirationRate;
            this.OxygenLevel = OxygenLevel;
            this.BloodPressureSystolic = BloodPressureSystolic;
            _BloodPressureDiastolic = BloodPressureDiastolic;
            generator = new VitaldatenGenerator();
            Vitaldaten = new Dictionary<string, double>();
        }

        public void GenerateAllVitaldata()
        {
            //generator.GenerateVitaldata(this);
            Vitaldaten["Heart Rate"] = generator.GenerateHeartRate(HeartRate);
            Vitaldaten["Respiration Rate"] = generator.GenerateRespirationRate(RespirationRate);
            Vitaldaten["Oxygen Level"] = generator.GenerateOxygenSaturation(OxygenLevel);
            Vitaldaten["Blood Pressure Systolic"] = generator.GenerateSystolicBloodPressure(BloodPressureSystolic);
            Vitaldaten["Blood Pressure Diastolic"] = generator.GenerateDiastolicBloodPressure(_BloodPressureDiastolic);
        }
    }
}