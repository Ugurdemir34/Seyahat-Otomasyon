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
    public partial class Kaptan_Islem : Form
    {
        public Kaptan_Islem()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        private void otoboyut(ListView lvw, int width)
        {
            foreach (ColumnHeader col in lvw.Columns)
                col.Width = width;
        }
        void kaptangetir()
        {
            
            listView4.View = View.Details;
            listView4.Items.Clear();
            listView4.Columns.Clear();
            OleDbDataAdapter ada = new OleDbDataAdapter("select * from KAPTANLAR", bag);
            DataTable dt = new DataTable();
            dt.Clear();
            ada.Fill(dt);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn kolonlar = dt.Columns[i];
                listView4.Columns.Add(Convert.ToString(kolonlar));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr[0].ToString());
                listitem.SubItems.Add(dr[1].ToString());
                listitem.SubItems.Add(dr[2].ToString());
                listitem.SubItems.Add(dr[3].ToString());

                listView4.Items.Add(listitem);
            }
            otoboyut(listView4, listView4.Width / 4);
            ada.Dispose();
        }       
        void kaptansil()
        {
            try
            {
                if(textBox5.Text.Trim()=="")
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
                        komut = new OleDbCommand("DELETE FROM KAPTANLAR WHERE Sicil_No ='" + textBox5.Text + "'", bag);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Silindi");
                        textBox5.Clear();
                        kaptangetir();
                        bag.Close();
                        bag.Dispose();
                    }
                }              
            }
            catch(Exception Hata)
            {
               MessageBox.Show("Bir Hata Meydana Geldi ! Tekrar Deneyiniz\n Hata : "+Hata.Message);
            }
        }
        void kaydet()
        {
            bool kayıtdurumu = false;
            try
            {
                if(textBox1.Text.Trim()==""||textBox2.Text.Trim()==""||textBox3.Text.Trim()==""||textBox4.Text.Trim()=="")
                {
                    kayıtdurumu=false;
                }
                else
                {
                    kayıtdurumu = true;
                }
                if(kayıtdurumu)
                {
                    bag.Open();
                    komut.Connection = bag;
                    komut.CommandText = "INSERT INTO KAPTANLAR(Sicil_No,Kaptan_Adi,Kaptan_Soyadi,Kaptan_Gsm) VALUES(@Sicil_No,@Kaptan_Adi,@Kaptan_Soyadi,@Kaptan_Gsm)";
                    komut.Parameters.AddWithValue("@Sicil_No", textBox1.Text);
                    komut.Parameters.AddWithValue("@Kaptan_Adi", textBox2.Text);
                    komut.Parameters.AddWithValue("@Kaptan_Soyadi", textBox3.Text);
                    komut.Parameters.AddWithValue("@Kaptan_Gsm", textBox4.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarılı !");
                    kaptangetir();
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
                MessageBox.Show("Bir Hata Meydana Geldi ! Tekrar Deneyiniz\n Hata : "+ hata.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void Kaptan_Islem_Load(object sender, EventArgs e)
        {
            kaptangetir();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            kaydet();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            kaptangetir();
        }
        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView4.SelectedIndices.Count > 0)
            {
                textBox5.Text = listView4.SelectedItems[0].Text;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            kaptansil();
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