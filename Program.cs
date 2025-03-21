using System;

namespace MayinTarlasi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OyunYonetici oyunYonetici = new OyunYonetici();
            oyunYonetici.Basla();
            Console.ReadLine();
        }
    }
}
