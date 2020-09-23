using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Seyahat_v1._00
{
    public partial class Admin_Panel : Form
    {
        public Admin_Panel()
        {
            InitializeComponent();          
        }
        public void label_location(Label lbl,Button btn,Timer tm)
        {
            lbl.Text = DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString();
            if (lbl.Left >= btn.Left)
            {
                x = panel1.Left - 250;
                lbl.Location = new Point(x, lbl.Location.Y);               
            }
            else
            {               
                lbl.Location = new Point(x, label1.Location.Y);
                x += 7;
            }
        }
        int x = 0;
        private void süre_Tick(object sender, EventArgs e)
        {
            label_location(label1,button6,süre);
        }
        private void Admin_Panel_Load(object sender, EventArgs e)
        {
            süre.Start();
        }
        private void seferİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sefer_Islem sfr_is = new Sefer_Islem();
            sfr_is.Name = "sefer";
            if (Application.OpenForms["sefer"] == null)
            {
                sfr_is.Show();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult a = new DialogResult();
            a = MessageBox.Show("Çıkmak İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
            //this.WindowState = FormWindowState.Minimized;
        }
        private void kaptanİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kaptan_Islem kptn = new Kaptan_Islem();                   
           kptn.Name = "kaptan";
            if (Application.OpenForms["kaptan"] == null)
            {
                kptn.Show();
            }
        }
        private void biletİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bilet_Islem blt = new Bilet_Islem();
            blt.Name = "bilet";
            if (Application.OpenForms["bilet"] == null)
            {
                blt.Show();
            }
        }
        private void hatİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hat_Islem ht = new Hat_Islem();
            ht.Name = "hat";
            if (Application.OpenForms["hat"] == null)
            {
                ht.Show();
            }            
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            (sender as Button).Height += 10;
            (sender as Button).Width += 10;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).Height -= 10;
            (sender as Button).Width -= 10;
        }
        AdminGiris adm;
        Kullanici kl;
        private void button1_Click(object sender, EventArgs e)
        {      
                if (adm == null || adm.IsDisposed)
                {               
                    adm = new AdminGiris();
                    adm.Show();
                }
                else
                {
                    adm.BringToFront();
                }         
        }            
        private void button3_Click(object sender, EventArgs e)
        {             
                if (kl == null || kl.IsDisposed)
                {                   
                    kl = new Kullanici();
                    kl.Show();
                }
                else
                {
                    kl.BringToFront();
                }                                     
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {           
        }
        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult a = new DialogResult();
            a = MessageBox.Show(" Çıkmak İstediğinize Emin Misiniz ?", "Uyarı !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void muavinİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Muavin_Islem mua = new Muavin_Islem();
            mua.Name = "muavin";
            if (Application.OpenForms["muavin"] == null)
            {
                mua.Show();
            }
        }
        private void otobüsİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Otobus_Islemler otb = new Otobus_Islemler();
            otb.Name = "otobus";
            if (Application.OpenForms["otobus"] == null)
            {
                otb.Show();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button3.Visible = true;
            menuStrip1.Visible = false;
            label4.Visible = false;
            button4.Visible = false;
        }
        private void kullanıcıİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADMINPANEL.ISLEMLER.Kullanici_Islem kl = new ADMINPANEL.ISLEMLER.Kullanici_Islem();
            kl.Name = "kullanici";
            if (Application.OpenForms["kullanici"] == null)
            {
                kl.Show();
            }         
        }
    }
}