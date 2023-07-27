using Savas.Library.Enum;
using Savas.Library.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Savas.Library.Concrete
{
    public partial class l3 : Form
    {
        //private oyun değişkeninin sadece tanımlandığı sınıf içinde erişilebilir olduğu
        //oyun değişkenine başlangıçta bir değer atanır ve bu değer değiştirilemez(readonly)
        private readonly Oyun _oyun;
        public l3()
        {
            InitializeComponent();
            _oyun = new Oyun(ucaksavarPanel, SavasAlaniPanel, panel1);
            _oyun.GecenSureDegisti += Oyun_GecenSureDegisti;
        }
        private void Oyun_GecenSureDegisti(object sender, EventArgs e)
        {
            surelabel.Text = _oyun.GecenSure.ToString(@"m\:ss");
            if (_oyun._thirdLevelFinish)
            {
                _oyun.ZamanlayicilariDurdur();
            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void l3_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _oyun._levelOne = false;
                    _oyun._levelTwo = false;
                    _oyun._levelThree = true;
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
    }
}
