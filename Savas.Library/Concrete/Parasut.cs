using Savas.Library.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas.Library.Concrete
{
    internal class Parasut : Cisim
    {
        private static readonly Random Random = new Random();//static yapıldı tek bir random nesnesi olur her uçağa özel olarak random oluşturmaz
        public Parasut(Size hareketAlaniBoyutlari) : base(hareketAlaniBoyutlari)
        {
            HareketMesafesi = (int)(Height * .1); //.1= 1/10
            Left = Random.Next(hareketAlaniBoyutlari.Width - Width + 1); //panel genişliği - ucağın kendi genişliği
            //+1 dahil yazılan değerin 1 eksiğini kabul ettiği için +1 ile konumu eşitledik
        }
        public Mermi VurulduMu(List<Mermi> mermiler)
        {
            foreach (var mermi in mermiler)
            {
                var vurulduMu = mermi.Top < Bottom && mermi.Right > Left && mermi.Left < Right;
                if (vurulduMu) return mermi;

            }
            return null; //hiçbir merminin beni vurmadığı anlamına gelir (null)
        }
    }
}
