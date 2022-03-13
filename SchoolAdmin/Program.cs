using System;

namespace SchoolAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            int keuze = 0;
            Console.WriteLine($"Wat wil je demonstreren?\n\t1. Studenten\n");
            keuze = Convert.ToInt32(Console.ReadLine());
            if (keuze == 1)
            {
                Student.DemonstreerStudenten();
            }
        }
    }
}
