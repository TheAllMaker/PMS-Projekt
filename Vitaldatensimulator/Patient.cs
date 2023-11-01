using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Vitaldatensimulator
{
    public class Patient
    {
        public string Name { get; set; }
        public int Herzschlag { get;  set; }
        public int Atemfrequenz { get;  set; }
        public int Sauerstoffsättigung { get;  set; }
        public int SystolischerBlutdruck { get;  set; }
        public int DiastolischerBlutdruck { get;  set; }

        public Dictionary<string, int> Vitaldaten { get; private set; }
        private VitaldatenGenerator generator;

        public Patient(string name)
        {
            Name = name;
            generator = new VitaldatenGenerator();
            Vitaldaten = new Dictionary<string, int>();
        }

        public void GeneriereAlleVitaldaten()
        {
            generator.GeneriereVitaldaten(this);
            Vitaldaten["Herzschlag"] = Herzschlag;
            Vitaldaten["Atemfrequenz"] = Atemfrequenz;
            Vitaldaten["Sauerstoffsättigung"] = Sauerstoffsättigung;
            Vitaldaten["SystolischerBlutdruck"] = SystolischerBlutdruck;
            Vitaldaten["DiastolischerBlutdruck"] = DiastolischerBlutdruck;
        }
    }
}