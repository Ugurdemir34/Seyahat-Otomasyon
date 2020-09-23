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
    public partial class Hat_Islem : Form
    {
        public Hat_Islem()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        void sehirgetir(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.AddRange(new string[] { "Istanbul", "Edirne", "Kırklareli", "Tekirdağ", "Çanakkale", "Kocaeli", "Yalova", "Sakarya", "Bilecik", "Bursa", "Balıkesir" });
        }
        bool durum = false;
        void kontrol()
        {
            bag.Open();
            komut = new OleDbCommand("SELECT * FROM HATLAR Where Kalkis_Varis ='"+(comboBox1.SelectedItem + "-" + comboBox2.SelectedItem)+"'",bag);
            OleDbDataReader oku = komut.ExecuteReader();
            if(oku.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            bag.Close();
           
        }
        void kaydet()
        {
            try
            {
                if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("Bilgileri Eksiksiz Doldurun !");
                }
                else
                {
                    kontrol();
                    if(durum)
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "INSERT INTO HATLAR(Kalkis_Varis,Ucret) VALUES(@Kalkis_Varis,@Ucret)";
                        komut.Parameters.AddWithValue("@Kalkis_Varis", (comboBox1.SelectedItem + "-" + comboBox2.SelectedItem));
                        komut.Parameters.AddWithValue("@Ucret", textBox1.Text);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Başarılı");
                        hatgetir();
                        otoboyut(listView1, listView1.Width / 4);
                        bag.Close();                    
                    }
                    else
                    {
                        MessageBox.Show("Zaten O Hat Mevcut !");
                    }                   
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir Hata Meydana Geldi ! Tekrar Deneyiniz\n Hata : " + hata.Message);
            }
        }
        void sil()
        {
            try
            {
                if(textBox2.Text.Trim()=="")
                {
                    MessageBox.Show("ID Seçiniz");
                }
                else
                {
                    DialogResult a = new DialogResult();
                    a = MessageBox.Show(" Silmek İstediğinize Emin Misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (a == DialogResult.Yes)
                    {
                        bag.Open();
                        komut = new OleDbCommand("DELETE FROM HATLAR Where Hat_Id=" + textBox2.Text + "", bag);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Hat Silindi");
                        hatgetir();
                        otoboyut(listView1, listView1.Width / 4);
                        bag.Close();                      
                    }
                }                             
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir Hata Meydana Geldi ! Tekrar Deneyiniz\n Hata : " + hata.Message);
            }
        }
        void hatgetir()
        {
            listView1.View = View.Details;
            listView1.Items.Clear();
            listView1.Columns.Clear();
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * from HATLAR", bag);
            DataTable dt = new DataTable();
            dt.Clear();
            ada.Fill(dt);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn kolonlar = dt.Columns[i];
                listView1.Columns.Add(Convert.ToString(kolonlar));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr[0].ToString());
                listitem.SubItems.Add(dr[1].ToString());
                listitem.SubItems.Add(dr[2].ToString());               
                listView1.Items.Add(listitem);
            }
            ada.Dispose();
        }
        private void otoboyut(ListView lvw, int width)
        {
            foreach (ColumnHeader col in lvw.Columns)
                col.Width = width;
        }
        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.Size = new Size(35, 30);
        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.Size = new Size(30, 25);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            kaydet();
        }
        private void Hat_Islem_Load(object sender, EventArgs e)
        {
            hatgetir();
            otoboyut(listView1,listView1.Width/4);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            hatgetir();
            otoboyut(listView1, listView1.Width / 4);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sehirgetir(comboBox2);
            comboBox2.Items.Remove(comboBox1.SelectedItem);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                textBox2.Text = listView1.SelectedItems[0].Text;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}