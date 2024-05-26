using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otopark_otomasyonu
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        System.Data.SqlClient.SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();
        private void Form5_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from araçkaydı ORDER BY CAST(SUBSTRING(parkyeri, 3, LEN(parkyeri) - 2) AS INT)", baglanti);
            adtr.Fill(daset, "form5");
            dataGridView1.DataSource = daset.Tables["form5"];
            baglanti.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
       

    

}
}
