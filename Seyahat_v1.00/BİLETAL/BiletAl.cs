using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;
using System.Diagnostics;
namespace Seyahat_v1._00
{
    public partial class BiletAl : Form
    {
        public BiletAl()
        {
            InitializeComponent();
        }        
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand yeni = new OleDbCommand();
        OleDbCommand komut = new OleDbCommand();
        ArrayList koltuklar = new ArrayList();
        ArrayList cinsiyetler = new ArrayList();      
        string sahiscins = "";
        int sayac = 0;
        public string secilen = "";
        public int ucret = 0;
        int x, y;
       // Fatura ftr = new Fatura();
        bool indirim = false;
        bool kaydetdurum = false;
        void baglantıkontrol()
        {
            if (bag.State == ConnectionState.Open)
            {
                bag.Close();
            }
            if (bag.State == ConnectionState.Closed)
            {
                bag.Open();
            }
        }      
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult a = new DialogResult();
            a = MessageBox.Show(" Çıkmak İstediğinize Emin Misiniz ?","Uyarı !",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(a == DialogResult.Yes)
            {
               
                Admin_Panel adm = (Admin_Panel)Application.OpenForms["Admin_Panel"];
                adm.WindowState = FormWindowState.Normal;
                this.Dispose();
            }
        }      
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Pen p = new Pen(Color.FromArgb(36,134,235), 3);
            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, 0, 5, -10, 5);
            gfx.DrawLine(p, 75, 5, e.ClipRectangle.Width - 2, 5);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Pen p = new Pen(Color.FromArgb(36, 134, 235), 3);
            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, 0, 5, -10, 5);
            gfx.DrawLine(p, 75, 5, e.ClipRectangle.Width - 2, 5);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);
        }
        void otobusgetir()
        {          
            lblmuavin.Text = "";
            lblplaka.Text = "";
            lblsofor.Text = "";
            lbltel.Text = "";
            lblwifi.Text = "";
            baglantıkontrol();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM SEFERLER Where Sefer_Tarihi =#"+comboBox1.SelectedItem+"# AND Hat_Id = (SELECT Hat_Id FROM HATLAR Where Kalkis_Varis = '"+(kalkiscb.SelectedItem+"-"+variscb.SelectedItem)+"')",bag);            
            OleDbDataReader oku = komut.ExecuteReader();
            string kaptan_id = "";
            string muavin_id = "";
            string otobus_id = "";
            if(oku.Read())
            {
                otobus_id = oku["Otobus_ID"].ToString();              
                kaptan_id=oku["Kaptan"].ToString();
                muavin_id = oku["Muavin"].ToString();
            }          
            OleDbCommand komut2 = new OleDbCommand("SELECT * FROM KAPTANLAR WHERE Sicil_No='" + kaptan_id + "'", bag);
            OleDbDataReader oku2 = komut2.ExecuteReader();
            if (oku2.Read())
            {
                lblsofor.Text = (oku2["Kaptan_Adi"] + " " + oku2["Kaptan_Soyadi"]).ToString();
            }                      
            OleDbCommand komut3= new OleDbCommand("SELECT * FROM MUAVINLER WHERE Sicil_No='" + muavin_id + "'", bag);
            OleDbDataReader oku3 = komut3.ExecuteReader();
            if (oku3.Read())
            {
                lblmuavin.Text = (oku3["Muavin_Adi"] +" "+ oku3["Muavin_Soyadi"]).ToString();
            }         
            OleDbCommand komut4 = new OleDbCommand("SELECT * FROM OTOBUSLER WHERE Otobus_ID=" + otobus_id + "", bag);
            OleDbDataReader oku4 = komut4.ExecuteReader();
            if (oku4.Read())
            {
                lblplaka.Text = oku4["Plaka"].ToString();
                lblwifi.Text = oku4["WifiSifresi"].ToString();
                lbltel.Text = oku4["Arac_Telefon"].ToString();
            }
            baglantıkontrol();
        }
        void temizle()
        {
            foreach (Control item in panel2.Controls)
            {
                if(item is Button)
                {                   
                    if(item.Name.Length ==5)
                    {
                        item.Text = item.Name.Substring(3, 2);
                        item.BackgroundImage = null;
                    }
                    if(item.Name.Length ==4)
                    {
                        item.Text = item.Name.Substring(3, 1);
                        item.BackgroundImage = null;
                    }
                    item.BackColor = Color.White;
                }               
            }
        }     
        private void cbcins_SelectedIndexChanged(object sender, EventArgs e)
        {
            sefersorgula();
        }
        void sefersorgula()
        {
           temizle();
            try
            {
                baglantıkontrol();            
                OleDbCommand komut = new OleDbCommand("SELECT Koltuk_No,Cinsiyet FROM SATIS WHERE Sefer_Id=(SELECT Sefer_Id FROM SEFERLER Where Sefer_Tarihi=#"+comboBox1.SelectedItem+"# AND Hat_Id = (SELECT Hat_Id FROM HATLAR Where Kalkis_Varis = '"+(kalkiscb.SelectedItem+"-"+variscb.SelectedItem)+"'))", bag);             
                OleDbDataReader oku = komut.ExecuteReader();                       
                koltuklar.Clear();
                while (oku.Read())
                {                  
                    koltuklar.Add(oku["Koltuk_No"]);                  
                    sahiscins = oku["Cinsiyet"].ToString();
                    string boyanacak = "btn" + oku["Koltuk_No"];
                    foreach (Button item in panel2.Controls)
                    {                        
                        //item.BackColor = Color.White;
                        if (koltuklar.Contains(Convert.ToInt32(item.Text)))
                        {                           
                            if (item.Name == boyanacak && sahiscins=="Bayan")
                            {                              
                                item.BackColor = Color.Pink;//Color.FromArgb(100, 65, 165);
                                item.BackgroundImage = ımageList1.Images[0];                                
                            }
                            else if(item.Name == boyanacak && sahiscins == "Bay")
                            {                             
                                item.BackColor = Color.FromArgb(0, 126, 229);
                                item.BackgroundImage = ımageList1.Images[1];                              
                            }                           
                        }
                        else
                        {                           
                        }
                    }
                }
                komut.Dispose();
                baglantıkontrol();
            }
            catch 
            {              
            }
        }        
        public int ucretgetir()
        {
            baglantıkontrol();
            ucret = 0;
            OleDbCommand komut2 = new OleDbCommand("SELECT * FROM HATLAR WHERE Kalkis_Varis = '" + (kalkiscb.SelectedItem + "-" + variscb.SelectedItem) + "'", bag);
            OleDbDataReader oku2 = komut2.ExecuteReader();
            if (oku2.Read())
            {
               ucret= Convert.ToInt32(oku2["Ucret"]);
            }
            baglantıkontrol();
            komut.Dispose();
            return ucret;                  
        }
        void sehirgetir(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.AddRange(new string[] { "Istanbul", "Edirne", "Kırklareli", "Tekirdağ", "Çanakkale", "Kocaeli", "Yalova", "Sakarya", "Bilecik", "Bursa", "Balıkesir" });
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            temizle();
            secilen = "";            
            sefersorgula();
            if (kalkiscb.SelectedIndex ==-1 && variscb.SelectedIndex ==-1 && comboBox1.SelectedIndex ==-1)
            {
                MessageBox.Show("Sefer Bilgisi Seçiniz");
            }
            else
            {              
                if (sayac == 0)
                {
                    if(cbcins.SelectedIndex ==-1)
                    {
                        MessageBox.Show("Kişisel Bilgileri Doldur!");
                    }
                    else
                    {
                        if(((Button)sender).BackColor != Color.White)
                        {
                              MessageBox.Show(((Button)sender).Text+" Numaralı Koltuk Alınmış");                          
                        }
                        else
                        {
                            secilen = ((Button)sender).Text;
                            ((Button)sender).Text = "";
                            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                            ((Button)sender).BackgroundImage = ımageList1.Images[cbcins.SelectedIndex];
                            if (cbcins.SelectedIndex == 0)
                            {
                                ((Button)sender).BackColor = Color.FromArgb(100, 65, 165);
                            }
                            else
                            {
                                ((Button)sender).BackColor = Color.FromArgb(0, 126, 229);
                            }
                        }                       
                    }                                    
                }
                else
                {
                }
            }            
        }
        private void kalkiscb_SelectedIndexChanged(object sender, EventArgs e)
        {
            label15.Visible = false;
            sehirgetir(variscb);
            variscb.Enabled = true;
            variscb.Items.Remove(kalkiscb.SelectedItem);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
           // notifyIcon1.ShowBalloonTip(3000, notifyIcon1.BalloonTipTitle, notifyIcon1.BalloonTipText, ToolTipIcon.Info);
        }      
        private void button3_MouseEnter(object sender, EventArgs e)
        {
           button3.Height += 3;
           button3.Width += 3;
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Height -= 3;
            button3.Width -= 3;
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.Location = new Point(button4.Location.X+2,button4.Location.Y);
        }
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Location = new Point(button4.Location.X - 2, button4.Location.Y);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (adtxt.Text.Trim() == "" || soyadtxt.Text.Trim() == "" || tctxt.Text.Trim() == "" || cbekbilgi.SelectedIndex == -1 || cbodeme.SelectedIndex == -1 || ceptxt.Text.Trim() == "")
                {
                    MessageBox.Show("Bilgileri Eksiksiz Doldurun");
                    kaydetdurum = false;
                }
                else
                {
                    kaydetdurum = true;
                }
                if (kaydetdurum)
                {
                    baglantıkontrol();
                    OleDbCommand komut2 = new OleDbCommand("SELECT * FROM SEFERLER Where Sefer_Tarihi =#" + comboBox1.SelectedItem + "# AND Hat_Id = (SELECT Hat_Id FROM HATLAR Where Kalkis_Varis='" + (kalkiscb.SelectedItem + "-" + variscb.SelectedItem) + "')", bag);
                    string id = "";
                    string sefer_no = "";
                    OleDbDataReader oku = komut2.ExecuteReader();
                    if (oku.Read())
                    {
                        id = oku["Sefer_Id"].ToString();
                        sefer_no = oku["Sefer_Kodu"].ToString();
                    }
                    string ekle = "INSERT INTO SATIS(Sefer_Id,Sefer_Kodu,Tarih,Yolcu_Adi_Soyadi,Koltuk_No,TC_No,Cep_Tel,Ek_Bilgi,Cinsiyet,Ucret) VALUES  (@Sefer_Id,@Sefer_Kodu,@Tarih,@Yolcu_Adi_Soyadi,@Koltuk_No,@TC_No,@Cep_Tel,@Ek_Bilgi,@Cinsiyet,@Ucret)";
                    OleDbCommand komut = new OleDbCommand(ekle, bag);
                    komut.Parameters.AddWithValue("@Sefer_Id,", Convert.ToInt32(id));
                    komut.Parameters.AddWithValue("@Sefer_Kodu,", sefer_no);
                    komut.Parameters.AddWithValue("@Tarih", comboBox1.SelectedItem);
                    komut.Parameters.AddWithValue("@Yolcu_Adi_Soyadi", (adtxt.Text + " " + soyadtxt.Text));
                    komut.Parameters.AddWithValue("@Koltuk_No", Convert.ToInt32(secilen));
                    komut.Parameters.AddWithValue("@TC_No", tctxt.Text);
                    komut.Parameters.AddWithValue("@Cep_Tel", ceptxt.Text);
                    komut.Parameters.AddWithValue("@Ek_Bilgi", cbekbilgi.SelectedItem);
                    komut.Parameters.AddWithValue("@Cinsiyet", cbcins.SelectedItem);
                    komut.Parameters.AddWithValue("@Ucret", ucret);
                    komut.ExecuteNonQuery();
                    groupBox3.Visible = false;
                    komut.Dispose();
                    komut2.Dispose();
                    baglantıkontrol();
                    DialogResult a = MessageBox.Show(" Kayıt Başarılı !  Fatura Çıktısı Görmek İster Misiniz ?", "Fatura Görüntüleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    groupBox1.Enabled = false;
                    if (a == DialogResult.Yes)
                    {
                        Fatura ftr = new Fatura();
                        ftr.Show();
                    }
                }          
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sefer Bilgisi ve Kişisel Bilgilerinizi Eksiksiz Giriniz ! :\n" + hata.Message);
                temizle();
                groupBox3.Visible = false;
            }
            finally
            {
                cbcins.SelectedIndex = -1;
               // kalkiscb.SelectedIndex = -1;
               // variscb.SelectedIndex = -1;
               // comboBox1.SelectedIndex = -1;               
            }
            tctxt.Clear();
            adtxt.Clear();
            soyadtxt.Clear();
            ceptxt.Clear();
            cbcins.SelectedIndex = -1;
            cbekbilgi.SelectedIndex = 0;
            cbodeme.SelectedIndex = -1;
        }           
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {          
            sefersorgula();
            groupBox1.Enabled = true;        
            otobusgetir();
            panel2.Visible = true;           
            groupBox3.Visible = true;                          
        }
        private void variscb_SelectedIndexChanged(object sender, EventArgs e)
        {
            label15.Visible = true;
            try
            {            
                comboBox1.Items.Clear();
                baglantıkontrol();
                OleDbCommand komut = new OleDbCommand("SELECT Sefer_Tarihi FROM SEFERLER WHERE Hat_Id = (SELECT Hat_Id FROM HATLAR WHERE Kalkis_Varis = '" + (kalkiscb.SelectedItem + "-" + variscb.SelectedItem) + "')", bag);
                OleDbDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    DateTime tarih = Convert.ToDateTime(oku["Sefer_Tarihi"]);
                    comboBox1.Items.Add(tarih.ToShortDateString().Replace(".", "/"));
                }
                baglantıkontrol();
                label15.Text = "Ücret : " + ucretgetir();
            }
            catch
            {
            }
            komut.Dispose();
        }      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
          Process.Start("www.yazilimisi.com");
        }          
        private void btn45_MouseEnter(object sender, EventArgs e)
        {
            if(((Button)sender).BackColor != Color.White)
            {
                ((Button)sender).Cursor = Cursors.No;
                ((Button)sender).Enabled = false;
            }  
            else
            {
                ((Button)sender).Cursor = Cursors.Hand;
            }
        }
        private void btn45_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = true;           
        }    
        private void cbekbilgi_SelectedIndexChanged(object sender, EventArgs e)
        {
           ucret = ucretgetir();          
           if(cbekbilgi.SelectedIndex == 1 || cbekbilgi.SelectedIndex == 2 || cbekbilgi.SelectedIndex == 3 || cbekbilgi.SelectedIndex == 4 )
            {
                indirim = true;
            }
           else
            {
                indirim = false;
            }
            if(indirim)
            {
                ucret -= 20;              
            }
            else
            {
                ucret = ucretgetir();
            }
            label15.Text = "Ücret : " + ucret;
        }
        private void cbodeme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbodeme.SelectedIndex == 1)
            {
                cbodeme.SelectedIndex = 0;
            }
        }
        private void BiletAl_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            y = this.Location.Y;
            x = this.Location.X;
            cbekbilgi.SelectedIndex = -0;      
            //notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            //notifyIcon1.BalloonTipTitle = "Lüks Marmara Seyahat";
            //notifyIcon1.BalloonTipText = "Program Gizlendi";                                         
        }
    }
}
