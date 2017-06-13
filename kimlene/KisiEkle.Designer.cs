namespace kimlene
{
    partial class KisiEkle
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KisiEkleAdSoyad = new System.Windows.Forms.TextBox();
            this.KisiEkleHakkinda = new System.Windows.Forms.RichTextBox();
            this.tbKaydet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ad Soyad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hakkında";
            // 
            // KisiEkleAdSoyad
            // 
            this.KisiEkleAdSoyad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KisiEkleAdSoyad.Location = new System.Drawing.Point(71, 18);
            this.KisiEkleAdSoyad.Name = "KisiEkleAdSoyad";
            this.KisiEkleAdSoyad.Size = new System.Drawing.Size(239, 20);
            this.KisiEkleAdSoyad.TabIndex = 2;
            // 
            // KisiEkleHakkinda
            // 
            this.KisiEkleHakkinda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KisiEkleHakkinda.Location = new System.Drawing.Point(71, 44);
            this.KisiEkleHakkinda.Name = "KisiEkleHakkinda";
            this.KisiEkleHakkinda.Size = new System.Drawing.Size(239, 117);
            this.KisiEkleHakkinda.TabIndex = 3;
            this.KisiEkleHakkinda.Text = "";
            // 
            // tbKaydet
            // 
            this.tbKaydet.Location = new System.Drawing.Point(235, 167);
            this.tbKaydet.Name = "tbKaydet";
            this.tbKaydet.Size = new System.Drawing.Size(75, 23);
            this.tbKaydet.TabIndex = 4;
            this.tbKaydet.Text = "Kaydet";
            this.tbKaydet.UseVisualStyleBackColor = true;
            this.tbKaydet.Click += new System.EventHandler(this.tbKaydet_Click);
            // 
            // KisiEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 214);
            this.Controls.Add(this.tbKaydet);
            this.Controls.Add(this.KisiEkleHakkinda);
            this.Controls.Add(this.KisiEkleAdSoyad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KisiEkle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Kişi Ekle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KisiEkleAdSoyad;
        private System.Windows.Forms.RichTextBox KisiEkleHakkinda;
        private System.Windows.Forms.Button tbKaydet;
    }
}