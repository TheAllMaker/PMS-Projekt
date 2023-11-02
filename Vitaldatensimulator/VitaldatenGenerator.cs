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
        private int aktuellerHerzschlag = 80; // Startwert
        private int aktuelleAtemfrequenz = 16; // Startwert
        private int aktuelleSauerstoffsättigung = 98; // Startwert
        private int aktuellerSystolischerBlutdruck = 120; // Startwert
        private int aktuellerDiastolischerBlutdruck = 80; // Startwert

        //public int GeneriereHerzschlag()
        //{
          //  return r.Next(60, 100);
        //}

        public int GeneriereHerzschlag()
        {
            return GeneriereRealistischenWert(ref aktuellerHerzschlag, 60, 100, -2, 2);
        }

        public int GeneriereAtemfrequenz()
        {
            return GeneriereRealistischenWert(ref aktuelleAtemfrequenz, 12, 20, -2, 2);
        }

        public int GeneriereSauerstoffsättigung()
        {
            return GeneriereRealistischenWert(ref aktuelleSauerstoffsättigung, 95, 100, -1, 1);
        }

        public int GeneriereSystolischerBlutdruck()
        {
            return GeneriereRealistischenWert(ref aktuellerSystolischerBlutdruck, 110, 130, -3, 3);
        }

        public int GeneriereDiastolischerBlutdruck()
        {
            return GeneriereRealistischenWert(ref aktuellerDiastolischerBlutdruck, 70, 90, -3, 3);
        }

        public int GeneriereRealistischenWert(ref int aktuellerWert, int minWert, int maxWert, int minÄnderung, int maxÄnderung)
        {
            int aenderung = r.Next(minÄnderung, maxÄnderung + 1);
            aktuellerWert += aenderung;

            // Überprüfen, ob der neue Wert im Bereich liegt
            if (aktuellerWert < minWert)
            {
                aktuellerWert = minWert;
            }
            else if (aktuellerWert > maxWert)
            {
                aktuellerWert = maxWert;
            }

            return aktuellerWert;
        }


        public void GeneriereVitaldaten(PatientVitalDaten patient)
        {
            patient.Herzschlag = GeneriereHerzschlag();
            patient.Atemfrequenz = GeneriereAtemfrequenz();
            patient.Sauerstoffsättigung = GeneriereSauerstoffsättigung();
            patient.SystolischerBlutdruck = GeneriereSystolischerBlutdruck();
            patient.DiastolischerBlutdruck = GeneriereDiastolischerBlutdruck();
        }
    }
}
