using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class CursusResultaat
    {
        private string naam;
        public string Naam
        {
            get 
            { 
                return naam; 
            }
        }
        private byte resultaat;
        public byte Resultaat
        {
            get 
            { 
                return resultaat; 
            }
            set 
            { 
                if (!(value < 0 || value > 20))
                {
                    resultaat = value; 
                }
            }
        }
        public CursusResultaat(string naam, byte resultaat)
        {
            this.naam = naam;
            this.Resultaat = resultaat;
        }
    }
}