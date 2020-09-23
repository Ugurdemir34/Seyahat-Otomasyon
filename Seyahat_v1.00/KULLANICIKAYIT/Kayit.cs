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
    public partial class Kayit : Form
    {
        public Kayit()
        {
            InitializeComponent();
        }
        void baglantıkontrol()
        {
            if (bag.State == ConnectionState.Open)
            {
                bag.Close();
            }           
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
            textBox3.PasswordChar = '\0';
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        bool durum = false;
        private void button4_Click(object sender, EventArgs e)
        {
           if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" ||textBox3.Text.Trim()=="" || textBox4.Text.Trim() =="")
            {
                MessageBox.Show("Tüm Alanları Doldurun");
            }
           else if (textBox2.Text !=textBox3.Text)
            {
                MessageBox.Show("Şifreler Uyuşmuyor");
            }
           else
            {
                try
                {
                    bag.Open();
                    komut = new OleDbCommand("SELECT * FROM KULLANICI Where K_ad='"+textBox1.Text+"'",bag);
                    OleDbDataReader oku = komut.ExecuteReader();                   
                    if (oku.Read())
                    {
                        durum = false;
                    }
                    else
                    {
                        durum = true;
                    }
                    bag.Close();
                    if(durum)
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "INSERT INTO KULLANICI(K_Ad,Sifre,Mail) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "')";
                        komut.ExecuteNonQuery();
                        // komut.Dispose();
                        bag.Close();
                        MessageBox.Show("Kayıt Başarılı");
                    }
                    else
                    {
                        MessageBox.Show("Kayıt Zaten Mevcut");
                    }
                    baglantıkontrol();
                 
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Bir Hata Meydana Geldi : "+ hata.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kullanici kl = new Kullanici();
            kl.Show();
            this.Dispose();
        }
    }
}
