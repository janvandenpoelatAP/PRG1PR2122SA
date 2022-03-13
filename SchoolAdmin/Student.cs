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
        private string[] cursussen = new string[5];
        public byte[] CursusResultaten = new byte[5];
    public byte BepaalWerkbelasting()
        {
            byte werkbelasting = 0;
            for (int i = 0; i < cursussen.Length; i++)
            {
                werkbelasting += 10;
            }
            return werkbelasting;
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            for (int i = 0; i < cursussen.Length; i++)
            {
                if (cursussen[i] is not null)
                {
                    som += CursusResultaten[i];
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
            if (cursusIndex < 0 || cursusIndex >= CursusResultaten.Length || cursussen[cursusIndex] is null || behaaldResulaat > 20)
            {
                Console.WriteLine("Ongeldig cijfer!");
            }
            else
            {
               CursusResultaten[cursusIndex] = behaaldResulaat;
            }
        }
        public void RegistreerVoorCursus(string cursus)
        {
            for (int i = 0; i < cursussen.Length; i++)
            {
                if (cursussen[i] is null)
                {
                    cursussen[i] = cursus;
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
            for (int i = 0; i < cursussen.Length; i++)
            {
                if (cursussen[i] is not null)
                {
                    Console.WriteLine($"{cursussen[i]}:\t{CursusResultaten[i]}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static void DemonstreerStudenten()
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.GeboorteDatum = new DateTime(2001,1,3);
            student1.RegistreerVoorCursus("Communicatie");
            student1.CursusResultaten[0] = 12;
            student1.RegistreerVoorCursus("Programmeren");
            student1.CursusResultaten[1] = 15;
            student1.RegistreerVoorCursus("Webtechnologie");
            student1.CursusResultaten[2] = 13;
            student1.ToonOverzicht();
            
            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.GeboorteDatum = new DateTime(1998, 1, 1);
            student2.RegistreerVoorCursus("Communicatie");
            student2.CursusResultaten[0] = 13;
            student2.RegistreerVoorCursus("Programmeren");
            student2.CursusResultaten[1] = 16;
            student2.RegistreerVoorCursus("Databanken");
            student2.CursusResultaten[2] = 14;
            student2.ToonOverzicht();
        }
    }
}