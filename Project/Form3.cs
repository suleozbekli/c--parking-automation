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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");

        public void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("update giris set parola='"+textBox2.Text+"' WHERE kullanıcı_adi='" +textBox1.Text+"'", baglanti);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("UPDATED");
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
