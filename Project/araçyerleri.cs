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
    public partial class araçyerleri : Form
    {
        public araçyerleri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");



        private void araçyerleri_Load(object sender,EventArgs e)
        {   empty();
            Occupied();
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçkaydı", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())

            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString())
                        {
                            item.Text = read["plaka"].ToString();
                            item.BackColor = Color.IndianRed;
                        }
                    }
                }
            }
            baglanti.Close();
        }

        private void Occupied()
        {
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())

            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString() && read["durumu"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;
                            Console.WriteLine("P-3 Durumu: " + read["parkyeri"].ToString() + ", Durumu: " + read["durumu"].ToString());
                        }
                    }
                }
            }
            baglanti.Close();
        }

        private void empty()
        {
            int sayac = 1;
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    item.Text = "P-" + sayac;
                    item.Name = "P-" + sayac;
                    sayac++;
                }
            }
        }
    }
}

     

