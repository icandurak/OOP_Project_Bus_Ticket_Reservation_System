using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtobusRezervasyonProje
{
    public class Koltuk
    {
        public int Numara { get; set; }
        public bool DoluMu { get; set; }
        public string Cinsiyet { get; set; }
    }

    public class Otobus
    {
        public List<Koltuk> Koltuklar { get; set; }

        public Otobus()
        {
            Koltuklar = new List<Koltuk>();

            
            for (int i = 1; i <= 25; i++)
            {
                Koltuklar.Add(new Koltuk
                {
                    Numara = i,
                    DoluMu = false,
                    Cinsiyet = null
                });
            }
        }

        public Koltuk KoltukGetir(int numara)
        {
            for (int i = 0; i < Koltuklar.Count; i++)
            {
                if (Koltuklar[i].Numara == numara)
                {
                    return Koltuklar[i];
                }
            }
            return null;
        }
    }

    public class Sefer
    {
        public string Nereden { get; set; }
        public string Nereye { get; set; }
        public DateTime Tarih { get; set; }
        public string Saat { get; set; }
        public int Ucret { get; set; }
    }

    public class SehirlerArasiSefer : Sefer
    {
        public bool MolaVarMi { get; set; }
    }
}
