﻿using System;

namespace SchoolAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            int keuze = 0;
            Console.WriteLine($"Wat wil je demonstreren?\n\t1. Studenten\n\t2. Cursussen\n\t3. Student Uit tekst\n\t4. StudieProgramma\n\t5. Administratief Personeel\n\t6. Lector\n");
            keuze = Convert.ToInt32(Console.ReadLine());
            if (keuze == 1)
            {
                Student.DemonstreerStudenten();
            }
            else if (keuze == 2)
            {
                Cursus.DemonstreerCursussen();
            }
            else if (keuze == 3)
            {
                Student.DemonstreerStudentUitTekstFormaat();
            }
            else if (keuze == 4)
            {
                StudieProgramma.DemonstreerStudieProgrmma();
            }
            else if (keuze == 5)
            {
                AdministratiefPersoneel.DemonstreerAdministratiefPersoneel();
            }
            else if (keuze == 6)
            {
                Lector.DemonstreerLectoren();
            }
        }
    }
}
