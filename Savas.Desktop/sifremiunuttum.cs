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
    public partial class sifremiunuttum : Form
    {
        public sifremiunuttum()
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e) //sifreyi e-maile adresine gönderme 
        {
            string connectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:SAVAS \\sistem1.mdb\"");
            OleDbConnection connection = new OleDbConnection(connectionString);
            try
            {
                connection.Open();
                string mail = mailSifeUnuttumText.Text;
                string query = "SELECT * FROM kayıt WHERE email = '" + mail + "'";
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string sifre = reader["sifre"].ToString();
                    MailMessage message = new MailMessage();
                    SmtpClient client = new SmtpClient();// e-postanın gönderileceği sunucuya bağlanmak için gerkli bilgilerin içerdiği kod.
                    client.Credentials = new NetworkCredential("", "");//client.Credentials satırı, SMTP sunucusuna erişmek için kullanılacak kimlik bilgilerini
                    client.Port = 587;//port numarası
                    client.Host = "smtp.office365.com";// Office 365 SMTP sunucusu
                    client.EnableSsl = true;//iletişimlerin güvenliğini sağlamak için kullanılan bir şifreleme protokolüdür.
                    message.To.Add(mail);//e-postanın gönderileceği alıcının e-posta adresini eklemeyi sağlar
                    message.From = new MailAddress("");
                    message.Subject = "Şifremi Unuttum";
                    message.Body = "Şifre: " + "";//unuttuğu şifre, veritabanından okunan değer kullanılarak oluşturuluyor.
                    client.Send(message); //e - posta göndermek için SmtpClient nesnesinin Send() metodu çağrılıyor
                    MessageBox.Show("Mail adresinize şifre gönderildi.");
                    this.Hide();
                    login form2 = new login();
                    form2.ShowDialog();
                    this.Close();
                }
                else if (string.IsNullOrEmpty(mailSifeUnuttumText.Text))
                {
                    MessageBox.Show("Mail adresini giriniz");
                }
                else
                {
                    MessageBox.Show("Mail adresi bulunamadı");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
  