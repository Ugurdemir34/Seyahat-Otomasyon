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
    public partial class Sefer_Islem : Form
    {
        public Sefer_Islem()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = otobus.accdb");
        OleDbCommand komut = new OleDbCommand();
        void verigetir(ListView lv)
        {
            try
            {              
                lv.View = View.Details;
                lv.Items.Clear();
                lv.Columns.Clear();
                OleDbDataAdapter ada = new OleDbDataAdapter("select * from SEFERLER", bag);
                DataTable dt = new DataTable();
                dt.Clear();
                ada.Fill(dt);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataColumn kolonlar = dt.Columns[i];
                    lv.Columns.Add(Convert.ToString(kolonlar));
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr[0].ToString());
                    listitem.SubItems.Add(dr[1].ToString());
                    listitem.SubItems.Add(dr[2].ToString());
                    listitem.SubItems.Add(dr[3].ToString().Replace("00:00:00", ""));
                    listitem.SubItems.Add(dr[4].ToString());
                    listitem.SubItems.Add(dr[5].ToString());
                    listitem.SubItems.Add(dr[6].ToString());
                    lv.Items.Add(listitem);
                }
                ada.Dispose();                         
            }
            catch (Exception)
            {
                throw;
            }
        }
        void hatgetir()
        {
            bag.Open();
            komut = new OleDbCommand("SELECT Kalkis_Varis FROM HATLAR", bag);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0]);
            }
            komut = new OleDbCommand("SELECT Plaka FROM OTOBUSLER", bag);
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku[0]);
            }
            komut = new OleDbCommand("SELECT Sicil_No FROM KAPTANLAR", bag);
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0]);
            }
            komut = new OleDbCommand("SELECT Sicil_No FROM MUAVINLER", bag);
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox4.Items.Add(oku[0]);
            }
            bag.Close();
           
        }
        void verisil()
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("ID Seçiniz !");
            }
            else
            {
                DialogResult a = new DialogResult();
                a = MessageBox.Show("Silmek İstediğinize Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    try
                    {
                        bag.Open();
                        komut.Connection = bag;
                        komut.CommandText = "DELETE FROM SEFERLER WHERE Sefer_Id =" + Convert.ToInt16(textBox1.Text) + "";
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Sefer Silindi!");
                        textBox1.Clear();
                        verigetir(listView1);
                        otoboyut(listView1, listView1.Width / 8);
                        bag.Close();                      
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Bir Hata Meydana Geldi : " + hata.Message);                        
                    }
                }
            }
        }
        private void kaydet()
        {
            try
            {
                bool kayıtdurumu = true;
                int hat_id = 0;         
                bag.Open();
                int ot_id = 0;
                komut = new OleDbCommand("SELECT * FROM HATLAR WHERE Kalkis_Varis ='" + comboBox1.SelectedItem + "' ", bag);
                OleDbDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {                    
                    hat_id = Convert.ToInt16(oku["Hat_Id"]);
                }
                string tarih = dateTimePicker1.Value.ToShortDateString().Replace(".", "/");
                komut = new OleDbCommand("SELECT * FROM SEFERLER WHERE Hat_Id=" + hat_id + " AND Sefer_Tarihi =#" + tarih + "#", bag);
                oku = komut.ExecuteReader();
                if (oku.Read())
                {           
                    kayıtdurumu = false;
                }
                komut = new OleDbCommand("SELECT * FROM OTOBUSLER WHERE Plaka ='"+comboBox2.SelectedItem+"'",bag);
                oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    ot_id = Convert.ToInt16(oku["Otobus_ID"]);
                }          
                bag.Close();              
                if ((textBox2.Text.Trim() == "" || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1))
                {
                    MessageBox.Show("Tüm Bilgileri Giriniz!");
                }
                else
                {
                    if (kayıtdurumu)
                    {
                        bag.Open();
                        //MessageBox.Show(hat_id + "");
                        komut.Connection = bag;
                        komut.CommandText = "INSERT INTO SEFERLER(Sefer_Kodu,Hat_Id,Sefer_Tarihi,Otobus_ID,Kaptan,Muavin)VALUES(@Sefer_Kodu,@Sefer_Tarihi,@Hat_Id,@Otobus_ID,@Kaptan,@Muavin)";
                        komut.Parameters.AddWithValue("@Sefer_Kodu", textBox2.Text);
                        komut.Parameters.AddWithValue("@Hat_Id", hat_id);
                        komut.Parameters.AddWithValue("@Sefer_Tarihi", tarih);
                        komut.Parameters.AddWithValue("@Otobus_ID", ot_id);
                        komut.Parameters.AddWithValue("@Kaptan", comboBox3.SelectedItem);
                        komut.Parameters.AddWithValue("@Muavin", comboBox4.SelectedItem);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Başarılı!");
                        textBox2.Clear();
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        comboBox3.SelectedIndex = -1;
                        comboBox4.SelectedIndex = -1;
                        verigetir(listView1);
                        otoboyut(listView1, listView1.Width / 8);
                        bag.Close();                     
                    }
                    else
                    {
                        MessageBox.Show("Zaten o sefer var");
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
                // MessageBox.Show("Bilgiler Eksiksiz ve Doğru Doldurun !","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void otoboyut(ListView lvw, int width)
        {
           foreach (ColumnHeader col in lvw.Columns)
           col.Width = width;
        }
        private void Sefer_Islem_Load(object sender, EventArgs e)
        {
            hatgetir();
            verigetir(listView1);
            otoboyut(listView1,listView1.Width/7);
        }      
        private void button3_Click(object sender, EventArgs e)
        {
            kaydet();            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            verigetir(listView1);
            otoboyut(listView1, listView1.Width / 8);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            verisil();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                textBox1.Text = listView1.SelectedItems[0].Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.Size = new Size(30, 25);
        }
        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.Size = new Size(35, 30);
        }
    }       
}
