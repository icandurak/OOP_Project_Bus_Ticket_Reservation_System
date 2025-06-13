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


namespace OtobusRezervasyonProje
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dosyaYolu = @"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler.txt";

            try
            {
                if (File.Exists(dosyaYolu))
                {
                    string[] satirlar = File.ReadAllLines(dosyaYolu);  //tum satirlari tek seferde okur her satir farkli eleman
                    listBox1.Items.Clear(); // Onceden yazilan seyleri siler
                    foreach (string satir in satirlar)
                    {
                        listBox1.Items.Add(satir);
                    }
                }
                else
                {
                    MessageBox.Show("Dosya bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dosyaYolu = @"C:\Users\HP\Desktop\OtobusRezervasyonProjev4\OtobusRezervasyonProjev2\OtobusRezervasyonProje\biletler_ters.txt";

            try
            {
                if (File.Exists(dosyaYolu))
                {
                    string[] satirlar = File.ReadAllLines(dosyaYolu);
                    listBox2.Items.Clear();
                    foreach (string satir in satirlar)
                    {
                        listBox2.Items.Add(satir);
                    }
                }
                else
                {
                    MessageBox.Show("Dosya bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }
    }
}
