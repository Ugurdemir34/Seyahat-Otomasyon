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
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace Seyahat_v1._00
{
    public partial class Sifre : Form
    {
        public Sifre()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim()=="")
            {
                MessageBox.Show("Mail Adresinizi Doğru Bir Biçimde Giriniz !");
            }
            else
            {
                bag.Open();
                komut = new OleDbCommand("SELECT  * FROM KULLANICI Where Mail='" + textBox1.Text.Trim() + "'", bag);
                OleDbDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    try
                    {                        
                        MailMessage ePosta = new MailMessage();
                        ePosta.From = new MailAddress("ugurdemir551@gmail.com");
                        ePosta.To.Add(textBox1.Text);
                        ePosta.Subject = "Şifre Hatırlatma";
                        ePosta.IsBodyHtml = true;
                        ePosta.Body = "Sayın " + oku["K_ad"] +"," + "\nŞifreniz: " + oku["Sifre"] + "\n" +"<a href = 'http://yazilimisi.com'>Sitemiz</a>";          
                        SmtpClient smtp = new SmtpClient();
                        smtp.Credentials = new System.Net.NetworkCredential("ugurdemir551@gmail.com", "ugurdemir2434./*+");
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        object userState = ePosta;
                        smtp.SendAsync(ePosta, (object)ePosta);
                        MessageBox.Show("Mail Başarıyla Gönderildi.", "Mail Gönderme");
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Hata : " + hata.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı Bulunamadı !","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                bag.Close();
            }           
        }
    }
}
