using Savas.Library.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas.Library.Interface
{
    internal interface IHareketEden
    {
        Size HareketAlaniBoyutlari { get; }
        int HareketMesafesi { get; }

        /* "///" yazıp enter yazarsak aşağıdaki açıklama kısmını verir */
        /// <summary>
        /// Cismi istenilen yöne hareket ettirir
        /// </summary>
        /// <param name="yon">Hangi yöne hareket edileceği </param>
        /// <returns> Cisim duvara çarpar ise TRUE dönsün </returns>
        bool HareketEttir(Yon yon);

    }
}
