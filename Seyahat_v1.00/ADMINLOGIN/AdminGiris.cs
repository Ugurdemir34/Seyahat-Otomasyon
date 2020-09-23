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
    public partial class AdminGiris : Form
    {
        public AdminGiris()
        {
            InitializeComponent();

        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }
       
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        Admin_Panel adm = new Admin_Panel();
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand("SELECT * FROM ADMINLER WHERE ADMIN_AD ='"+textBox1.Text +"' AND ADMIN_SİFRE ='"+textBox2.Text+"'",bag);
                OleDbDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    Admin_Panel adm = (Admin_Panel)Application.OpenForms["Admin_Panel"];
                    adm.menuStrip1.Visible = true;
                    adm.button1.Visible = false;
                    adm.button3.Visible = false;
                    adm.label4.Visible = true;
                    adm.button4.Visible = true;
                    adm.label4.Text = "Hoşgeldiniz : " + oku["ADMIN_AD"];
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Parola Hatalı!");
                }
                bag.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
            }
        }       
    }
}
