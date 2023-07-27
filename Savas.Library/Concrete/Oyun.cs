using Savas.Library.Enum;
using Savas.Library.Interface;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label; // kütüphane eklenmediğinde private Label _skorLabel; kısmında hata alınıyor

namespace Savas.Library.Concrete
{
    public class Oyun : IOyun
    {
        //Oyun sınıfı IOyun arayüzündeki tanımlanan yöntemleri ve özellikleri içermeli ve onları gerçekleştirmelidir.


        public bool _levelOne, _levelTwo, _levelThree;
        public bool _firstLevelFinish, _secondLevelFinish, _thirdLevelFinish;

        #region Alanlar 

        private readonly Timer _gecenSureTimer = new Timer { Interval = 1000 }; //Timer oluşturduk
        private readonly Timer _hareketTimer = new Timer { Interval = 100 };//sn 1/10 
                                                                            //!! _ucakOlusturmaTimer Interval = 2000 azaltılırsa oyunun zorluk seviyesini arttırmış oluruz p 
        private readonly Timer _bombaOlusturmaTimer = new Timer { Interval = 9000 };
        private readonly Timer _ucakOlusturmaTimer = new Timer { Interval = 2000 }; //2sn
        private readonly Timer _parasutOlusturmaTimer = new Timer { Interval = 5000 }; //5sn
        private readonly Timer _heartOlusturmaTimer = new Timer { Interval = 20000 };
        private TimeSpan _gecenSure;
        private readonly Panel _ucaksavarPanel;
        private readonly Panel _savasAlaniPanel;
        private readonly Panel _panel1;
        private Ucaksavar _ucaksavar;
        private readonly List<Mermi> _mermiler = new List<Mermi>();
        private readonly List<Ucak> _ucaklar = new List<Ucak>();
        private readonly List<Parasut> _parasutler = new List<Parasut>();
        private readonly List<Bomba> _bombalar = new List<Bomba>();
        private readonly List<Heart> _hearts = new List<Heart>();
        private int _puan = 0; // Puan değişkeni

        private Label _skorLabel; // Skor Label kontrolü


        #endregion

        #region Olaylar 

        public event EventHandler GecenSureDegisti;

        #endregion

        #region Özellikler 

        public bool DevamEdiyorMu { get; private set; }
        public TimeSpan GecenSure
        {
            get => _gecenSure;
            private set
            {
                _gecenSure = value;
                GecenSureDegisti?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Metotlar
        public Oyun(Panel ucaksavarPanel, Panel savasAlaniPanel, Panel panel1) //ctor Tab yazınca oto oluşturur 
        {

            _ucaksavarPanel = ucaksavarPanel;
            _savasAlaniPanel = savasAlaniPanel;
            _panel1 = panel1;
            _gecenSureTimer.Tick += GecenSureTimer_Tick;
            _hareketTimer.Tick += HareketTimer_Tick;
            _ucakOlusturmaTimer.Tick += UcakOlusturmaTimer_Tick;
            _parasutOlusturmaTimer.Tick += ParasutOlusturmaTimer_Tick;
            _bombaOlusturmaTimer.Tick += BombaOlusturmaTimer_Tick;
            _heartOlusturmaTimer.Tick += HeartOlusturmaTimer_Tick;
        }

        private void MermileriHareketEttir()
        {
            for (int i = _mermiler.Count - 1; i >= 0; i--) //(int i = _mermiler.Count - 1; i>=0; i--)
            {
                var mermi = _mermiler[i];
                var carptiMi = mermi.HareketEttir(Yon.Yukari);
                if (carptiMi)
                {
                    _mermiler.Remove(mermi);//sadece list silmek yetmiyor 
                    _savasAlaniPanel.Controls.Remove(mermi);//oyun panelinden de mmermiyi silmem lazım
                }
            }
        }

        private void GecenSureTimer_Tick(object sender, EventArgs e)
        {
            GecenSure += TimeSpan.FromSeconds(1);
        }
        private void BombaOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            BombaOlustur();
        }
        private void HeartOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            HeartOlustur();
        }
        private void HareketTimer_Tick(object sender, EventArgs e)
        {
            MermileriHareketEttir();
            UcaklariHareketEttir();
            VurulanUcaklariCikar();
            ParasutleriHareketEttir();
            VurulanParasutleriCikar();
            BombalariHareketEttir();
            VurulanBombalariCikar();
            HeartsHareketEttir();
            VurulanHeartsCikar();
        }

        private void VurulanBombalariCikar()
        {
            for (var i = _bombalar.Count - 1; i >= 0; i--)
            {
                var bomba = _bombalar[i];

                var vuranMermi = bomba.VurulduMu(_mermiler);
                if (vuranMermi is null) continue;

                _bombalar.Remove(bomba);
                _mermiler.Remove(vuranMermi);
                _savasAlaniPanel.Controls.Remove(bomba);
                _savasAlaniPanel.Controls.Remove(vuranMermi);
                // Ses çalma işlemi
                var sesDosyasi = "C:\\Users\\Desktop\\SAVAS - Kopya\\Savas.Desktop\\bin\\Debug\\resim\\bomba.wav"; // Ses dosyasının yolunu ve adını belirtin
                var player = new System.Media.SoundPlayer(sesDosyasi);
                player.Play();

                // Puanı 20 azalt
                _puan -= 20;

                // Skor labelını güncelle
                if (_skorLabel is null)
                {
                    _skorLabel = new Label();
                    _skorLabel.Location = new Point(420, 30);
                    _skorLabel.ForeColor = Color.White;
                    _skorLabel.Font = new Font("Diary of an 8-bit mage", 12, FontStyle.Regular);
                    _panel1.Controls.Add(_skorLabel);
                }

                // Skor labelını oluştur ve konumunu ayarla
                var skorLabel = new Label();
                skorLabel.Text = "-20";
                skorLabel.ForeColor = Color.Red;
                skorLabel.Font = new Font("Diary of an 8-bit mage", 15, FontStyle.Bold);
                skorLabel.AutoSize = true;
                skorLabel.Location = new Point(bomba.Location.X + bomba.Width, bomba.Location.Y);
                _savasAlaniPanel.Controls.Add(skorLabel);

                // Timer kullanarak label'ı belirli bir süre sonra kaldır
                var timer = new Timer();
                timer.Interval = 2000; // 2 saniye sonra kaldırılacak
                timer.Tick += (sender, e) =>
                {
                    _savasAlaniPanel.Controls.Remove(skorLabel);
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();

                _skorLabel.Text = "Skor: " + _puan.ToString();
            }
        }
        private void BombalariHareketEttir()
        {
            foreach (var bomba in _bombalar)
            {
                var carptiMİ = bomba.HareketEttir(Yon.Asagi);
                if (!carptiMİ)
                {
                    continue;
                }
                else
                {
                    bomba.Visible = false;
                }
            }
        }
        private void HeartsHareketEttir()
        {
            foreach (var heart in _hearts)
            {
                var carptiMİ = heart.HareketEttir(Yon.Asagi);
                if (!carptiMİ)
                {
                    continue;
                }
                else
                {
                    heart.Visible = false;
                }
            }
        }

        private void VurulanHeartsCikar()
        {
            for (var i = _hearts.Count - 1; i >= 0; i--)
            {
                var heart = _hearts[i];

                var vuranMermi = heart.VurulduMu(_mermiler);
                if (vuranMermi is null) continue;

                _hearts.RemoveAt(i); // Kalbi listeden kaldır
                _savasAlaniPanel.Controls.Remove(heart); // Kalbi panele ekliyse paneleden kaldır
                _mermiler.Remove(vuranMermi); // Vuran mermiyi listeden kaldır
                _savasAlaniPanel.Controls.Remove(vuranMermi); // Vuran mermiyi panele ekliyse paneleden kaldır

            }
        }

        private void VurulanParasutleriCikar()
        {
            for (var i = _parasutler.Count - 1; i >= 0; i--)
            {
                var parasut = _parasutler[i];

                var vuranMermi = parasut.VurulduMu(_mermiler);
                if (vuranMermi is null) continue;

                _parasutler.Remove(parasut);
                _mermiler.Remove(vuranMermi);
                _savasAlaniPanel.Controls.Remove(parasut);
                _savasAlaniPanel.Controls.Remove(vuranMermi);

                // Label oluştur ve puanı 30 artır
                var label = new Label();
                label.Text = "+30";
                label.ForeColor = Color.Fuchsia;
                label.Font = new Font("Arial", 12, FontStyle.Bold);
                label.AutoSize = true;
                label.Location = parasut.Location; // Paraşütün konumuna göre labelın konumunu ayarla
                _savasAlaniPanel.Controls.Add(label);

                _puan += 30; // Puanı 30 artır
                if (_skorLabel is null)
                {
                    _skorLabel = new Label();
                    _skorLabel.Location = new Point(420, 30); // Skor Label kontrolünün konumunu ayarla
                    _skorLabel.ForeColor = Color.White; // Yazı rengini beyaz olarak ayarla
                    _skorLabel.Font = new Font("Diary of an 8-bit mage", 12, FontStyle.Regular);  // _skorLabel.Font = new Font("Arial", 12, FontStyle.Regular); // Arial fontunda 24 punto büyüklüğünde ayarla
                    _panel1.Controls.Add(_skorLabel);
                }
                _skorLabel.Text = "Skor: " + _puan.ToString();

                // Timer kullanarak labelı belirli bir süre sonra kaldır
                var timer = new Timer();
                timer.Interval = 2000; // 2 saniye sonra kaldırılacak
                timer.Tick += (sender, e) =>
                {
                    _savasAlaniPanel.Controls.Remove(label);
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }
        private void ParasutleriHareketEttir()
        {
            foreach (var parasut in _parasutler)
            {
                var carptiMİ = parasut.HareketEttir(Yon.Asagi);
                if (!carptiMİ)
                {
                    continue;
                }
                else
                {
                    parasut.Visible = false;
                }
            }
        }
        private void VurulanUcaklariCikar()
        {
            for (var i = _ucaklar.Count - 1; i >= 0; i--)
            {
                var ucak = _ucaklar[i];

                var vuranMermi = ucak.VurulduMu(_mermiler); // Her uçağın vurulup vurulmadığını kontrol ediyoruz
                if (vuranMermi is null) continue; // Uçak vurulmamışsa diğer uçakla devam et

                _ucaklar.Remove(ucak);
                _mermiler.Remove(vuranMermi);
                _savasAlaniPanel.Controls.Remove(ucak);
                _savasAlaniPanel.Controls.Remove(vuranMermi);

                // Puanı 10 artır ve label oluştur
                var label = new Label();
                label.Text = "+10";
                label.ForeColor = Color.White;
                label.Font = new Font("Arial", 12, FontStyle.Bold);
                label.AutoSize = true;
                label.Location = ucak.Location; // Labelın konumunu uçağın konumuna ayarla
                _savasAlaniPanel.Controls.Add(label);

                _puan += 10; // Puanı 10 artır

                if (_skorLabel is null)
                {
                    _skorLabel = new Label();
                    _skorLabel.Location = new Point(420, 30); // Skor Label kontrolünün konumunu ayarla
                    _skorLabel.ForeColor = Color.White; // Yazı rengini beyaz olarak ayarla
                    _skorLabel.Font = new Font("Diary of an 8-bit mage", 12, FontStyle.Regular);
                    _panel1.Controls.Add(_skorLabel);
                }
                _skorLabel.Text = "Skor: " + _puan.ToString();

                // Labelı belirli bir süre sonra kaldırmak için Timer kullan
                var timer = new Timer();
                timer.Interval = 2000; // 2 saniye sonra kaldırılacak
                timer.Tick += (sender, e) =>
                {
                    _savasAlaniPanel.Controls.Remove(label);
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }
        private void UcaklariHareketEttir()
        {
            foreach (var ucak in _ucaklar)
            {
                var carptiMİ = ucak.HareketEttir(Yon.Asagi);
                if (!carptiMİ) continue; //çarpmadıysa diğer ucağa geçmesi 
                Bitir();
                break;
            }
        }
        private void UcakOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            UcakOlustur();
        }
        private void ParasutOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            ParasutOlustur();
        }
        public void AtesEt()
        {
            if (!DevamEdiyorMu) return; //oyun devam etmiyorsa çıkmalı ki oyun başlamadan önce mermi oluşumu engellensin.Oyun başladığı zmn oluşsun.
            var mermi = new Mermi(_savasAlaniPanel.Size, _ucaksavar.Center);
            _mermiler.Add(mermi);
            _savasAlaniPanel.Controls.Add(mermi);//panelin üzerine eklemem için controlden alması gerek. PictureBox ise bir control olduğunu unutmamak lazım 
                                                 //  mermi.HareketEttir(Yon.Yukari); bu kod ile mermiler hareket ettirebiliriz ancak tüm mermilere bu komutu yazmam gerekecek onun yerine 
                                                 //mermilerin oluştuğu bir liste oluşturmalıyım ki yazarken kolaylk olsun List<Mermi> _mermiler ooluşturdum
        }

        public void Baslat()
        {
            if (DevamEdiyorMu) return;
            DevamEdiyorMu = true;
            ZamanlayicilariBaslat();
            UcaksavarOlustur();
            UcakOlustur();
            ParasutOlustur();
            //BombaOlustur();


            _skorLabel = new Label();
            _skorLabel.Text = "Skor: 0";
            _skorLabel.Location = new Point(420, 30);
            _skorLabel.ForeColor = Color.White;
            _skorLabel.Font = new Font("Diary of an 8-bit mage", 12, FontStyle.Regular);
            _panel1.Controls.Add(_skorLabel);

        }

        private void BombaOlustur()
        {
            var bomba = new Bomba(_savasAlaniPanel.Size);
            _bombalar.Add(bomba);
            _savasAlaniPanel.Controls.Add(bomba);
        }
        private void HeartOlustur()
        {
            var heart = new Heart(_savasAlaniPanel.Size);
            _hearts.Add(heart);
            _savasAlaniPanel.Controls.Add(heart);
        }

        private void ParasutOlustur()
        {
            var parasut = new Parasut(_savasAlaniPanel.Size);
            _parasutler.Add(parasut);
            _savasAlaniPanel.Controls.Add(parasut);
        }

        private void UcakOlustur()
        {

            if (_levelOne)
            {
                var ucak = new Ucak(_savasAlaniPanel.Size);
                _ucaklar.Add(ucak);
                _savasAlaniPanel.Controls.Add(ucak);
                _bombaOlusturmaTimer.Stop();
                _heartOlusturmaTimer.Stop();

            }
            else if (_levelTwo) //Bomba ve Uçak Oluşturma 
            {
                var ucak = new Ucak(_savasAlaniPanel.Size);
                _ucaklar.Add(ucak);
                _savasAlaniPanel.Controls.Add(ucak);

                //_bombaOlusturmaTimer.Tick += BombaOlusturmaTimer_Tick;
                _bombaOlusturmaTimer.Start();

                var bomb = new Bomba(_savasAlaniPanel.Size);
                _bombalar.Add(bomb);
                _savasAlaniPanel.Controls.Add(bomb);
                _heartOlusturmaTimer.Stop();

            }
            else if (_levelThree)
            {

                var ucak = new Ucak(_savasAlaniPanel.Size);
                _ucaklar.Add(ucak);
                _savasAlaniPanel.Controls.Add(ucak);

                _heartOlusturmaTimer.Start();
                _bombaOlusturmaTimer.Stop();
            }
        }

        private void ZamanlayicilariBaslat()
        {
            _gecenSureTimer.Start();
            _hareketTimer.Start();
            _ucakOlusturmaTimer.Start();
            _parasutOlusturmaTimer.Start();
            _bombaOlusturmaTimer.Start();
            _heartOlusturmaTimer.Start();
        }
        private void UcaksavarOlustur()
        {
            _ucaksavar = new Ucaksavar(_ucaksavarPanel.Width, _ucaksavarPanel.Size);
            _ucaksavarPanel.Controls.Add(_ucaksavar); //ucaksavarı Panelin üzerinde görülebilecek
        }

        private void Bitir()
        {
            if (!DevamEdiyorMu) return;
            DevamEdiyorMu = false;

            if (_levelOne)
            {
                _firstLevelFinish = true;

                string skor = _skorLabel.Text.Replace("Skor: ", ""); // Skor değerini alın
                DateTime tarih = DateTime.Now;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Users\\asus\\Desktop\\SAVAS - Kopya\\skor.mdb\"";
                OleDbConnection bag = null;

                try
                {
                    bag = new OleDbConnection(connectionString);
                    bag.Open();
                    OleDbCommand command = new OleDbCommand("INSERT INTO skor (ad, skor, tarih) VALUES ( @ad, @skor, @tarih)", bag);
                    command.Parameters.AddWithValue("@ad", "Kolay"); // Sabit olarak "Kolay" değerini kullanın
                    command.Parameters.AddWithValue("@skor", skor); // Skor değişkenini kullanın
                    command.Parameters.AddWithValue("@tarih", tarih); // Tarihi uygun formata dönüştürün
                    command.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
                finally
                {
                    bag?.Close();
                }
            }
            else if (_levelTwo)
            {
                _secondLevelFinish = true;

                string skor = _skorLabel.Text.Replace("Skor: ", ""); // Skor değerini alın
                DateTime tarih = DateTime.Now;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Users\\asus\\Desktop\\SAVAS - Kopya\\skor.mdb\"";
                OleDbConnection bag = null;

                try
                {
                    bag = new OleDbConnection(connectionString);
                    bag.Open();
                    OleDbCommand command = new OleDbCommand("INSERT INTO skor (ad, skor, tarih) VALUES ( @ad, @skor, @tarih)", bag);
                    command.Parameters.AddWithValue("@ad", "Orta"); // Sabit olarak "Orta" değerini kullanın
                    command.Parameters.AddWithValue("@skor", skor); // Skor değişkenini kullanın
                    command.Parameters.AddWithValue("@tarih", tarih); // Tarihi uygun formata dönüştürün
                    command.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
                finally
                {
                    bag?.Close();
                }

            }
            else if (_levelThree)
            {
                _thirdLevelFinish = true;

                string skor = _skorLabel.Text.Replace("Skor: ", ""); // Skor değerini alın
                DateTime tarih = DateTime.Now;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Users\\asus\\Desktop\\SAVAS - Kopya\\skor.mdb\"";
                OleDbConnection bag = null;

                try
                {
                    bag = new OleDbConnection(connectionString);
                    bag.Open();
                    OleDbCommand command = new OleDbCommand("INSERT INTO skor (ad, skor, tarih) VALUES ( @ad, @skor, @tarih)", bag);
                    command.Parameters.AddWithValue("@ad", "Zor"); // Sabit olarak "Orta" değerini kullanın
                    command.Parameters.AddWithValue("@skor", skor); // Skor değişkenini kullanın
                    command.Parameters.AddWithValue("@tarih", tarih); // Tarihi uygun formata dönüştürün
                    command.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
                finally
                {
                    bag?.Close();
                }

            }
            #endregion
        }
        public void ZamanlayicilariDurdur()
        {
            _gecenSureTimer.Stop();
            _hareketTimer.Stop();
            _ucakOlusturmaTimer.Stop();
            _parasutOlusturmaTimer.Stop();
            _bombaOlusturmaTimer.Stop();
            _heartOlusturmaTimer.Stop();
        }

        public void UcaksavariHareketEttir(Yon yon)
        {
            if (!DevamEdiyorMu) return;//bu kod satırını yazmazsak oyuna başlatmadan sağ-sol yön tuşlarına basıldığında hata alırız.Bu hatayı almamak için 
            _ucaksavar.HareketEttir(yon);
        }
    }

}

