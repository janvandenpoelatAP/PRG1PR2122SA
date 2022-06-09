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
            /*
            DateTime nu = DateTime.Now;
            int aantalJaar = nu.Year - this.Geboortedatum.Year - 1;
            if (nu.Month > this.Geboortedatum.Month || nu.Month == this.Geboortedatum.Month && nu.Day >= this.Geboortedatum.Day)
            {
                aantalJaar++;
            }
            Console.WriteLine($"{Naam}, {aantalJaar} jaar");
            Console.WriteLine();
            */
            Console.WriteLine(this);
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("**********");
            foreach(var inschrijving in this.VakInschrijvingen) {
                if (!(inschrijving is null))
                {
                    Console.WriteLine($"{inschrijving.Cursus.Titel}:\t{inschrijving.Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public override string ToString()
        {
            return $"{base.ToString()}\nMeerbepaald, student";
        }
        public static void ToonStudenten()
        {
            Console.WriteLine("Toon studenten in:");
            Console.WriteLine("1. Stijgende alfabetische volgorde");
            Console.WriteLine("2. Dalende alfabetische volgorde");
            int keuze = Convert.ToInt32(Console.ReadLine());
            IComparer<Student> comparer = null;
            if (keuze == 1)
            {
                comparer = new StudentVolgensNaamOplopendComparer();
            }
            else if (keuze == 2)
            {
                comparer = new StudentVolgensNaamAflopendComparer();
            }
            else
            {
            }
            ImmutableList<Student> alleStudentenGesorteerd = AlleStudenten.Sort(comparer);
            foreach (Student student in alleStudentenGesorteerd)
            {
                Console.WriteLine(student.ToString());
            }
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
        public static void LeesVanafCommandLine()
        {
            Console.WriteLine("Naam van de student?");
            var naam = Console.ReadLine();
            Console.WriteLine("Geboortedatum van de student?");
            var geboorteDatum = Convert.ToDateTime(Console.ReadLine());
            new Student(naam, geboorteDatum);
        }
    }
}