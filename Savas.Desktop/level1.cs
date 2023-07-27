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
    public partial class level1 : Form
    {
        private readonly Oyun _oyun;
        private int skor;

        public level1()
        {
            InitializeComponent();
            _oyun = new Oyun(ucaksavarPanel, SavasAlaniPanel, panel1);
            _oyun.GecenSureDegisti += Oyun_GecenSureDegisti;
        }

        private void level1_KeyDown(object sender, KeyEventArgs e)
        {
            

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _oyun._levelOne = true;
                    _oyun._levelTwo = false;
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

        private void Oyun_GecenSureDegisti(object sender, EventArgs e)
        {
           
            surelabel.Text = _oyun.GecenSure.ToString(@"m\:ss");
            if (_oyun._firstLevelFinish)
            {
                l2 gecis = new l2();
                gecis.Show();
                this.Hide();
                _oyun.ZamanlayicilariDurdur();
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
