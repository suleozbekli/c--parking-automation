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
    public partial class Form2 : Form
    {
        
        
        SqlDataReader dr;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM giris where kullanıcı_adi = '"+textBox1.Text+"'AND parola = '"+textBox2.Text+"'", baglanti);


            SqlDataReader read = komut.ExecuteReader();

            if (read.Read())
            {
                
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Check Your Username and Password!");
            }
            baglanti.Close();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm1 = new Form3();
            frm1.Show();
            this.Hide();
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Balıkhisar,Özel Bulvarı \n No:236 \n 06860 \n Çankaya/ANKARA \n 0(312)2714253");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
