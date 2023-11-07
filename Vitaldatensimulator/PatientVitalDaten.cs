using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Vitaldatensimulator
{
    public class PatientVitalDaten
    {
        public string Name { get; set; }
        public int HeartRate { get;  set; }
        public int RespirationRate { get;  set; }
        public int OxygenLevel { get;  set; }
        public int BloodPressureSystolic { get;  set; }
        public int BloodPressureDiastolic { get;  set; }
        public Dictionary<string, int> Vitaldaten { get; set; }

        private VitaldatenGenerator generator;

        public PatientVitalDaten(string name)
        {
            Name = name;
            generator = new VitaldatenGenerator();
            Vitaldaten = new Dictionary<string, int>();
        }

        public void GenerateAllVitaldata()
        {
            generator.GenerateVitaldata(this);
            Vitaldaten["HeartRate"] = HeartRate;
            Vitaldaten["RespirationRate"] = RespirationRate;
            Vitaldaten["OxygenLevel"] = OxygenLevel;
            Vitaldaten["BloodPressureSystolic"] = BloodPressureSystolic;
            Vitaldaten["BloodPressureDiastolic"] = BloodPressureDiastolic;
        }
    }
}