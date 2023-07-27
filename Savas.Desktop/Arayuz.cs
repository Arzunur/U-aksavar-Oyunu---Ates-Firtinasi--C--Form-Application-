using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Savas.Library.Concrete;


namespace Savas.Desktop
{
    public partial class Arayuz : Form
    {
        public Arayuz()
        {
            InitializeComponent();
        }
        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nasıloynanır gecis = new nasıloynanır();
            gecis.Show(); 
        }
        private void pictureBox3_Click(object sender, EventArgs e) //giriş buton 
        {
            login gecis = new login();
            gecis.Show();
        }

        private void yardımToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            nasıloynanır yeni = new nasıloynanır();
            yeni.Show();
        }

        private void bizeUlaşınToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bizeulasin gecis = new bizeulasin();
            gecis.Show();
        }

        private void hakkımızdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hakkimizda gecis = new hakkimizda();
            gecis.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            level1 gecis = new level1();
            gecis.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            l2 gecis = new l2();
            gecis.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            l3 gecis4 = new l3();
            gecis4.Show();
            this.Hide();
        }
    }
}
