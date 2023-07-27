using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Desktop
{
    public partial class bizeulasin : Form
    {

        private OleDbConnection baglanti = new OleDbConnection();

        public bizeulasin()
        {
            InitializeComponent();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                string konu = textBox1.Text;
                string metin = textBox3.Text;
                string kimden = textBox2.Text;

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""C:""";
                baglanti.ConnectionString = connectionString;

                OleDbCommand kayit = new OleDbCommand("INSERT INTO bizeulasin (ad, mail,mesaj) VALUES (@ad, @mail, @mesaj)", baglanti);

                kayit.Parameters.AddWithValue("@ad", textBox1);
                kayit.Parameters.AddWithValue("@mail", textBox2);
                kayit.Parameters.AddWithValue("@mesaj", textBox3);

                try
                {
                    // Veritabanı bağlantısını aç ve kaydı ekle
                    baglanti.Open();
                    int etkilenenSatirSayisi = kayit.ExecuteNonQuery();
                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla oluşturuldu.");
                    }
                    else
                    {
                        MessageBox.Show("Kayıt oluşturulamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    // Veritabanı bağlantısını kapat
                    baglanti.Close();
                }

                // E-posta gönderme işlemleri
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(" "," ");
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                message.To.Add(kimden);
                message.From = new MailAddress("");
                message.Subject = konu;
                message.Body = metin;
                client.Send(message);
                MessageBox.Show("İletiniz gönderildi.");

                this.Hide();
                login form3 = new login();
                form3.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            login gecis = new login();
            gecis.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
