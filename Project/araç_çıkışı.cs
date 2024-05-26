
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
    public partial class araç_çıkışı : Form
    {
        public araç_çıkışı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");
        private void araç_çıkışı_Load(object sender, EventArgs e)
        {
            
            plates();
            timer1.Enabled = true;
        }

        private void plates()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select  *from araçkaydı ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboplaka.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        

       public void comboplaka_SelectedIndexChanged(object sender, EventArgs e)
        {


            baglanti.Open();
            SqlCommand komut = new SqlCommand("select  *from araçkaydı where plaka='" + comboplaka.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtparkyeri.Text = read["parkyeri"].ToString();
                textBox1.Text = read["parkyeri"].ToString();

                txtparkyeri2.Text = read["parkyeri"].ToString();
                txttc.Text = read["tc"].ToString();
                txtad.Text = read["ad"].ToString();
                txtsoyad.Text = read["soyad"].ToString();
                txtmarka.Text = read["marka"].ToString();
                txtseri.Text = read["seri"].ToString();
                txtplaka.Text = read["plaka"].ToString();
                lblgeliş.Text = read["tarih"].ToString();
            }
            baglanti.Close();
            DateTime arrival, exit;
            arrival = DateTime.Parse(lblgeliş.Text);
            exit = DateTime.Parse(lblçıkış.Text);
            TimeSpan fark;
            fark = exit - arrival;
            lblsüre.Text = fark.TotalHours.ToString();
            lbltoplamtutar.Text = (double.Parse(lblsüre.Text) * (0.75)).ToString("0.00");

        }
      

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblçıkış.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from araçkaydı where plaka='" + txtplaka.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu ='BOŞ'  where parkyeri='" + txtparkyeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();

             SqlCommand komut3 = new SqlCommand("insert into satis(parkyeri,plaka,geliş,çıkış,süre,tutar) values(@parkyeri,@plaka,@geliş,@çıkış,@süre,@tutar)", baglanti);
                 komut3.Parameters.AddWithValue("@parkyeri", txtparkyeri2.Text);
                 komut3.Parameters.AddWithValue("@plaka", txtplaka.Text);
                 komut3.Parameters.AddWithValue("@geliş", lblgeliş.Text);
                 komut3.Parameters.AddWithValue("@çıkış", lblçıkış.Text);
                 komut3.Parameters.AddWithValue("@süre", double.Parse(lblsüre.Text));
                 komut3.Parameters.AddWithValue("@tutar", double.Parse(lbltoplamtutar.Text));


                 komut3.ExecuteNonQuery();
           
            



            baglanti.Close();
            MessageBox.Show("CAR HAS EXIT");
            foreach (Control item in groupBox2.Controls)


            {
                if (item is TextBox)
                {
                    item.Text = "";
                    txtparkyeri.Text = "";
                    textBox1.Text = "";
                    comboplaka.Text = "";

                }

            }
            comboplaka.Items.Clear();
            
            plates();
        }

        
    }


}