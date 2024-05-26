using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace otopark_otomasyonu
{
    public partial class araçkaydı : Form
    {
        public araçkaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True");

        private void araçkaydı_Load(object sender, EventArgs e)
        {
            EmptyCars();
            Brand();

        }

        private void Brand()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combomarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void EmptyCars()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboparkyeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex regexmail = new Regex(@"^([a-zA-Z0-9]+)(@(gmail|hotmail|iCloud).com)$");
            Regex regexphone = new Regex(@"^([5][0-9]{9})");
            Regex regexssn = new Regex(@"^([1-9]{1})([0-9]{10})");
            Regex regexname = new Regex(@"([a-zA-Z]{2,})");
            Regex regexlastname = new Regex(@"([a-zA-Z]{2,})");


            bool isValidmail = regexmail.IsMatch(txtemail.Text);
            bool isValidphone = regexphone.IsMatch(txttelefon.Text);
            bool isValidssn = regexssn.IsMatch(txtTC.Text);
            bool isValidname = regexname.IsMatch(txtad.Text);
            bool isValidlastname = regexlastname.IsMatch(txtsoyad.Text);

            if (!isValidssn)
            {
                MessageBox.Show("PLEASE CHECK YOUR SSN");
                txtTC.Focus();
                return;
               
            }

            else if (!isValidname)
            {
                MessageBox.Show("PLEASE CHECK YOUR NAME");
                txtad.Focus();
                return;
               
            }
            else if (!isValidlastname)
            {
                MessageBox.Show("PLEASE CHECK YOUR SURNAME");
                txtsoyad.Focus();
                return;
               
            }
           
            else if (!isValidphone)
            {
                MessageBox.Show("PLEASE CHECK YOUR PHONE NUMBER");
                txttelefon.Focus();
                return;
                
            }
            else if (!isValidmail)
            {
                MessageBox.Show("PLEASE ENTER VALID MAİL");
                txtemail.Focus();
                return;
            }


            else
            {
                // Check if the TC already exists in the database
                bool duplicateTcExists;
                bool duplicateTelefonExists;
                bool duplicateEmailExists;
                bool duplicatePlakaExists;

                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MUP4ISK;Initial Catalog=otopark_otomasyonu;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand checkDuplicateTcCommand = new SqlCommand("IF EXISTS (SELECT 1 FROM araçkaydı WHERE Tc = @tc) SELECT 1 ELSE SELECT 0", connection))
                    {
                        checkDuplicateTcCommand.Parameters.AddWithValue("@tc", txtTC.Text.Trim());

                        duplicateTcExists = (int)checkDuplicateTcCommand.ExecuteScalar() == 1;
                    }
                    using (SqlCommand checkDuplicateTelefonCommand = new SqlCommand("IF EXISTS (SELECT 1 FROM araçkaydı WHERE Telefon = @telefon) SELECT 1 ELSE SELECT 0", connection))
                    {
                        checkDuplicateTelefonCommand.Parameters.AddWithValue("@telefon", txttelefon.Text.Trim());
                        duplicateTelefonExists = (int)checkDuplicateTelefonCommand.ExecuteScalar() == 1;
                    }

                    using (SqlCommand checkDuplicateEmailCommand = new SqlCommand("IF EXISTS (SELECT 1 FROM araçkaydı WHERE Email = @email) SELECT 1 ELSE SELECT 0", connection))
                    {
                        checkDuplicateEmailCommand.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                        duplicateEmailExists = (int)checkDuplicateEmailCommand.ExecuteScalar() == 1;
                    }

                    using (SqlCommand checkDuplicatePlakaCommand = new SqlCommand("IF EXISTS (SELECT 1 FROM araçkaydı WHERE Plaka = @plaka) SELECT 1 ELSE SELECT 0", connection))
                    {
                        checkDuplicatePlakaCommand.Parameters.AddWithValue("@plaka", txtplaka.Text.Trim());
                        duplicatePlakaExists = (int)checkDuplicatePlakaCommand.ExecuteScalar() == 1;
                    }
                    // Connection will be automatically closed when leaving the 'using' block
                }

                if (duplicateTcExists)
                {
                    // If TC already exists, show an error message
                    MessageBox.Show("A record with the same TC already exists. Please enter a unique TC.", "Duplicate TC");
                }
                else if (duplicateTelefonExists)
                {
                    // If TC already exists, show an error message
                    MessageBox.Show("A record with the same telefon already exists. Please enter a unique telefon.", "Duplicate telefon");
                }
                else if (duplicateEmailExists)
                {
                    // If TC already exists, show an error message
                    MessageBox.Show("A record with the same email already exists. Please enter a unique email.", "Duplicate email");
                }
                else if (duplicatePlakaExists)
                {
                    // If TC already exists, show an error message
                    MessageBox.Show("A record with the same plaka already exists. Please enter a unique plaka.", "Duplicate plaka");
                }

                else
                {
                    // Open the connection
                    baglanti.Open();

                    // Continue with the rest of your code for insertion
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO araçkaydı(Tc,Ad,Soyad,Telefon,Email,Plaka,Marka,Seri,Renk,Parkyeri,Tarih) VALUES('" +
                        txtTC.Text.ToString() + "','" + txtad.Text.ToString() + "','" + txtsoyad.Text.ToString() + "','" + txttelefon.Text.ToString() + "','" + txtemail.Text.ToString() + "','" +
                        txtplaka.Text.ToString() + "','" + combomarka.Text.ToString() + "','" + txtseri.Text.ToString() + "','" + txtrenk.Text.ToString() + "','" + comboparkyeri.Text.ToString() + "','" +
                        DateTime.Now.ToString() + "')", baglanti);

                    insertCommand.ExecuteNonQuery();



                  
                    // Set the primary key constraint on 'Tc' column
                    /*SqlCommand alterCommand = new SqlCommand("ALTER TABLE araçkaydı ADD CONSTRAINT PK_araçkaydı_tc PRIMARY KEY (tc);", baglanti);
                    alterCommand.ExecuteNonQuery();*/

                    SqlCommand alterCommand2 = new SqlCommand(@"
     ALTER TABLE araçkaydı 
    ADD CONSTRAINT UQ_telefon UNIQUE(telefon);

    ALTER TABLE araçkaydı 
    ADD CONSTRAINT UQ_email UNIQUE(email);

    ALTER TABLE araçkaydı 
    ADD CONSTRAINT UQ_plaka UNIQUE(plaka);
", baglanti);
                    alterCommand2.ExecuteNonQuery();

                    // Update the status to 'DOLU' for the specified parkyeri
                    SqlCommand updateCommand = new SqlCommand("UPDATE araçdurumu SET durumu='DOLU' WHERE parkyeri='" + comboparkyeri.SelectedItem + "'", baglanti);
                    updateCommand.ExecuteNonQuery();

                    // Close the connection
                    baglanti.Close();

                    // Display a success message
                    MessageBox.Show("CAR HAS BEEN REGISTERED", "Kayıt");
                }
            }


            comboparkyeri.Items.Clear();
            EmptyCars();
            combomarka.Items.Clear();
            Brand();
            


            foreach (Control item in groupkişi.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in grouparaç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in grouparaç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }

        }







        private void btnmarka_Click_1(object sender, EventArgs e)
        {
            Marka brand = new Marka();
            brand.FormClosed += Brand_FormClosed; // Marka formu kapatıldığında tetiklenecek olay
            brand.ShowDialog();
        }

        private void Brand_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Marka formu kapatıldığında combomarka'nın içeriğini güncelle
            combomarka.Items.Clear();
            Brand();
        }





        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}