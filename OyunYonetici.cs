using System;
using System.Threading.Channels;

namespace MayinTarlasi
{
    public class OyunYonetici
    {
        private OyunTahtasi oyunTahtasi;
        private int boyut;

        public void Basla()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("====Mayın Tarlası Oyununa Hoşgeldiniz====");
                boyut = OyunSeviyesi();
                oyunTahtasi = new OyunTahtasi(boyut);
                oyunTahtasi.MayinYerlestir();
                oyunTahtasi.TahtaYazdır();
                OyunDongusu();

                Console.WriteLine("\nTekrar oynamak ister misiniz? (Evet için 'E', Hayır için 'H' tuşlayın)");
            } while (TekrarOynama());
        }

        private int OyunSeviyesi()
        {
            Console.WriteLine("1 - Kolay: 5x5\n2 - Orta: 8x8\n3 - Zor: 10x10");
            Console.Write("Bir seviye seçin: ");
            int secilen;
            while (!int.TryParse(Console.ReadLine(), out secilen) || secilen < 1 || secilen > 3)
            {
                Console.WriteLine("Lütfen geçerli bir seçenek girin (1, 2 veya 3)!");
            }

            return secilen switch
            {
                1 => 5,
                2 => 8,
                3 => 10
            };
        }

        public void OyunDongusu()
        {
            while (true)
            {
                Console.Write("Satır girin: ");
                int satir = KullaniciGirdisiAl(boyut);
                Console.Write("Sütun girin: ");
                int sutun = KullaniciGirdisiAl(boyut);
                oyunTahtasi.HucreAc(satir, sutun);

                if (oyunTahtasi.MayinMi(satir, sutun))
                {
                    oyunTahtasi.TahtaYazdır();
                    Console.WriteLine("\nBOOM! Mayına bastınız. Oyun bitti!");                   
                    break;
                }

                if (oyunTahtasi.TumGuvenliAlanlarAcildiMi())
                {
                    oyunTahtasi.TahtaYazdır();
                    Console.WriteLine("\nTebrikler! Tüm güvenli hücreleri açtınız.");
                    break;
                }

                oyunTahtasi.TahtaYazdır();
            }
        }

        private int KullaniciGirdisiAl(int max)
        {
            int deger;
            while (!int.TryParse(Console.ReadLine(), out deger) || deger < 0 || deger >= max)
            {
                Console.WriteLine($"Lütfen 0 ile {max - 1} arasında bir sayı girin.");
            }
            return deger;
        }
        private bool TekrarOynama()
        {
            string cevap = Console.ReadLine().ToUpper();
            return cevap == "E"; // "E" tuşuna basarsa oyun tekrar başlar
        }
    }
}
