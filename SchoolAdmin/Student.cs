using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Student
    {
        public static uint Sudententeller;
        private static List<Student> alleStudenten = new List<Student>();
        public static List<Student> AlleStudenten
        {
            get 
            { 
                return alleStudenten; 
            }
        }
        
        public string Naam;
        public DateTime GeboorteDatum;
        public uint Studentennummer;
        private VakInschrijving[] vakInschrijvingen = new VakInschrijving[5];
        public Student(string naam, DateTime geboorteDatum)
        {
            Naam = naam;
            GeboorteDatum = geboorteDatum;
            AlleStudenten.Add(this);
        }

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
        public void RegistreerVakInschrijving(Cursus cursus, byte? behaaldResultaat)
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
            int aantalJaar = nu.Year - this.GeboorteDatum.Year - 1;
            if (nu.Month > GeboorteDatum.Month || nu.Month == GeboorteDatum.Month && nu.Day >= GeboorteDatum.Day)
            {
                aantalJaar++;
            }
            Console.WriteLine($"{Naam}, {aantalJaar} jaar");
            Console.WriteLine();
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("*************");
            foreach (VakInschrijving vakInschrijving in vakInschrijvingen)
            {
                if (vakInschrijving is not null)
                {
                    Console.WriteLine($"{vakInschrijving.Cursus.Titel}:\t{vakInschrijving.Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static Student StudentUitTekstFormaat(string csvWaarde)
        {
            string[] studentInfo = csvWaarde.Split(';');
            Student student = new Student(studentInfo[0], new DateTime(Convert.ToInt32(studentInfo[3]), Convert.ToInt32(studentInfo[2]), Convert.ToInt32(studentInfo[1])));
            for (int i = 4; i < studentInfo.Length; i += 2)
            {
                Cursus cursus = new Cursus(studentInfo[i]);
                student.RegistreerVakInschrijving(cursus, Convert.ToByte(studentInfo[i + 1]));
            }
            return student;
        }
        public static void DemonstreerStudenten()
        {
            Cursus communicatie = new Cursus("Communicatie", new Student[2]);
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new Student[5], 6);
            Cursus databanken = new Cursus("Databanken", new Student[7], 5);

            Student student1 = new Student("Said Aziz", new DateTime(2001,1,3));
            student1.RegistreerVakInschrijving(communicatie, 12);
            student1.RegistreerVakInschrijving(programmeren, null);
            student1.RegistreerVakInschrijving(webtechnologie, 13);
            student1.ToonOverzicht();
            
            Student student2 = new Student("Mieke Vermeulen", new DateTime(1998, 1, 1));
            student2.RegistreerVakInschrijving(communicatie, 13);
            student2.RegistreerVakInschrijving(programmeren, null);
            student2.RegistreerVakInschrijving(databanken, 14);
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