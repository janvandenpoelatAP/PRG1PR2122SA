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
        private string[] cursussen;

        public async void RegistreerVoorCursus(string cursus)
        {
            for (int i = 1; i < cursussen.Length; i++)
            {
                if (cursussen[i] is null)
                {
                    cursussen[i] = cursus;
                }
            }
        }
        public string GenereerNaamkaarje()
        {
            return $"{Naam} (STUDENT)";
        }
        public double BepaalWerkbelasting()
        {
            double werkbelasting = 0.0;
            for (int i = 0; i < cursussen.Length; i++)
            {
                werkbelasting += 10.0;
            }
            return werkbelasting;
        }
    }
}