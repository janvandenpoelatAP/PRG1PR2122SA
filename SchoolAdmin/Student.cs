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
        public DateTime Geboortedatum;
        public uint Sudentennummer;
        private VakInschrijving[] vakInschrijvingen = new VakInschrijving[5];
        public byte BepaalWerkbelasting()
        {
            byte werkbelasting = 0;
            for (int i = 0; i < vakInschrijvingen.Length; i++)
            {
                if (vakInschrijvingen[i] is not null)
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
            for (int i = 0; i < vakInschrijvingen.Length; i++)
            {
                if (vakInschrijvingen[i] is not null)
                {
                    if (vakInschrijvingen[i].Resultaat is not null)
                    {
                        som += (byte)vakInschrijvingen[i].Resultaat;
                        aantalCursussen++;
                    }
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
            if (cursusIndex < 0 || cursusIndex >= vakInschrijvingen.Length || vakInschrijvingen[cursusIndex] is null || behaaldResulaat > 20)
            {
                Console.WriteLine("Ongeldig cijfer!");
            }
            else
            {
               vakInschrijvingen[cursusIndex].Resultaat = behaaldResulaat;
            }
        }
        public void RegistreerVakInschrijving(string cursus, byte? behaaldResultaat)
        {
            for (int i = 0; i < vakInschrijvingen.Length; i++)
            {
                if (vakInschrijvingen[i] is null)
                {
                    vakInschrijvingen[i] = new VakInschrijving(cursus, behaaldResultaat);;
                    return;
                }
            }
        }
        public void ToonOverzicht()
        {
            DateTime nu = DateTime.Now;
            int aantalJaar = nu.Year - this.Geboortedatum.Year - 1;
            if (nu.Month > Geboortedatum.Month || nu.Month == Geboortedatum.Month && nu.Day >= Geboortedatum.Day)
            {
                aantalJaar++;
            }
            Console.WriteLine($"{Naam}, {aantalJaar} jaar");
            Console.WriteLine();
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("*************");
            for (int i = 0; i < vakInschrijvingen.Length; i++)
            {
                if (vakInschrijvingen[i] is not null)
                {
                    Console.WriteLine($"{vakInschrijvingen[i].Naam}:\t{vakInschrijvingen[i].Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static Student StudentUitTekstFormaat(string csvWaarde)
        {
            string[] studentInfo = csvWaarde.Split(';');
            Student student = new Student();
            student.Naam = studentInfo[0];
            student.Geboortedatum = new DateTime(Convert.ToInt32(studentInfo[3]), Convert.ToInt32(studentInfo[2]), Convert.ToInt32(studentInfo[1]));
            for (int i = 4; i < studentInfo.Length; i += 2)
            {
                student.RegistreerVakInschrijving(studentInfo[i], Convert.ToByte(studentInfo[i + 1]));
            }
            return student;
        }
        public static void DemonstreerStudenten()
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.Geboortedatum = new DateTime(2001,1,3);
            student1.RegistreerVakInschrijving("Communicatie", 12);
            student1.RegistreerVakInschrijving("Programmeren", null);
            student1.RegistreerVakInschrijving("Webtechnologie", 13);
            student1.ToonOverzicht();
            
            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.Geboortedatum = new DateTime(1998, 1, 1);
            student2.RegistreerVakInschrijving("Communicatie", 13);
            student2.RegistreerVakInschrijving("Programmeren", null);
            student2.RegistreerVakInschrijving("Databanken", 14);
            student2.ToonOverzicht();
        }
        public static void DemonstreerStudentUitTekstFormaat()
        {
            Console.WriteLine("Geef de tekstvoorstelling van 1 student in CSV-formaat:");
            string csvWaarde = Console.ReadLine();
            Student student = Student.StudentUitTekstFormaat(csvWaarde);
            student.ToonOverzicht();
        }
    }
}