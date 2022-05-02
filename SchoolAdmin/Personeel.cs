using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdmin
{

    abstract class Personeel : Persoon
    {
        private byte ancienniteit;
        public byte Ancienniteit
        {
            get
            {
                return this.ancienniteit;
            }
            set
            {
                byte maximum = 50;
                this.ancienniteit = Math.Min(maximum, value);
            }
        }

        public Dictionary<string, byte> taken;

        public ImmutableDictionary<string, byte> Taken
        {
            get
            {
                var builder = ImmutableDictionary.CreateBuilder<string, byte>();
                foreach (KeyValuePair<string, byte> pair in taken)
                {
                    builder.Add(pair);
                }
                return builder.ToImmutableDictionary<string, byte>();
            }
        }

        private static List<Personeel> allePersoneel = new List<Personeel>();

        public static ImmutableList<Personeel> AllePersoneel
        {
            get
            {
                return allePersoneel.ToImmutableList<Personeel>();
            }
        }

        public Personeel(string naam, DateTime geboortedatum, Dictionary<string, byte> taken) : base(naam, geboortedatum)
        {
            this.taken = new Dictionary<string, byte>();
            if (!(taken is null))
            {
                foreach (var paar in taken)
                {
                    this.taken.Add(paar.Key, paar.Value);
                }
            }
            allePersoneel.Add(this);
        }
        public abstract uint BerekenSalaris();
    }
}