using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace VitaldataSimulator
{
    public class VitaldataPatient
    {
        public string Name { get; set; }
        public int HeartRate { get;  set; }
        public int RespirationRate { get;  set; }
        public int OxygenLevel { get;  set; }
        public int BloodPressureSystolic { get;  set; }
        public int BloodPressureDiastolic { get;  set; }
        public Dictionary<string, int> Vitaldata { get; set; }

        private VitaldataGenerator generator;

        public VitaldataPatient(string name)
        {
            Name = name;
            generator = new VitaldataGenerator();
            Vitaldata = new Dictionary<string, int>();
        }

        public void GenerateAllVitaldata()
        {
            generator.GenerateVitaldata(this);
            Vitaldata["Herzschlag"] = HeartRate;
            Vitaldata["Atemfrequenz"] = RespirationRate;
            Vitaldata["Sauerstoffsättigung"] = OxygenLevel;
            Vitaldata["SystolischerBlutdruck"] = BloodPressureSystolic;
            Vitaldata["DiastolischerBlutdruck"] = BloodPressureDiastolic;
        }
    }
}