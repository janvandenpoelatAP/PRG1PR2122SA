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
        private CursusResultaat[] cursusResultaten = new CursusResultaat[5];
        public byte BepaalWerkbelasting()
        {
            byte werkbelasting = 0;
            for (int i = 0; i < cursusResultaten.Length; i++)
            {
                if (cursusResultaten[i] is not null)
                {
                    werkbelasting += 10;
                }
            }
            return werkbelasting;
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            for (int i = 0; i < cursusResultaten.Length; i++)
            {
                if (cursusResultaten[i] is not null)
                {
                    som += cursusResultaten[i].Resultaat;
                    aantalCursussen++;
                }
            }
             return som / aantalCursussen;
        }
        public string GenereerNaamkaarje()
        {
            return $"{Naam} (STUDENT)";
        }
        public void Kwoteer(byte cursusIndex, byte behaaldResulaat)
        {
            if (cursusIndex < 0 || cursusIndex >= cursusResultaten.Length || cursusResultaten[cursusIndex] is null || behaaldResulaat > 20)
            {
                Console.WriteLine("Ongeldig cijfer!");
            }
            else
            {
               cursusResultaten[cursusIndex].Resultaat = behaaldResulaat;
            }
        }
        public void RegistreerCursusResultaat(string cursus, byte behaaldResultaat)
        {
            for (int i = 0; i < cursusResultaten.Length; i++)
            {
                if (cursusResultaten[i] is null)
                {
                    CursusResultaat nieuwCursusResultaat = new CursusResultaat();
                    cursusResultaten[i] = nieuwCursusResultaat;
                    nieuwCursusResultaat.Naam = cursus;
                    Kwoteer((byte)i, behaaldResultaat);
                    return;
                }
            }
        }
        public void ToonOverzicht()
        {
            DateTime nu = DateTime.Now;
            int aantalJaar = nu.Year - this.GeboorteDatum.Year - 1;
            if (nu.Month > GeboorteDatum.Month || nu.Month == GeboorteDatum.Month && nu.Day >= GeboorteDatum.Day)
            {
                aantalJaar++;
            }
            Console.WriteLine($"{Naam}, {aantalJaar} jaar");
            Console.WriteLine();
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("*************");
            for (int i = 0; i < cursusResultaten.Length; i++)
            {
                if (cursusResultaten[i] is not null)
                {
                    Console.WriteLine($"{cursusResultaten[i].Naam}:\t{cursusResultaten[i].Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static void DemonstreerStudenten()
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.GeboorteDatum = new DateTime(2001,1,3);
            student1.RegistreerCursusResultaat("Communicatie", 12);
            student1.RegistreerCursusResultaat("Programmeren", 15);
            student1.RegistreerCursusResultaat("Webtechnologie", 13);
            student1.ToonOverzicht();
            
            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.GeboorteDatum = new DateTime(1998, 1, 1);
            student2.RegistreerCursusResultaat("Communicatie", 13);
            student2.RegistreerCursusResultaat("Programmeren", 16);
            student2.RegistreerCursusResultaat("Databanken", 14);
            student2.ToonOverzicht();
        }
    }
}