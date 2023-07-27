using Savas.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas.Library.Interface
{
    //internal kullanmak sadece o assembly içerisinde kullanmak demek (Savas.Library)
    internal interface IOyun
    {
        event EventHandler GecenSureDegisti; //olayın gerçekleştiğinde belirli bir işlemi veya metodu tetiklemeyi sağlar.
        //internal içerisinde erişim belirteçleri (public,private) yazılmaz.
        bool DevamEdiyorMu { get; }
        
        TimeSpan GecenSure { get; }

        void Baslat();
        void AtesEt();
        void UcaksavariHareketEttir(Yon yon);
        
    }
}
