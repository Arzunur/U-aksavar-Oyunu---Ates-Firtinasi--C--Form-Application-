using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.Mail;
using System.Net;
using Savas.Library.Concrete;

namespace Savas.Desktop
{
    public partial class login : Form
    {
        private OleDbCommand komut;
        public login()
        {
            InitializeComponent();
            //Tooltip kontrol 
            toolTip1.ToolTipIcon = ToolTipIcon.Warning;
            toolTip1.ToolTipTitle = "UYARI"; 
            toolTip1.AutomaticDelay = 200; 
            toolTip1.SetToolTip(this.kullaniciadiText, "@ işaretini,büyük harf, küçük harflere dikkat ediniz.");
            toolTip1.SetToolTip(this.sifreGirisText, "Şifre harf, sayı, ve özel karakter içermelidir.");
            toolTip1.SetToolTip(this.mailText, "@ işaretini,büyük harf, küçük harflere dikkat ediniz.");
            toolTip1.SetToolTip(this.sifreUyeText, "Şifre en az 8 karakter olmalıdır.");
            //Arkaplan seffaf
            linkLabel1.Parent = pictureBox1;
            linkLabel1.BackColor = Color.Transparent;
            hatırla.Parent = pictureBox1;
            hatırla.BackColor = Color.Transparent;
        }
      
        private void pictureBox6_Click(object sender, EventArgs e) 
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void temizle()
        {
            adText.Text = " ";
            soyadText.Text = " ";
            mailText.Text = " ";
            sifreUyeText.Text = " ";
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (sifreUyeText.Text.Length < 8)
            {
                MessageBox.Show("Şifreniz en az 8 karakter uzunluğunda olmalıdır.", "Şifre Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\SAVAS \\sistem1.mdb\"");
                string sorgu = "insert into kayıt (ad,soyad,email,sifre) values (@ad,@soyad,@email,@sifre)";
                OleDbCommand komut = new OleDbCommand(sorgu, baglantı);
                komut.Parameters.AddWithValue("@ad", adText.Text);
                komut.Parameters.AddWithValue("@soyad", soyadText.Text);
                komut.Parameters.AddWithValue("@email", mailText.Text);
                komut.Parameters.AddWithValue("@sifre", sifreUyeText.Text);
                baglantı.Open();
                komut.ExecuteNonQuery();
                liste();
                temizle();
                baglantı.Close();
                adText.Text = "";
                soyadText.Text = "";
                mailText.Text = "";
                sifreUyeText.Text = "";
            }
        }
        void liste()//access bağlantı
        {
            OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\SAVAS \\sistem1.mdb\"");// access bağlantı adresi
            baglantı.Open();
            OleDbCommand komut = new OleDbCommand();//sorguları göndermek 
            komut.Connection = baglantı;
            komut.CommandText = ("Select * From kayıt");
            DataTable tablo = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(tablo);
            baglantı.Close();
        }
        private void sifreUyeText_TextChanged(object sender, EventArgs e)
        {
            sifreUyeText.PasswordChar = '•';
        }
        private void textBox2_TextChanged(object sender, EventArgs e) //passwordchar sembol 
        {
            sifreGirisText.PasswordChar = '•';
        }
        Random rnd = new Random();
        String onaykodu;
        private void pictureBox2_Click(object sender, EventArgs e) //Doğru gir kontrolü
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:SAVAS\\sistem1.mdb\"";
            OleDbConnection connection = new OleDbConnection(connectionString);
            try
            {
                connection.Open();
                string email = kullaniciadiText.Text;
                string query = "SELECT * FROM kayıt WHERE email = '" + email + "'";
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string Sifre = reader["sifre"].ToString();
                    MailMessage message = new MailMessage();
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("", "");
                    client.Host = "smtp.office365.com";
                    client.EnableSsl = true;
                    message.To.Add(email);
                    client.Port = 587;
                    message.From = new MailAddress("");
                    message.Subject = " Aktivasyon Şifre";

                    // Rastgele şifre oluşturma işlemi
                    Random random = new Random();
                    int randomPassword = random.Next(1000, 10000);
                    string passwordText = "Şifre: " + randomPassword.ToString();
                    message.Body = passwordText;

                    client.Send(message);
                    MessageBox.Show("Mail adresinize aktivasyon şifresi gönderildi.");

                    onaykodu = randomPassword.ToString(); // onaykodu değişkenine atama yapılıyor
                }
                else if (string.IsNullOrEmpty(textBox1.Text))
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            sifremiunuttum menu = new sifremiunuttum();
            menu.ShowDialog();
            this.Close();
        }
        private void hatırla_CheckedChanged(object sender, EventArgs e)
        {
            if (hatırla.Checked == true)
            {
                Properties.Settings.Default.UserName = kullaniciadiText.Text; //kullanıcının uygulamaya girerken girdiği kullanıcı adını, uygulama ayarlarına kaydetmek için kullanılır.
                Properties.Settings.Default.Password = sifreGirisText.Text;
            }

            Properties.Settings.Default.Save();//Settings.settings kısmında ayarları kaydederek kullanılacak son ayarların güncellenmesini sağlar.
        }
        private void login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                kullaniciadiText.Text = Properties.Settings.Default.UserName;
                //önceden kaydedilmiş kullanıcı adı varsa sonraki sefer uygulamayı açtığında kaydedilmiş kullanıcı adı TextBox'ında görüntülenmesini sağlar.
                sifreGirisText.Text = Properties.Settings.Default.Password;
                hatırla.Checked = true;//uygulama ilk yüklendiğinde daha önce kaydedilmiş bir kullanıcı adı ve şifre varsa işaretler
            }
        }
        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nasıloynanır yeni = new nasıloynanır();
            yeni.Show();
        }
        private void hakkımızdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hakkimizda gecis = new hakkimizda();
            gecis.Show();
        }
        private void bizeUlaşınToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bizeulasin gecis = new bizeulasin();
            gecis.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)//kapatma
        {
            Application.Exit();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void admimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admingiris gecis = new admingiris();
            gecis.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == onaykodu)
            {
                MessageBox.Show("Kayıt oluşturuldu.");
                string girisKullaniciAdi = kullaniciadiText.Text;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"";
                OleDbConnection bag = new OleDbConnection(connectionString);
                bag.Open();
                OleDbCommand giris = new OleDbCommand("SELECT * FROM kayıt WHERE email = @email AND sifre = @sifre", bag);

                giris.Parameters.AddWithValue("@email", girisKullaniciAdi);
                giris.Parameters.AddWithValue("@sifre", sifreGirisText.Text);
                OleDbDataReader oku = giris.ExecuteReader();
                if (oku.Read())
                { 
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş");
                    bag.Close();
                }
                if (hatırla.Checked == true)
                {
                    Properties.Settings.Default.UserName = kullaniciadiText.Text;
                    Properties.Settings.Default.Password = sifreGirisText.Text;
                }
                else
                {
                    Properties.Settings.Default.UserName = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                }
                Properties.Settings.Default.Save();

                Arayuz menu = new Arayuz();
                menu.ShowDialog();
                bag.Close();
                this.Hide();
            }
        }
        private void skorlarımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kullaniciskor gecis = new kullaniciskor();
            gecis.Show();
        }
    }
}


