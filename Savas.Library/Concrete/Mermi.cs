using Savas.Library.Abstract;
using System;
using Savas.Library.Abstract;
using System.Drawing;

namespace Savas.Library.Concrete
{
    internal class Mermi:Cisim
    {
        public Mermi(Size hareketAlaniBoyutlari,int namluOrtasiX) : base(hareketAlaniBoyutlari)
        {
            //baslangıç konumunu Mermi sınıfı içerisinde ayarlanmalı
          //Metot yazmayadabilirdim ama kodu inceleme açısından kolaylık sağlansın nerede ne yapıldığını açıkça görebilmek için  
            BaslangicKonumunuAyarla(namluOrtasiX);
            HareketMesafesi = (int) (Height * 0.5d); //mesafeyi merminin yük yarısını aldık 0.5 double old için(d) koyduk
                                                     //sonucu int çevirmesi için de (int)() şeklinde dönüşümü tamamladık 
        }

        private void BaslangicKonumunuAyarla(int namluOrtasiX)
        {
            //Ucaksavar(orta nok.) nerede ise mermi de orada oluşması lazım
            Bottom = HareketAlaniBoyutlari.Height;
            Center = namluOrtasiX;
        }
    }
}
