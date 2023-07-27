using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Desktop
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=SAVAS\\skor.mdb\"";
        private DataSet ds = new DataSet(); // verileri tutacak olan DataSet

        private void verigoster(string veriler)
        {
            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                baglanti.Open();

                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = new OleDbCommand(veriler, baglanti);

                if (veriler == "Select * from skor")
                {
                    dataGridView1.DataSource = null; // Önceki verileri temizle
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else if (veriler == "SELECT * FROM skor WHERE tarih = @tarih")
                {
                    dataGridView2.DataSource = null; // Önceki verileri temizle
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();

                    da.SelectCommand.Parameters.AddWithValue("@tarih", "Belirli bir tarih değeri"); // Değeri istediğiniz tarih değeriyle değiştirin
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
                else if (veriler == "SELECT * FROM skor WHERE skor = @skor")
                {
                    dataGridView3.DataSource = null; // Önceki verileri temizle
                    dataGridView3.Rows.Clear();
                    dataGridView3.Columns.Clear();

                    da.SelectCommand.Parameters.AddWithValue("@skor", "Belirli bir skor değeri"); // Değeri istediğiniz skor değeriyle değiştirin
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView3.DataSource = dt;
                }

                baglanti.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition);
        }

        private void gÖRÜNTÜLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM skor";

                try
                {
                    baglanti.Open();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, baglanti);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri görüntüleme işlemi sırasında bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                baglanti.Open();

                OleDbCommand komut = new OleDbCommand("DELETE FROM skor WHERE tarih = @tarih", baglanti);
                komut.Parameters.AddWithValue("@tarih", dataGridView1.SelectedRows[0].Cells["tarih"].Value);
                komut.ExecuteNonQuery();

                baglanti.Close();

                // Veriyi güncellemek için verigoster metodunu yeniden çağırabilirsiniz.
                verigoster("Select * from skor");
            }
        }

        private void button2_Click(object sender, EventArgs e) //GÜN
        {
            string tarih = DateTime.Today.ToString("yyyy-MM-dd");
            string sorgu = "SELECT * FROM skor WHERE tarih LIKE @tarih";

            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu, baglanti))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@tarih", "%" + tarih + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        dataGridView2.DataSource = null;
                        MessageBox.Show("Bulunduğunuz güne ait veri bulunamadı.");
                    }
                }
            }
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            this.Close(); // Mevcut formu kapat
            login gecis = new login();
            gecis.Show(); // Yeni formu görüntüle
        }
        private void button3_Click(object sender, EventArgs e)//AY
        {
            DateTime bugun = DateTime.Today;
            string ay = bugun.ToString("MM");
            string yil = bugun.ToString("yyyy");
            string tarih = $"{yil}-{ay}";

            string sorgu = "SELECT * FROM skor WHERE tarih LIKE @tarih";

            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu, baglanti))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@tarih", $"{tarih}%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView3.DataSource = dt;
                    }
                    else
                    {
                        dataGridView3.DataSource = null;
                        MessageBox.Show("Bulunduğunuz ayda veri bulunamadı.");
                    }

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)//YIL
        {
            DateTime bugun = DateTime.Today;
            string yil = bugun.Year.ToString();
            string sorgu = "SELECT * FROM skor WHERE tarih >= @baslangicTarihi AND tarih < @bitisTarihi";

            DateTime baslangicTarihi = new DateTime(int.Parse(yil), 1, 1);
            DateTime bitisTarihi = baslangicTarihi.AddYears(1);

            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sorgu, baglanti))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@baslangicTarihi", baslangicTarihi);
                    adapter.SelectCommand.Parameters.AddWithValue("@bitisTarihi", bitisTarihi);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView4.DataSource = dt;
                    }
                    else
                    {
                        dataGridView4.DataSource = null;
                        MessageBox.Show("Bulunduğunuz yıla ait veri bulunamadı.");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)//Bulunduğu aya göre en yüksek 5 veri (skor)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:SAVAS\\skor.mdb\"";

            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                baglanti.Open();

                string sql = "SELECT TOP 5 * FROM skor ORDER BY CInt(skor) DESC";

                // Veritabanından verileri seçin
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, baglanti);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Verileri DataGridView'e aktarın
                dataGridView5.DataSource = table;
            }
        }
        private void button6_Click(object sender, EventArgs e)//10  
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:SAVAS\\skor.mdb\"";

            using (OleDbConnection baglanti = new OleDbConnection(connectionString))
            {
                baglanti.Open();

                string sql = "SELECT TOP 10 * FROM skor ORDER BY CInt(skor) DESC";

                // Veritabanından verileri seçin
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, baglanti);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Verileri DataGridView'e aktarın
                dataGridView6.DataSource = table;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dosyaYolu = openFileDialog.FileName; //Seçilen dosyanın yolunu al
                pictureBox2.Image = Image.FromFile(dosyaYolu);// Dosyanın png, jpg içeriğini olarak oku
            }
        }
    }
}

