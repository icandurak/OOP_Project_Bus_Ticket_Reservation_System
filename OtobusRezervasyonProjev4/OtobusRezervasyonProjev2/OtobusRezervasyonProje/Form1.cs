using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace OtobusRezervasyonProje
{
    public partial class Form1: Form
    {

 
        private Otobus otobusGidis;
        private Otobus otobusDonus;

        private Otobus otobus
        {
            get
            {
                if (tersYon)
                    return otobusDonus;
                else
                    return otobusGidis;
            }
        }


        private SehirlerArasiSefer aktifSefer; 


        public bool tersYon = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Koltuk_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button; //tiklanan butonu tutar
            secilenkoltuk.Text = tiklanan.Text;
        }



        private void satısyapb_Click(object sender, EventArgs e)
        {
            string guzergah = "";

            if (Guzergah.SelectedItem != null)
            {
                guzergah = Guzergah.SelectedItem.ToString();
            }

            if (guzergah == null || guzergah.Trim() == "")
            {
                MessageBox.Show("Lütfen bir güzergah seçin.");
                return;
            }

            string secilen = secilenkoltuk.Text;

            if (secilen == null || secilen.Trim() == "")
            {
                MessageBox.Show("Lütfen bir koltuk seçin.");
                return;
            }

            if (!int.TryParse(secilen, out int secilenKoltukNo)) //string koltuk numarasi komple int mi diye kontrol etme a3 vb
            {                                                    //eger komple intsa ikinci parametreye yazilir yoksa birsey atamaz
                MessageBox.Show("Geçersiz koltuk numarası.");
                return;
            }

 
            Koltuk koltuk = otobus.KoltukGetir(secilenKoltukNo);
            if (koltuk == null || koltuk.DoluMu)
            {
                MessageBox.Show("Seçilen koltuk dolu veya geçersiz.");
                return;
            }

            string cinsiyet = "";

            if (rbkadin.Checked) cinsiyet = "Kadın"; 
            else if (rberkek.Checked) cinsiyet = "Erkek";
            else
            {
                MessageBox.Show("Lütfen cinsiyet seçin.");
                return;
            }

            // Tarih ve saat kontrolleri

            string tarih = tarıhmasked.Text;

            string saat;
            if (sefersaatlerı.SelectedItem != null)
                saat = sefersaatlerı.SelectedItem.ToString();
            else
                saat = "";

            if (!DateTime.TryParse(tarih + " " + saat, out DateTime secilenTarihSaat))
            {
                MessageBox.Show("Geçerli bir tarih ve saat girin.");
                return;
            }
            if (secilenTarihSaat < DateTime.Now)
            {
                MessageBox.Show("Geçmiş bir tarih ve saat seçilemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            aktifSefer.Nereden = lblNereden.Text;
            aktifSefer.Nereye = lblNereye.Text;
            aktifSefer.Tarih = secilenTarihSaat.Date;
            aktifSefer.Saat = saat;

            koltuk.DoluMu = true;
            koltuk.Cinsiyet = cinsiyet;

            
            foreach (Control ctrl in groupBoxKoltuklar.Controls)
            {
                if (ctrl is Button btn && btn.Text == secilen)  //secilen buton mu kontrol etme ve buton uzerindeki ile secilen ayni mi
                {
                    btn.BackColor = cinsiyet == "Kadın" ? Color.Pink : Color.LightBlue;
                    btn.Enabled = false;
                    break;
                }
            }

            
            string dosyayolu = tersYon
                ? @"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler_ters.txt"
                : @"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler.txt";

            string ad = adyerı.Text;
            string soyad = soyadyerı.Text;
            string tel = telnomasked.Text;

            string yazilacak = ad + " " + soyad + " Koltuk No:" + secilen + " " + guzergah + " " + tarih + " " + saat + " Cep no:" + tel + " " + cinsiyet;


            try
            {
                using (StreamWriter sw = new StreamWriter(dosyayolu, true))
                {
                    sw.WriteLine(yazilacak);
                }
                MessageBox.Show("Bilet başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            secilenkoltuk.Clear();
        

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            otobusGidis = new Otobus();
            otobusDonus = new Otobus();
            aktifSefer = new SehirlerArasiSefer();

            
            tersYon = false;

            
            Guzergah.Items.Clear();
            Guzergah.Items.Add("Elazığ-->Hatay");
            Guzergah.Items.Add("Elazığ-->Gaziantep");
            Guzergah.Items.Add("Elazığ-->Şanlıurfa");
            Guzergah.Items.Add("Elazığ-->Malatya");

            lblNereden.Text = "Elazığ";
            lblNereye.Text = "Hatay";

            // Koltuk butonlarini hazirlama
            foreach (Control ctrl in groupBoxKoltuklar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Click += Koltuk_Click; //butonlari koltuk_click metoduna baglar
                    btn.Enabled = true;
                    btn.BackColor = Color.White;
                }
            }

            // Her iki yön için dolu koltukları yükleme 

            // Gidiş yönü için
            DoluKoltuklariYukle(@"C: \Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler.txt", otobusGidis);

            // Dönüş yönü için
            DoluKoltuklariYukle(@"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler_ters.txt", otobusDonus);


            // Ekrana ilk başta gidiş yönü (tersYon = false) yansıyacağı için
            KoltuklariGuncelle();
        


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UcretHesapla()
        {
            if (!int.TryParse(yasalanı.Text, out int yas)) //gecersiz yas girilirse
            {
                Ucret.Clear();
                return;
            }

            string guzergah = "";

            if (Guzergah.SelectedItem != null)
            {
                guzergah = Guzergah.SelectedItem.ToString();
            }

            if (guzergah == null || guzergah == "")
            {
                Ucret.Clear();
                return;
            }

            int ucret = 0;

            if (guzergah == "Elazığ-->Hatay" || guzergah == "Hatay-->Elazığ")
            {
                if (yas < 12)
                    ucret = 300;
                else if (yas >= 60)
                    ucret = 400;
                else if (yas >= 18 && yas <= 25)
                    ucret = 450; // Öğrenci fiyatı
                else
                    ucret = 500;
            }
            else if (guzergah == "Elazığ-->Gaziantep" || guzergah == "Gaziantep-->Elazığ")
            {
                if (yas < 12)
                    ucret = 250;
                else if (yas >= 60)
                    ucret = 350;
                else if (yas >= 18 && yas <= 25)
                    ucret = 400;
                else
                    ucret = 450;
            }
            else if (guzergah == "Elazığ-->Şanlıurfa" || guzergah == "Şanlıurfa-->Elazığ")
            {
                if (yas < 12)
                    ucret = 200;
                else if (yas >= 60)
                    ucret = 300;
                else if (yas >= 18 && yas <= 25)
                    ucret = 350;
                else
                    ucret = 400;
            }
            else if (guzergah == "Elazığ-->Malatya" || guzergah == "Malatya-->Elazığ")
            {
                if (yas < 12)
                    ucret = 100;
                else if (yas >= 60)
                    ucret = 150;
                else if (yas >= 18 && yas <= 25)
                    ucret = 180;
                else
                    ucret = 200;
            }


            Ucret.Text = ucret.ToString() + "₺";
        }


        private void textBox1_TextChanged(object sender, EventArgs e) //yas her degistiginde ona gore ucret 
        {
            UcretHesapla();
        }

        private void Guzergah_SelectedIndexChanged(object sender, EventArgs e) //guzergah degisince fiyat degis
        {
            UcretHesapla();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void yondegistir_Click(object sender, EventArgs e)
        {
         
            tersYon = !tersYon;

            string temp = lblNereden.Text;
            lblNereden.Text = lblNereye.Text;
            lblNereye.Text = temp;

          
            Guzergah.Items.Clear();
            if (!tersYon)
            {
                Guzergah.Items.Add("Elazığ-->Hatay");
                Guzergah.Items.Add("Elazığ-->Gaziantep");
                Guzergah.Items.Add("Elazığ-->Şanlıurfa");
                Guzergah.Items.Add("Elazığ-->Malatya");
            }
            else
            {
                Guzergah.Items.Add("Hatay-->Elazığ");
                Guzergah.Items.Add("Gaziantep-->Elazığ");
                Guzergah.Items.Add("Şanlıurfa-->Elazığ");
                Guzergah.Items.Add("Malatya-->Elazığ");
            }

            
            secilenkoltuk.Clear();
            Guzergah.SelectedIndex = -1; //guzergah yerini bos yapar 0 olursa ilk eleman gozukur cunku
            Ucret.Clear();
            adyerı.Clear();
            soyadyerı.Clear();
            yasalanı.Clear();
            telnomasked.Clear();
            rberkek.Checked = false;
            rbkadin.Checked = false;


           
            foreach (Control ctrl in groupBoxKoltuklar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Enabled = true;
                    btn.BackColor = Color.White;
                }
            }

        
            foreach (var k in otobus.Koltuklar) //tum koltuklar icin koltuklari bos yapar sonra dosya bilgileri girince guncelleme yapilir
            {
                k.DoluMu = false;
                k.Cinsiyet = null;
            }

            
            string dosyaYolu = tersYon
                ? @"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler_ters.txt"
                : @"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler.txt";

            if (File.Exists(dosyaYolu))
            {
                DoluKoltuklariYukle(dosyaYolu, otobus);
            }

         
            KoltuklariGuncelle();
        

        

        }

        public void DoluKoltuklariYukle(string dosyaYolu, Otobus otobus)
        {
            if (!File.Exists(dosyaYolu))
                return;

            var satirlar = File.ReadAllLines(dosyaYolu); //tum satirlar okunur satirlar dizisine atilir her satir bir yolcu
            foreach (var satir in satirlar)
            {
                int koltukNo = -1;
                string cinsiyet = "";

                int koltukNoIndex = satir.IndexOf("Koltuk No:"); //koltuk nonun oldugu yeri arar
                if (koltukNoIndex != -1)
                {
                    int start = koltukNoIndex + "Koltuk No:".Length; //koltuk nonun basladigi yer
                    int end = satir.IndexOf(' ', start); //bir bosluk atlariz cunku arada bosluk var numara ile
                    string noStr = (end == -1) ? satir.Substring(start).Trim() : satir.Substring(start, end - start).Trim(); //eger bosluk yoksa
                    if (int.TryParse(noStr, out int num))                                                          //satirin kalanini alir 
                    {                                         //sayiya cevirmeye calisir olursa  koltuk no olarak alinir
                        koltukNo = num;
                    }
                }

                if (satir.EndsWith("Kadın")) //satirin sonunda kadin yaziyorsa kadin yaapr
                    cinsiyet = "Kadın";
                else if (satir.EndsWith("Erkek"))
                    cinsiyet = "Erkek";

                if (koltukNo != -1 && !string.IsNullOrEmpty(cinsiyet)) //koltuk dolu ve cinsiyet bos degilse 
                {
                    var koltuk = otobus.KoltukGetir(koltukNo);  //ilgili koltuk bulma
                    if (koltuk != null)
                    {
                        koltuk.DoluMu = true;
                        koltuk.Cinsiyet = cinsiyet;
                    }
                }
            }
        }



        public void KoltuklariGuncelle()
        {
            foreach (Control ctrl in groupBoxKoltuklar.Controls)
            {
                if (ctrl is Button btn)
                {
                    int koltukNo;
                    if (int.TryParse(btn.Text, out koltukNo)) //buton uzerindeki yaxziyi saiya cevirme
                    {
                        var koltuk = otobus.KoltukGetir(koltukNo); //koltuk otobus nesnesinden bulunur
                        if (koltuk != null)
                        {
                            if (koltuk.DoluMu)
                            {
                                btn.Enabled = false;
                                btn.BackColor = koltuk.Cinsiyet == "Kadın" ? Color.Pink :
                                                koltuk.Cinsiyet == "Erkek" ? Color.LightBlue : Color.Gray;
                            }
                            else
                            {
                                btn.Enabled = true;
                                btn.BackColor = Color.White;
                            }
                        }
                    }
                }
            }
        }







        private void geridonme_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }
    }

}
