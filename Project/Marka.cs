using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace otopark_otomasyonu
{
    public partial class Marka : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");

        public Marka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // Check if the table exists
                SqlCommand checkTableCommand = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_NAME = 'markabilgileri') " +
                    "CREATE TABLE markabilgileri (id INT PRIMARY KEY IDENTITY(1,1), " +
                    "marka NVARCHAR(50) NOT NULL UNIQUE)", baglanti);
                checkTableCommand.ExecuteNonQuery();

                // Insert brand
                SqlCommand insertCommand = new SqlCommand("INSERT INTO markabilgileri (marka) VALUES (@marka)", baglanti);
                insertCommand.Parameters.AddWithValue("@marka", textBox1.Text);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("BRAND HAS BEEN ADDED.");
                textBox1.Clear();
            }
            catch (SqlException )
            {
                MessageBox.Show("Brand with the same name already exists!" );
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Marka_Load(object sender, EventArgs e)
        {
            // Additional initialization if needed
        }
    }
}