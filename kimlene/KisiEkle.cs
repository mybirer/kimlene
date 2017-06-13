using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kimlene
{
    public partial class KisiEkle : Form
    {
        Boolean editMode = false;
        string editPk;
        string[][] parametreler;

        public KisiEkle(List<string[]> parameters)
        {
            InitializeComponent();
            if (parameters.Count != 0)
            {
                editMode = true;
                foreach (var prm in parameters)
                {
                    if (prm[0].Equals("pkkisi") && !prm[1].Equals(null))
                    {
                        editPk = prm[1];
                        parametreler = new string[1][] {
                            new string[2] { "pkkisi", editPk }
                        };
                        DataTable retObj = GlobalClass.executeSqlQuery("SELECT * FROM kisiler WHERE pkkisi=@pkkisi", GlobalClass.SQLConn, parametreler);
                        KisiEkleAdSoyad.Text = retObj.Rows[0]["adSoyad"].ToString();
                        KisiEkleHakkinda.Text = retObj.Rows[0]["hakkinda"].ToString();
                    }
                }
            }
        }

        private void tbKaydet_Click(object sender, EventArgs e)
        {
            if (KisiEkleAdSoyad.Text.Trim() == "")
            {
                MessageBox.Show("Ad boş olamaz!\n", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    if (editMode)
                    {
                        parametreler = new string[2][] {
                            new string[2] { "adSoyad", KisiEkleAdSoyad.Text.Trim() },
                            new string[2] { "pkkisi", editPk }
                        };
                        DataTable countTable = GlobalClass.executeSqlQuery("SELECT pkkisi FROM kisiler WHERE adSoyad=@adSoyad AND pkkisi!=@pkkisi", GlobalClass.SQLConn, parametreler);
                        if (countTable.Rows.Count > 0)
                        {
                            MessageBox.Show("Aynı adda bir kişi zaten mevcut! Lütfen başka bir ad giriniz\n", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        parametreler = new string[3][] {
                            new string[2] { "adSoyad", KisiEkleAdSoyad.Text.Trim() },
                            new string[2] { "hakkinda", KisiEkleHakkinda.Text.Trim() },
                            new string[2] { "pkkisi", editPk }
                        };
                        GlobalClass.executeSqlQuery("UPDATE kisiler SET adSoyad=@adSoyad,hakkinda=@hakkinda WHERE pkkisi=@pkkisi", GlobalClass.SQLConn, parametreler);
                        
                    }
                    else
                    {
                        parametreler = new string[1][] {
                            new string[2] { "adSoyad", KisiEkleAdSoyad.Text.Trim() }
                        };
                        DataTable countTable = GlobalClass.executeSqlQuery("SELECT pkkisi FROM kisiler WHERE adSoyad=@adSoyad", GlobalClass.SQLConn, parametreler);
                        if (countTable.Rows.Count > 0)
                        {
                            MessageBox.Show("Aynı adda bir kişi zaten mevcut! Lütfen başka bir ad giriniz\n", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        parametreler = new string[2][] {
                            new string[2] { "adSoyad", KisiEkleAdSoyad.Text.Trim() },
                            new string[2] { "hakkinda", KisiEkleHakkinda.Text.Trim() }
                        };
                        GlobalClass.executeSqlQuery("INSERT INTO kisiler (adSoyad,hakkinda) VALUES (@adSoyad,@hakkinda)", GlobalClass.SQLConn, parametreler);
                        
                    }
                    this.Close();
                }
                catch (Exception exx)
                {
                    MessageBox.Show("Hata:\n" + exx);
                }
                return;
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
