using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Student : Persoon
    {
        private List<VakInschrijving> vakInschrijvingen = new List<VakInschrijving>();
        public static uint Studententeller;
        public static ImmutableList<Student> AlleStudenten
        {
            get
            {
                var enkelStudenten = new List<Student>();
                foreach (var persoon in Persoon.AllePersonen)
                {
                    if (persoon is Student)
                    {
                        enkelStudenten.Add((Student)persoon);
                    }
                }
                return enkelStudenten.ToImmutableList<Student>();
            }
        }
        private Dictionary<DateTime,string> dossier;
        public ImmutableDictionary<DateTime,string> Dossier {
            get {
                return this.dossier.ToImmutableDictionary<DateTime,string>();
            }
        }
        public Student(string naam, DateTime geboorteDatum) : base(naam, geboorteDatum)
        {
            this.dossier = new Dictionary<DateTime, string>();
        }

        public override double BepaalWerkbelasting()
        {
            double werkbelasting = 0.0;
            foreach (VakInschrijving vakinschrijving in vakInschrijvingen)
            {
                werkbelasting += 10;
            }
            return werkbelasting;
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            foreach (VakInschrijving vakInschrijving in vakInschrijvingen)
            {
                if (vakInschrijving.Resultaat is not null)
                {
                    som += (byte)vakInschrijving.Resultaat;
                    aantalCursussen++;
                }
            }
            return som / aantalCursussen;
        }
        public override string GenereerNaamkaartje()
        {
            return $"{Naam} (STUDENT)";
        }
        public void Kwoteer(byte cursusIndex, byte behaaldResulaat)
        {
            if (cursusIndex < 0 || cursusIndex >= vakInschrijvingen.Count || vakInschrijvingen[cursusIndex] is null || behaaldResulaat > 20)
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
            vakInschrijvingen.Add(new VakInschrijving(cursus, behaaldResultaat));
        }
        public void ToonOverzicht()
        {
            DateTime nu = DateTime.Now;
            int aantalJaar = nu.Year - this.Geboortedatum.Year - 1;
            if (nu.Month > this.Geboortedatum.Month || nu.Month == this.Geboortedatum.Month && nu.Day >= this.Geboortedatum.Day)
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
            Cursus communicatie = new Cursus("Communicatie", new List<Student>());
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new List<Student>(), 6);
            Cursus databanken = new Cursus("Databanken", new List<Student>(), 5);

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
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new List<Student>(), 6);
            Cursus databanken = new Cursus("Databanken", new List<Student>(), 5);
            Console.WriteLine("Geef de tekstvoorstelling van 1 student in CSV-formaat:");
            string csvWaarde = Console.ReadLine();
            Student student = Student.StudentUitTekstFormaat(csvWaarde);
            student.ToonOverzicht();
        }
    }
}