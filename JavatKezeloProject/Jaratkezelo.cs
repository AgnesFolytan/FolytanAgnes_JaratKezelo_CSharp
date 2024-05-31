using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaratKezeloProject
{
    public class Jarat
    {
        private string jaratSzam;
        private string repterHonnan;
        private string repterHova;
        private DateTime indulas;
        private int keses;
        public Jarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
        {
            this.JaratSzam = jaratSzam;
            this.RepterHonnan = repterHonnan;
            this.RepterHova = repterHova;
            this.Indulas = indulas;
            this.Keses = 0;
        }

        public string JaratSzam { get => jaratSzam; set => jaratSzam = value; }
        public string RepterHonnan { get => repterHonnan; set => repterHonnan = value; }
        public string RepterHova { get => repterHova; set => repterHova = value; }
        public DateTime Indulas { get => indulas; set => indulas = value; }
        public int Keses { get => keses; set => keses = value; }
    }

    public class NegativKesesException: Exception
    {
        public NegativKesesException(): base("Nem indulhat hamarabb a Járat!")
        {

        }
    }
    public class Jaratkezelo
    {
        private List<Jarat> jaratok;

        public Jaratkezelo()
        {
            this.Jaratok = new List<Jarat>();
        }

        public List<Jarat> Jaratok { get => jaratok; set => jaratok = value; }

        public void ujJarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
        {
            if (jaratSzam == null)
            {
                throw new ArgumentNullException(nameof(jaratSzam));
            }
            if (jaratSzam == "")
            {
                throw new ArgumentException("A járatszám nem lehet üres", nameof(jaratSzam));
            }
            if (repterHonnan == null)
            {
                throw new ArgumentNullException(nameof(repterHonnan));
            }
            if (repterHonnan == "")
            {
                throw new ArgumentException("A Reptér indulási helyszíne nem lehet üres", nameof(repterHonnan));
            }
            if (repterHova == null)
            {
                throw new ArgumentNullException(nameof(repterHova));
            }
            if (repterHova == "")
            {
                throw new ArgumentException("A Reptér érkezési helyszíne nem lehet üres", nameof(repterHova));
            }

            int index = 0;
            while (index < jaratok.Count && jaratok[index].JaratSzam != jaratSzam)
            {
                index++;
            }
            if (index < jaratok.Count)
            {
                throw new ArgumentException("A járastzámmal már létezik járat", nameof(jaratSzam));
            }

            Jarat jarat = new Jarat(jaratSzam, repterHonnan, repterHova, indulas);
            jaratok.Add(jarat);
        }

        public void keses(string jaratSzam, int keses)
        {
            Jarat jarat = jaratok.Where(x => x.JaratSzam == jaratSzam).First();

            if (jarat.Keses + keses >= 0)
            {
                jarat.Keses += keses;
            }
            else
            {
                throw new NegativKesesException();
            }
        }

        public DateTime mikorIndul(string jaratSzam)
        {
            Jarat jarat = jaratok.Where(x => x.JaratSzam == jaratSzam).First();

            DateTime indulas = jarat.Indulas + TimeSpan.FromMinutes(jarat.Keses);

            return indulas;
        }

        public List<string> jaratokRepuloterrol(string repter)
        {
            return jaratok.Where(x => x.RepterHonnan == repter).Select(y => y.JaratSzam).ToList();
        }

    }
}
