using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitaldaten_Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Random r = new Random();
            int Herzschlag = r.Next(60, 80);
            Console.WriteLine("Herzschlag Test:" + Herzschlag);
            Console.ReadLine();


        }
    }
}
