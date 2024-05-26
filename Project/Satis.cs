using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace otopark_otomasyonu
{
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();

        private void satış_Load(object sender, EventArgs e)
        {
            List();
            CalcDailyTotal();
            Calc();
            
            

        }

        private void Calc()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satis", baglanti);
            label1.Text = "TOTAL PRICE = " + komut.ExecuteScalar() + "TL";
          
          
            baglanti.Close();
        }

        private void List()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis ORDER BY CONVERT(DATE, çıkış, 103) DESC", baglanti);
            adtr.Fill(daset, "satis");
            dataGridView1.DataSource = daset.Tables["satis"];
            baglanti.Close();
        }

        private void CalcDailyTotal()
        {
            baglanti.Open();

            // Calculate daily total
            string sqlSorgusu = "SELECT SUM(tutar) FROM satis WHERE CONVERT(DATE, TRY_CONVERT(datetime, çıkış, 104)) = CONVERT(DATE, GETDATE())";
            SqlCommand komut = new SqlCommand(sqlSorgusu, baglanti);

            object sonuc = komut.ExecuteScalar();

            if (sonuc != DBNull.Value)
            {
                label1.Text = "TOTAL PRICE = " + sonuc.ToString() + "TL";
                label3.Text = "DAILY PRICE = " + sonuc.ToString() + "TL";
            }
            else
            {
                label1.Text = "TOTAL PRICE = 0TL";
                label3.Text = "DAILY PRICE = 0TL";
            }

            baglanti.Close();
        }








        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
