using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Desktop
{
    public partial class admingiris : Form
    {
        public admingiris()
        {
            InitializeComponent();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            this.Close(); // Mevcut formu kapat
            login gecis = new login();
            gecis.Show(); // Yeni formu görüntüle
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "kullanıcıadı" && textBox2.Text == "sifre")
            {
                // Kullanıcı doğru bilgileri girdiyse
                // adminskor sayfasına geçiş yapılabilir
                Admin adminskorSayfasi = new Admin();
                adminskorSayfasi.Show();
                this.Hide();
                this.Close(); // Mevcut formu kapat

            }
        }
    }
}
