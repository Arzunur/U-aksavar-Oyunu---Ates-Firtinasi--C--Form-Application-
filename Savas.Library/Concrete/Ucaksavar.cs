using Savas.Library.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Library.Concrete
{
    internal class Ucaksavar : Cisim //ucaksavar cisim dedik aslında cisim sınıfını(cs) picturebox tanımladık 
    {
        public Ucaksavar(int panelGenisligi,Size hareketAlaniBoyutlari): base(hareketAlaniBoyutlari)
        {

            Center = panelGenisligi / 2; //panel genisliğinden ucaksavarın genisliğini çıkartıp /2 bölersem ucaksavar orta noktasında baslar
            HareketMesafesi = Width;//kendi boyutu kadar hareket ettirmek (ucaksavarın)
        }
    }
}
