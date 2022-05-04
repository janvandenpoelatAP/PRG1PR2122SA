using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Student : Persoon
    {
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
        public ImmutableList<VakInschrijving> VakInschrijvingen
        {
            get
            {
                var enkelVoorDezeStudent = new List<VakInschrijving>();
                foreach (var inschrijving in VakInschrijving.AlleVakInschrijvingen)
                {
                    if (inschrijving.Student.Equals(this))
                    {
                        enkelVoorDezeStudent.Add(inschrijving);
                    }
                }
                return enkelVoorDezeStudent.ToImmutableList<VakInschrijving>();
            }
        }
        public ImmutableList<Cursus> Cursussen
        {
            get
            {
                var cursussen = new List<Cursus>();
                foreach (var inschrijving in this.VakInschrijvingen)
                {
                    cursussen.Add(inschrijving.Cursus);
                }
                return cursussen.ToImmutableList<Cursus>();
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
            foreach (VakInschrijving vakinschrijving in VakInschrijvingen)
            {
                werkbelasting += 10;
            }
            return werkbelasting;
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            foreach (VakInschrijving vakInschrijving in VakInschrijvingen)
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
            if (cursusIndex < 0 || cursusIndex >= VakInschrijvingen.Count || VakInschrijvingen[cursusIndex] is null || behaaldResulaat > 20)
            {
                Console.WriteLine("Ongeldig cijfer!");
            }
            else
            {
               VakInschrijvingen[cursusIndex].Resultaat = behaaldResulaat;
            }
        }
        public void RegistreerCursusResultaat(Cursus cursus, byte? behaaldResultaat)
        {
            new VakInschrijving(this, cursus, behaaldResultaat);
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
            foreach (VakInschrijving vakInschrijving in VakInschrijvingen)
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
                student.RegistreerCursusResultaat(cursus, Convert.ToByte(studentInfo[i + 1]));
            }
            return student;
        }
        public static void DemonstreerStudenten()
        {
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", 6);
            Cursus databanken = new Cursus("Databanken", 5);

            Student student1 = new Student("Said Aziz", new DateTime(2001,1,3));
            student1.RegistreerCursusResultaat(communicatie, 12);
            student1.RegistreerCursusResultaat(programmeren, null);
            student1.RegistreerCursusResultaat(webtechnologie, 13);
            student1.ToonOverzicht();
            
            Student student2 = new Student("Mieke Vermeulen", new DateTime(1998, 1, 1));
            student2.RegistreerCursusResultaat(communicatie, 13);
            student2.RegistreerCursusResultaat(programmeren, null);
            student2.RegistreerCursusResultaat(databanken, 14);
            student2.ToonOverzicht();
        }
        public static void DemonstreerStudentUitTekstFormaat()
        {
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", 6);
            Cursus databanken = new Cursus("Databanken", 5);
            Console.WriteLine("Geef de tekstvoorstelling van 1 student in CSV-formaat:");
            string csvWaarde = Console.ReadLine();
            Student student = Student.StudentUitTekstFormaat(csvWaarde);
            student.ToonOverzicht();
        }
    }
}