using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitaldatensimulator
{
    internal class VitaldatenGenerator
    {
        private int Herzschlag;
        private int Atemfrequenz;
        private int Sauerstoffsättigung;
        private int SystolischerBlutdruck;
        private int DiastolischerBlutdruck;
        private Random r = new Random();

        public int HerzschlagGenerator() 
        {
            Herzschlag = r.Next(60, 100);
            return Herzschlag; 
        }
        public int AtemfrequenzGenerator()
        {
            Atemfrequenz = r.Next(12, 20);
            return Atemfrequenz;
        }
        public int SauerstoffsättigungGenerator()
        {
            Sauerstoffsättigung = r.Next(95, 100);
            return Sauerstoffsättigung;
        }
        public int SystolischerBlutdruckGenerator()
        {
            SystolischerBlutdruck = r.Next(110, 130);
            return SystolischerBlutdruck;
        }
        public int DiastolischerBlutdruckGenerator()
        {
            DiastolischerBlutdruck = r.Next(70, 90);
            return DiastolischerBlutdruck;
        }

    }
}
