using Savas.Library.Enum;
using Savas.Library.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Library.Abstract
{
    internal abstract class Cisim: PictureBox,IHareketEden
    {
        public Size HareketAlaniBoyutlari { get; }

        public int HareketMesafesi { get; protected set; }

        public new int Right
        {
            get => base.Right;//türetilmiş sınıfın Right özelliğini temel sınıfın Right özelliğine yönlendirir
            set => Left = value - Width;
            //Right özelliğine bir değer atandığında bu değeri temel sınıfın (base) Left özelliği üzerinden Width
            //değerini çıkartarak hesaplar. Böylece, sağ kenarın x koordinatı, atanan değer ve nesnenin genişliği
            //(Width) arasındaki ilişkiye göre ayarlanır.
        }

        public new int Bottom 
        {
            get => base.Bottom;
            set => Top = value - Height;
        }
        public int Center
        {
            get => Left + Width / 2;
            set=> Left = value - Width / 2;
        }
        public int Middle
        {
            get => Top + Height / 2;
            set => Top = value - Height / 2;
        }
        protected Cisim(Size hareketAlaniBoyutlari) //public de kullanılabilirdi
        {
            //Image = Image.FromFile($@"C:\Users\asus\Desktop\SAVAS\Savas.Desktop\bin\Debug\resim\{GetType().Name}.png");
            Image = Image.FromFile($@"C:\SAVAS\Savas.Desktop\bin\Debug\resim\{GetType().Name}.png");
            HareketAlaniBoyutlari = hareketAlaniBoyutlari;
            /*ucaksavar class içerisine de yazabilirdim ancak oyuna ekleyeceğim cisimlerde resmin boyutu kadar olması lazım AUTOSIZE
            çünkü oyun ekranından dışına çıkmaması gerek onuun için pictureboxların sağ ve sol kısımlarda boşlukların olmaması lazım*/
            SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public bool HareketEttir(Yon yon)
        {
            switch(yon)
            {
                case Yon.Yukari:
                    return YukariHareketEttir(); 
                case Yon.Saga:
                    return SagaHareketEttir();
                case Yon.Asagi:
                    return AsagiHareketEttir();
                case Yon.Sola:
                    return SolaHareketEttir();
                default:
                    throw new ArgumentOutOfRangeException(nameof(yon), yon, null);
            }
        }

        private bool SolaHareketEttir()
        {
            if (Left == 0) return true; //çarptıysa true değer 
            var yeniLeft = Left - HareketMesafesi;
            var tasacakMi = yeniLeft < 0;
            Left = tasacakMi ? 0 : yeniLeft; //resimin sol kenarı 0dan küçük olursa dışarı çıkar eğer tasarsa left konumu 0 olmalı taformun dışına çıkmıyacaksa(:) yeniLeft oolamli
            return Left == 0;

        }

        private bool AsagiHareketEttir()
        {
            if (Bottom == HareketAlaniBoyutlari.Height) return true;
            var yeniBottom = Bottom + HareketMesafesi;
            var tasacakMi = yeniBottom > HareketAlaniBoyutlari.Height;
            Bottom = tasacakMi ? HareketAlaniBoyutlari.Height : yeniBottom; //Right yazınca hata aldık bu yüzden right değişkenmiş gibi davranıp left üzerinden righti hessaplamam lazım

            return Bottom == HareketAlaniBoyutlari.Height;

        }

        private bool SagaHareketEttir()
        {
            if (Right == HareketAlaniBoyutlari.Width) return true; 
            var yeniRight= Right + HareketMesafesi;
            var tasacakMi = yeniRight > HareketAlaniBoyutlari.Width;
            Right = tasacakMi ? HareketAlaniBoyutlari.Width : yeniRight; //Right yazınca hata aldık bu yüzden right değişkenmiş gibi davranıp left üzerinden righti hessaplamam lazım
           
            return Right == HareketAlaniBoyutlari.Width;

        }

        private bool YukariHareketEttir()
        {
            //sola hareket ile asagı yukarı aynı 
            if (Top == 0) return true; 
            var yeniTop = Top - HareketMesafesi;
            var tasacakMi = yeniTop < 0;
            Top = tasacakMi ? 0 : yeniTop; 
            return Top == 0;
        }


    }
}
