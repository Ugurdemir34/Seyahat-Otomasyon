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
    public partial class Otobus_Islemler : Form
    {
        public Otobus_Islemler()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        void kaydet()
        {
            bool kayıtdurumu = true;
            try
            {
                bag.Open();
                komut = new OleDbCommand("SELECT * FROM OTOBUSLER WHERE Plaka='"+ textBox1.Text + "'",bag);
                OleDbDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    kayıtdurumu = false;
                }
                bag.Close();             
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox6.Text.Trim() == "" || textBox7.Text.Trim() == "")
                {
                    kayıtdurumu = false;
                    MessageBox.Show("Doldurun!");
                }
                else
                {
                    if (kayıtdurumu)
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "INSERT INTO OTOBUSLER(Plaka,Marka,Model,Tarih,Arac_Telefon,WifiSifresi) VALUES(@Plaka,@Marka,@Model,@Tarih,@Arac_Telefon,@WifiSifresi)";
                        komut.Parameters.AddWithValue("@Plaka", textBox1.Text);
                        komut.Parameters.AddWithValue("@Marka", textBox2.Text);
                        komut.Parameters.AddWithValue("@Model", textBox4.Text);
                        komut.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value.ToShortDateString());
                        komut.Parameters.AddWithValue("@Arac_Telefon", textBox6.Text);
                        komut.Parameters.AddWithValue("@WifiSifresi", textBox7.Text);
                        komut.ExecuteNonQuery();
                        bag.Close();                       
                        MessageBox.Show("Kayıt Başarılı !");
                        otobusgetir();
                        otoboyut(listView3, listView3.Width / 7);
                    }
                    else
                    {
                        MessageBox.Show("Bilgilerinizi Doğru ve Eksiksiz Giriniz !");
                    }
                }
               
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir Hata Meydana Geldi !\n Hata: " + hata.Message);
            }
        }
        private void otoboyut(ListView lvw, int width)
        {
            foreach (ColumnHeader col in lvw.Columns)
                col.Width = width;
        }
        void otobusgetir()
        {
            listView3.View = View.Details;
            listView3.Items.Clear();
            listView3.Columns.Clear();
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM  OTOBUSLER", bag);
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
                listitem.SubItems.Add(dr[4].ToString());
                listitem.SubItems.Add(dr[5].ToString());
                listitem.SubItems.Add(dr[6].ToString());
                listView3.Items.Add(listitem);
            }
            otoboyut(listView3, listView3.Width / 7);
            ada.Dispose();
        }
        void sil()
        {
            try
            {
                if (textBox3.Text.Trim() == "")
                {
                    MessageBox.Show("Plaka Giriniz");
                }
                else
                {
                    DialogResult a = new DialogResult();
                    a = MessageBox.Show(" Silmek İstediğinize Emin Misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (a == DialogResult.Yes)
                    {
                        bag.Open();
                        komut = new OleDbCommand("DELETE FROM OTOBUSLER WHERE Otobus_ID =" + textBox3.Text + "", bag);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Silindi");
                        textBox3.Clear();
                        otobusgetir();
                        otoboyut(listView3, listView3.Width / 7);
                        bag.Close();
                      
                    }
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Bir Hata Meydana Geldi ! Tekrar Deneyiniz\n Hata : " + Hata.Message);
            }
        }
        private void Otobus_Islemler_Load(object sender, EventArgs e)
        {
            otobusgetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kaydet();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            otobusgetir();
            otoboyut(listView3, listView3.Width / 7);
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count > 0)
            {
                textBox3.Text = listView3.SelectedItems[0].Text;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            sil();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
