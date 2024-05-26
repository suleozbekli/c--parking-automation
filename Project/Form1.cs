using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otopark_otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            araçkaydı register = new araçkaydı();
            register.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            araçyerleri location = new araçyerleri();
            location.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            araç_çıkışı exit = new araç_çıkışı();
            exit.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Satis sale = new Satis();
            sale.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.ShowDialog();
        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        

        }
    }


