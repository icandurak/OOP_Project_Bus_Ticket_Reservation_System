using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtobusRezervasyonProje
{
    public class Otobus
    {
        public List<Koltuk> Koltuklar { get; set; }

        public Otobus()
        {
            Koltuklar = new List<Koltuk>();
            for (int i = 1; i <= 25; i++)
            {
                Koltuklar.Add(new Koltuk { Numara = i, DoluMu = false });
            }
        }

        public Koltuk KoltukGetir(int numara) =>
            Koltuklar.FirstOrDefault(k => k.Numara == numara);
    }

}
