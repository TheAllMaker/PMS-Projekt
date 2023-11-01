using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitaldatensimulator
{
    internal class VitaldatenGenerator
    {
        private static Random r = new Random();

        public int GeneriereHerzschlag()
        {
            return r.Next(60, 100);
        }

        public int GeneriereAtemfrequenz()
        {
            return r.Next(12, 20);
        }

        public int GeneriereSauerstoffsättigung()
        {
            return r.Next(95, 100);
        }

        public int GeneriereSystolischerBlutdruck()
        {
            return r.Next(110, 130);
        }

        public int GeneriereDiastolischerBlutdruck()
        {
            return r.Next(70, 90);
        }

        public void GeneriereVitaldaten(Patient patient)
        {
            patient.Herzschlag = GeneriereHerzschlag();
            patient.Atemfrequenz = GeneriereAtemfrequenz();
            patient.Sauerstoffsättigung = GeneriereSauerstoffsättigung();
            patient.SystolischerBlutdruck = GeneriereSystolischerBlutdruck();
            patient.DiastolischerBlutdruck = GeneriereDiastolischerBlutdruck();
        }
    }
}
