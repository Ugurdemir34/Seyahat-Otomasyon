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
namespace Seyahat_v1._00
{
    public partial class Muavin_Islem : Form
    {
        public Muavin_Islem()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        void muavingetir()
        {
            listView3.View = View.Details;
            listView3.Items.Clear();
            listView3.Columns.Clear();
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM  MUAVINLER", bag);
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
                listitem.SubItems.Add(dr[3].ToString());               
                listView3.Items.Add(listitem);
            }
            otoboyut(listView3, listView3.Width / 4);
            ada.Dispose();
        }
        void kaydet()
        {
            bool kayıtdurumu = false;
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "")
                {
                    kayıtdurumu = false;
                }
                else
                {
                    kayıtdurumu = true;
                }
                if (kayıtdurumu)
                {
                    bag.Open();
                    komut.Connection = bag;
                    komut.CommandText = "INSERT INTO MUAVINLER(Sicil_No,Muavin_Adi,Muavin_Soyadi,Muavin_Gsm) VALUES(@Sicil_No,@Muavin_Adi,@Muavin_Soyadi,@Muavin_Gsm)";
                    komut.Parameters.AddWithValue("@Sicil_No", textBox1.Text);
                    komut.Parameters.AddWithValue("@Muavin_Adi", textBox2.Text);
                    komut.Parameters.AddWithValue("@Muavin_Soyadi", textBox4.Text);
                    komut.Parameters.AddWithValue("@Muavin_Gsm", textBox5.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarılı !");
                    muavingetir();
                    otoboyut(listView3, listView3.Width / 4);
                    bag.Close();
                    bag.Dispose();
                }
                else
                {
                    MessageBox.Show("Bilgilerinizi Doğru ve Eksiksiz Giriniz !");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir Hata Meydana Geldi !\n Hata: " + hata.Message);
            }
        }
        void sil()
        {
            try
            {
                if (textBox3.Text.Trim() == "")
                {
                    MessageBox.Show("Sicil Numarası Giriniz");
                }
                else
                {
                    DialogResult a = new DialogResult();
                    a = MessageBox.Show(" Silmek İstediğinize Emin Misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (a == DialogResult.Yes)
                    {
                        bag.Open();
                        komut = new OleDbCommand("DELETE FROM MUAVINLER WHERE Sicil_No ='" + textBox3.Text + "'", bag);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Silindi");
                        textBox3.Clear();
                        muavingetir();
                        otoboyut(listView3, listView3.Width / 4);
                        bag.Close();
                        bag.Dispose();
                    }
                }
            }
            catch (Exception Hata)
            {

                MessageBox.Show("Bir Hata Meydana Geldi ! Tekrar Deneyiniz\n Hata : " + Hata.Message);
            }
        }
        private void otoboyut(ListView lvw, int width)
        {
            foreach (ColumnHeader col in lvw.Columns)
                col.Width = width;
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count > 0)
            {
                textBox3.Text = listView3.SelectedItems[0].Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void Muavin_Islem_Load(object sender, EventArgs e)
        {
            muavingetir();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            kaydet();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            sil();
        }
        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.Size = new Size(35, 30);
        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.Size = new Size(30, 25);
        }
    }
}