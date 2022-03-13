using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Student
    {
        public static uint Sudententeller;
        public string Naam;
        public DateTime GeboorteDatum;
        public uint Sudentennummer;
        public string[] Cursussen;

        public string GenereerNaamkaarje()
        {
            return $"{Naam} (STUDENT)";
        }
        public double BepaalWerkbelasting()
        {
            double werkbelasting = 0.0;
            for (int i = 0; i < Cursussen.Length; i++)
            {
                werkbelasting += 10.0;
            }
            return werkbelasting;
        }
    }
}