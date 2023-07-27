using Savas.Library.Concrete;
using Savas.Library.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Savas.Desktop.l2;

namespace Savas.Desktop
{
    public partial class l2 : Form
    {
        private readonly Oyun _oyun;//Oyun class ismi
        public l2()
        {
            InitializeComponent();
            _oyun = new Oyun(ucaksavarPanel, SavasAlaniPanel, panel1);
            _oyun.GecenSureDegisti += Oyun_GecenSureDegisti;
        }

        private void Oyun_GecenSureDegisti(object sender, EventArgs e)
        {
            //Alternatif kod olarak kullanılabilir -->  surelabel.Text = $"{_oyun.GecenSure.Minutes}:{_oyun.GecenSure.Seconds.ToString("D2")}"; //saniyeyi 2 haneli yazmak D2 
            surelabel.Text = _oyun.GecenSure.ToString(@"m\:ss"); 
            if (_oyun._secondLevelFinish)
            {
                l3 gecis = new l3();
                gecis.Show();
                this.Hide();
                _oyun.ZamanlayicilariDurdur();
            }
        }

        private void l2_KeyDown(object sender, KeyEventArgs e)
        {
            // Hangi tuşa basıldığını söyler--->MessageBox.Show(e.KeyCode.ToString());

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _oyun._levelOne = false;
                    _oyun._levelTwo = true;
                    _oyun._levelThree = false;
                    _oyun.Baslat();
                    break;
                case Keys.Right:
                    _oyun.UcaksavariHareketEttir(Yon.Saga);
                    break;
                case Keys.Left:
                    _oyun.UcaksavariHareketEttir(Yon.Sola);
                    break;
                case Keys.Space:
                    _oyun.AtesEt();
                    break;
            }
        }
        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            login gecis = new login();
            gecis.Show();
            this.Hide();
        }
    }
}
