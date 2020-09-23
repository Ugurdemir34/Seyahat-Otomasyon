using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Seyahat_v1._00.ADMINPANEL.ISLEMLER
{
    public partial class Kullanici_Islem : Form
    {
        public Kullanici_Islem()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        void bul()
        {
            try
            {
                bag.Open();
                string arama = "SELECT * FROM KULLANICI WHERE K_ad LIKE '" + textBox1.Text + "%'";
                OleDbDataAdapter adaptor = new OleDbDataAdapter(arama, bag); //adaptör oluşturuyoruz.
                kullanicigetir(arama);
                bag.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
            }
        }
        void Kullanici_Sil()
        {
            try
            {
                if (textBox3.Text.Trim() == "")
                {
                    MessageBox.Show("ID Seçiniz !");
                }
                else
                {
                    DialogResult a = new DialogResult();
                    a = MessageBox.Show("Silmek İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (a == DialogResult.Yes)
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "DELETE FROM KULLANICI WHERE K_ad ='" + textBox3.Text + "'";
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Bilet Silindi !");
                        string sorgu = "SELECT * FROM KULLANICI";
                        kullanicigetir(sorgu);
                        bag.Close();
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata meydana geldi : " + hata.Message);
            }
        }
        private void otoboyut(ListView lvw, int width)
        {
            foreach (ColumnHeader col in lvw.Columns)
                col.Width = width;
        }
        void kullanicigetir(string sorgu)
        {
            listView3.View = View.Details;
            listView3.Items.Clear();
            listView3.Columns.Clear();
            OleDbDataAdapter ada = new OleDbDataAdapter(sorgu, bag);
            DataTable dt = new DataTable();
            dt.Clear();
            ada.Fill(dt);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn kolonlar = dt.Columns[i];
                listView3.Columns.Add(Convert.ToString(kolonlar));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr[0].ToString());
                listitem.SubItems.Add(dr[1].ToString());
                listitem.SubItems.Add(dr[2].ToString());               
                listView3.Items.Add(listitem);
            }
            otoboyut(listView3,listView3.Width/3);
            ada.Dispose();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Kullanici_Sil();
        }
        private void Kullanici_Islem_Load(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM KULLANICI";
            kullanicigetir(sorgu);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count > 0)
            {
                textBox3.Text = listView3.SelectedItems[0].Text;
            }
        }
        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.Size = new Size(35, 30);
        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.Size = new Size(30, 25);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                string sorgu = "SELECT * FROM KULLANICI";
                kullanicigetir(sorgu);
            }
            else
            {
                bul();
            }
        }
    }
}