using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobusRezervasyonProje
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Koltuk_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button;
            secilenkoltuk.Text = tiklanan.Text;
        }



        private void satısyapb_Click(object sender, EventArgs e)
        {
            string secilen = secilenkoltuk.Text;

            if (string.IsNullOrWhiteSpace(secilen))
            {
                MessageBox.Show("Lütfen bir koltuk seçin.");
                return;
            }

            foreach (Control ctrl in groupBoxKoltuklar.Controls)
            {
                if (ctrl is Button btn && btn.Text == secilen)
                {
                    if (rbkadin.Checked)
                        btn.BackColor = Color.Pink;
                    else if (rberkek.Checked)
                        btn.BackColor = Color.LightBlue;
                    else
                    {
                        MessageBox.Show("Lütfen cinsiyet seçin.");
                        return;
                    }

                    btn.Enabled = false;
                    secilenkoltuk.Clear();
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in groupBoxKoltuklar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Click += Koltuk_Click;
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
