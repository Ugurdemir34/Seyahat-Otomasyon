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
    public partial class Kullanici : Form
    {
        public Kullanici()
        {
            InitializeComponent();
          
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otobus.accdb");
        OleDbCommand komut = new OleDbCommand();      
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bag.Open();                       
                komut.Connection = bag;
                komut.CommandText = "SELECT * FROM KULLANICI where K_ad='" + textBox1.Text + "' AND Sifre='" + textBox2.Text + "'";
                OleDbDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {                   
                    Admin_Panel adm = (Admin_Panel)Application.OpenForms["Admin_Panel"];
                    adm.WindowState = FormWindowState.Minimized;
                    BiletAl a = new BiletAl();
                    a.Show();
                    this.Close();             
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
                }
                bag.Close();
            }
            catch(Exception hata)
            {
                MessageBox.Show("Bir Hata Meydana Geldi : \n" + hata.Message);
            }
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Kayit kyt = new Kayit();
            kyt.Show();
            this.Hide();
        }
      
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sifre sf = new Sifre();
            sf.Show();
            this.Hide();
        }
    }
}
