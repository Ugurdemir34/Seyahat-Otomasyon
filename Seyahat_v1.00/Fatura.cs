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
using CrystalDecisions.CrystalReports.Engine;

namespace Seyahat_v1._00
{
    public partial class Fatura : Form
    {
        public Fatura()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otobus.accdb");
        DataTable tablo = new DataTable();       
        private void Fatura_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    tablo.Clear();
            //    OleDbDataAdapter adptr = new OleDbDataAdapter("SELECT * FROM SATIS ORDER BY Bilet_Id DESC LIMIT 1", bag);
            //    adptr.Fill(tablo);
            //    BiletKesimi rapor = new BiletKesimi();

            //    rapor.SetDataSource(tablo);
            //    crystalReportViewer1.ReportSource = rapor;
            //            
            //}
            //catch (Exception)
            //{
            //}
            BiletKesim2.Refresh();
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
