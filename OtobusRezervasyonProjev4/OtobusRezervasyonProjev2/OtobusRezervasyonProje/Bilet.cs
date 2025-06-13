using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobusRezervasyonProje
{
    public class Bilet
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Tel { get; set; }
        public Koltuk Koltuk { get; set; }
        public Sefer Sefer { get; set; }

        public void BiletYazdır(String dosyayolu,String ad,String soyad,String koltuk, String guzergah,String tarih, String saat, String tel )
        {


            string yazilacak = (ad + "," + soyad + "," + "Koltuk No:" + koltuk + "," + guzergah + "," + tarih + "," + saat + "," + "Cep no:" + tel);

            try
            {
                using (StreamWriter sw = new StreamWriter(dosyayolu, true))
                {
                    sw.WriteLine(yazilacak);
                }

                MessageBox.Show("Bilet başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Bilet başarıyla kaydedildi!"); // Konsola da yazdırma
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void TersBiletYazdır(string dosyayolu2,String ad, String soyad, String koltuk, String guzergah, String tarih, String saat, String tel)
        {

            string yazilacak = (ad + "," + soyad + "," + "Koltuk No:" + koltuk + "," + guzergah + "," + tarih + "," + saat + "," + "Cep no:" + tel);

            try
            {
                using (StreamWriter sw = new StreamWriter(dosyayolu2, true))
                {
                    sw.WriteLine(yazilacak);
                }

                MessageBox.Show("Bilet başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Bilet başarıyla kaydedildi!"); // Konsola da yazdırma
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }

}
