namespace OtobusRezervasyonProje
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.biletsatisgecis = new System.Windows.Forms.Button();
            this.yolculistelemebutonu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // biletsatisgecis
            // 
            this.biletsatisgecis.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.biletsatisgecis.Location = new System.Drawing.Point(566, 198);
            this.biletsatisgecis.Name = "biletsatisgecis";
            this.biletsatisgecis.Size = new System.Drawing.Size(288, 41);
            this.biletsatisgecis.TabIndex = 0;
            this.biletsatisgecis.Text = "Bilet Satis Ekrani";
            this.biletsatisgecis.UseVisualStyleBackColor = true;
            this.biletsatisgecis.Click += new System.EventHandler(this.biletsatisgecis_Click);
            // 
            // yolculistelemebutonu
            // 
            this.yolculistelemebutonu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yolculistelemebutonu.Location = new System.Drawing.Point(12, 198);
            this.yolculistelemebutonu.Name = "yolculistelemebutonu";
            this.yolculistelemebutonu.Size = new System.Drawing.Size(275, 41);
            this.yolculistelemebutonu.TabIndex = 1;
            this.yolculistelemebutonu.Text = "Yolcu Listesi";
            this.yolculistelemebutonu.UseVisualStyleBackColor = true;
            this.yolculistelemebutonu.Click += new System.EventHandler(this.yolculistelemebutonu_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OtobusRezervasyonProje.Properties.Resources.FıratBUSLogo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(877, 511);
            this.Controls.Add(this.yolculistelemebutonu);
            this.Controls.Add(this.biletsatisgecis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnaSayfa";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button biletsatisgecis;
        private System.Windows.Forms.Button yolculistelemebutonu;
    }
}