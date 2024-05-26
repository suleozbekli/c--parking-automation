using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace otopark_otomasyonu
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            comboplaka.SelectedIndexChanged += comboplaka_SelectedIndexChanged;
            button1.Click += new EventHandler(button1_Click); 
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");

        private void Form6_Load(object sender, EventArgs e)
        {
            plates();
        }

        private void plates()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçkaydı", baglanti);
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
            SqlCommand komut = new SqlCommand("select * from araçkaydı where plaka='" + comboplaka.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtPhone.Text = read["telefon"].ToString();
                txtad.Text = read["ad"].ToString();
                txtsur.Text = read["soyad"].ToString();
                txtseri.Text = read["seri"].ToString();
            }
            baglanti.Close();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (comboplaka.SelectedItem == null)
            {
                MessageBox.Show("Please select a plaka before updating.");
                return;
            }

            string updatedPhoneNumber = txtPhone.Text;
            if (!IsNumeric(updatedPhoneNumber) || updatedPhoneNumber.Length != 10)
            {
                MessageBox.Show("Phone number should contain exactly 10 numeric characters.");
                return;
            }

            string updatedAd = txtad.Text;
            string updatedSoyad = txtsur.Text;
            string updatedSeri = txtseri.Text;

            string updateQuery = "UPDATE araçkaydı SET telefon=@telefon, ad=@ad, soyad=@soyad, seri=@seri WHERE plaka=@plaka";

            using (SqlCommand cmd = new SqlCommand(updateQuery, baglanti))
            {
                cmd.Parameters.AddWithValue("@telefon", updatedPhoneNumber);
                cmd.Parameters.AddWithValue("@ad", updatedAd);
                cmd.Parameters.AddWithValue("@soyad", updatedSoyad);
                cmd.Parameters.AddWithValue("@seri", updatedSeri);
                cmd.Parameters.AddWithValue("@plaka", comboplaka.SelectedItem.ToString());

                try
                {
                    baglanti.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update successful!");
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating data: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                    // Close the form after the update
                    this.Close();
                }
            }
        }


        private bool IsNumeric(string value)
        {
            return value.All(char.IsDigit);
        }

    }
}