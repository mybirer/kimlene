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
    public partial class Kisiler : Form
    {
        DataTable kisiListe = new DataTable();
        public Kisiler()
        {
            InitializeComponent();
            refreshListData(null, null);
        }
        
        private void refreshListData(object sender, EventArgs e)
        {
            try
            {
                kisiListe = GlobalClass.executeSqlQuery("SELECT * FROM kisiler", GlobalClass.SQLConn);
                BindingSource bs = new BindingSource();
                bs.DataSource = kisiListe;
                dataGridView1.DataSource = bs;
                dataGridView1.ClearSelection();
                dataGridView1.Refresh();

                setColumnOptions(dataGridView1);
            }
            catch (Exception exx)
            {
                MessageBox.Show("Hata:\n" + exx);
            }
        }
        

        private void notlarıGörToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notlariGor(dataGridView1);
        }
        private void notlariGor(DataGridView GV)
        {
            if (GV.CurrentRow != null)
            {
                string[][] parameters = new string[1][] { new string[2] { "pkkisi", GV.CurrentRow.Cells["pkkisi"].Value.ToString() } };
                NotEkle notEkle = new NotEkle(new List<string[]>(parameters));
                if (notEkle != null)
                {
                    notEkle.Show();
                    notEkle.FormClosed += refreshListData;
                }
            }
            else
            {
                MessageBox.Show("İşlemi gerçekleştirebilmek için lütfen seçim yapınız\n", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var GV = dataGridView1;
            if (GV.CurrentRow != null)
            {
                string[][] parameters = new string[1][] { new string[2] { "pkkisi", GV.CurrentRow.Cells["pkkisi"].Value.ToString() } };
                KisiEkle kisiEkle = new KisiEkle(new List<string[]>(parameters));
                if (kisiEkle != null)
                {
                    kisiEkle.Show();
                    kisiEkle.FormClosed += refreshListData;
                }
            }
            else
            {
                MessageBox.Show("İşlemi gerçekleştirebilmek için lütfen seçim yapınız\n", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var GV = dataGridView1;
                if (GV.SelectedRows.Count > 1)
                {
                    if (MessageBox.Show("Seçili kayıtları silmek istediğinizden emin misiniz?", "Onayla", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int i;
                        for (i = 0; i < GV.SelectedRows.Count; i++)
                        {
                            if (GV.SelectedRows[i] != null)
                            {
                                string pkkisi = GV.SelectedRows[i].Cells["pkkisi"].Value.ToString();
                                GlobalClass.executeSqlQuery(string.Format("DELETE FROM kisiler WHERE pkkisi='{0}'", pkkisi), GlobalClass.SQLConn);
                            }
                        }
                        refreshListData(null, null);
                    }
                }
                else
                {
                    if (GV.CurrentRow != null)
                    {
                        string pkkisi = GV.CurrentRow.Cells["pkkisi"].Value.ToString();
                        if (MessageBox.Show("Seçili kaydı silmek istediğinizden emin misiniz?", "Onayla", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            GlobalClass.executeSqlQuery(string.Format("DELETE FROM kisiler WHERE pkkisi='{0}'", pkkisi), GlobalClass.SQLConn);
                            refreshListData(null, null);
                        }
                    }
                    else
                    {
                        MessageBox.Show("İşlemi gerçekleştirebilmek için lütfen seçim yapınız\n", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show("Hata:\n" + exx);
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshListData(null, null);
        }

        private void ogrenciEkleBtn_Click_1(object sender, EventArgs e)
        {
            KisiEkle kisiEkle = new KisiEkle(new List<string[]>());
            kisiEkle.Show();
            kisiEkle.FormClosed += refreshListData;
        }


        private void setColumnOptions(DataGridView GV)
        {
            try
            {
                GV.Columns["adSoyad"].HeaderText = "Ad Soyad";
                GV.Columns["hakkinda"].HeaderText = "Hakkında";
                GV.Columns["kayitTarihi"].HeaderText = "Kayıt Tarihi";

                GV.Columns["pkkisi"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:\n" + ex);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            notlariGor(dataGridView1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            notlariGor(dataGridView1);
        }
    }
}
