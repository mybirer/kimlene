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
    public partial class NotEkle : Form
    {
        Boolean editMode = false;
        string editPk;
        string[][] parametreler;

        public NotEkle(List<string[]> parameters)
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
                        label1.Text = retObj.Rows[0]["adSoyad"].ToString();

                        refreshListData(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir isim seçiniz!\n", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void refreshListData(object sender, EventArgs e)
        {
            try
            {
                parametreler = new string[1][] {
                            new string[2] { "pkkisi", editPk }
                        };
                DataTable retObj = GlobalClass.executeSqlQuery("SELECT * FROM notlar WHERE pkkisi=@pkkisi", GlobalClass.SQLConn, parametreler);
                
                BindingSource bs = new BindingSource();
                bs.DataSource = retObj;
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
                                string pknot = GV.SelectedRows[i].Cells["pknot"].Value.ToString();
                                GlobalClass.executeSqlQuery(string.Format("DELETE FROM notlar WHERE pknot='{0}'", pknot), GlobalClass.SQLConn);
                            }
                        }
                        refreshListData(null, null);
                    }
                }
                else
                {
                    if (GV.CurrentRow != null)
                    {
                        string pknot = GV.CurrentRow.Cells["pknot"].Value.ToString();
                        if (MessageBox.Show("Seçili kaydı silmek istediğinizden emin misiniz?", "Onayla", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            GlobalClass.executeSqlQuery(string.Format("DELETE FROM notlar WHERE pknot='{0}'", pknot), GlobalClass.SQLConn);
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

        private void tbKaydet_Click(object sender, EventArgs e)
        {

            if (NotEkleNotMetni.Text.Trim() == "")
            {
                MessageBox.Show("Not boş olamaz!\n", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    if (editMode)
                    {
                        parametreler = new string[3][] {
                            new string[2] { "pkkisi", editPk },
                            new string[2] { "notMetin", NotEkleNotMetni.Text.Trim() },
                            new string[2] { "notYontem", NotEkleYontem.Text.Trim() }
                        };
                        GlobalClass.executeSqlQuery("INSERT INTO notlar (pkkisi,notMetin,notYontem) VALUES (@pkkisi,@notMetin,@notYontem)", GlobalClass.SQLConn, parametreler);
                        NotEkleNotMetni.Text = "";
                        refreshListData(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir isim seçiniz!\n", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                }
                catch (Exception exx)
                {
                    MessageBox.Show("Hata:\n" + exx);
                }
                return;
            }
        }

        private void setColumnOptions(DataGridView GV)
        {
            try
            {
                GV.Columns["notMetin"].HeaderText = "Not Metni";
                GV.Columns["notYontem"].HeaderText = "Yöntem";
                GV.Columns["kayitTarihi"].HeaderText = "Kayıt Tarihi";

                GV.Columns["pkkisi"].Visible = false;
                GV.Columns["pknot"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:\n" + ex);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            NotEkleNotMetni.Text = dataGridView1.CurrentRow.Cells["notMetin"].Value.ToString();
        }
    }
}
