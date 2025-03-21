using System;
using System.Linq;

namespace MayinTarlasi
{
    public class OyunTahtasi
    {
        private char[,] tablo;
        private bool[,] acilanAlanlar;
        private int mayinSayisi;
        private bool patladiMi;
        public OyunTahtasi(int boyut)
        {
            tablo = new char[boyut, boyut];
            acilanAlanlar = new bool[boyut, boyut];
            mayinSayisi = boyut * boyut / 5;
            TabloBaslatici();
        }
        private void TabloBaslatici()
        {
            for (int i = 0; i < tablo.GetLength(0); i++)
                for (int j = 0; j < tablo.GetLength(1); j++)
                {
                    tablo[i, j] = '■';
                    acilanAlanlar[i, j] = false;
                }
        }

        public void TahtaYazdır()
        {
            Console.Clear();
            string sutunlar = string.Join("|", Enumerable.Range(0, tablo.GetLength(0)));
            Console.WriteLine($"\u001b[4m    {new string(' ',sutunlar.Length)}\u001b[0m");
            Console.WriteLine($"\u001b[4m|#| {sutunlar}|\u001b[0m");

            for (int i = 0; i < tablo.GetLength(0); i++)
            {  
                Console.Write($"\u001b[4m|{i}|\u001b[0m");
                for (int j = 0; j < tablo.GetLength(1); j++)
                {   
              
                    if (acilanAlanlar[i, j] && !MayinMi(i, j))
                        Console.Write($"\u001b[4m|{tablo[i, j]}\u001b[0m");
                    else if (patladiMi && MayinMi(i, j))

                        Console.Write($"\u001b[4m|{tablo[i, j]}\u001b[0m");
                    else
                        Console.Write($"\u001b[4m|■\u001b[0m");
                    if (j == tablo.GetLength(1) - 1)
                        Console.Write($"\u001b[4m|\u001b[0m");

                }
                Console.WriteLine();
            }
        }

        public void MayinYerlestir()
        {
            Random rastgele = new Random();
            int yeniSatir, yeniSutun;
            int mayinSayaci = 0;
            while (mayinSayaci < mayinSayisi)
            {
                yeniSatir = rastgele.Next(0, tablo.GetLength(0));
                yeniSutun = rastgele.Next(0, tablo.GetLength(1));

                if (!MayinMi(yeniSatir, yeniSutun))
                {
                    tablo[yeniSatir, yeniSutun] = '*';
                    mayinSayaci++;
                }
            }
        }
        private int CevredekiMayinSayisiniGetir(int satir, int sutun)
        {
            int sayac = 0;
            int yeniSatir, yeniSutun;
            if (!MayinMi(satir, sutun))
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        yeniSatir = satir + i;
                        yeniSutun = sutun + j;
                        if (yeniSatir >= 0 && yeniSatir < tablo.GetLength(0) && yeniSutun >= 0 && yeniSutun < tablo.GetLength(1))
                        {
                            if (MayinMi(yeniSatir, yeniSutun))
                            {
                                sayac++;
                            }
                        }
                    }
                }
            }
            return sayac;
        }
        public void HucreAc(int satir, int sutun)
        {
            if (acilanAlanlar[satir, sutun]) return;
            acilanAlanlar[satir, sutun] = true;
            if (MayinMi(satir, sutun))
            {
                patladiMi = true;
                return;
            }


            int mayinSayisi = CevredekiMayinSayisiniGetir(satir, sutun);
            if (mayinSayisi == 0)
            {
                tablo[satir, sutun] = ' ';
                int yeniSatir, yeniSutun;
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        yeniSatir = i + satir;
                        yeniSutun = j + sutun;
                        if (yeniSatir >= 0 && yeniSatir < tablo.GetLength(0) && yeniSutun >= 0 && yeniSutun < tablo.GetLength(1))
                            HucreAc(yeniSatir, yeniSutun);
                    }
            }
            else
                tablo[satir, sutun] = (char)(mayinSayisi + '0');
        }

        public bool MayinMi(int satir, int sutun)
        {
            if (tablo[satir, sutun] == '*')
                return true;
            return false;
        }


        public bool TumGuvenliAlanlarAcildiMi()
        {
            for (int i = 0; i < tablo.GetLength(0); i++)
                for (int j = 0; j < tablo.GetLength(1); j++)
                    if (!MayinMi(i, j) && !acilanAlanlar[i, j]) return false;
            return true;
        }
    }
}
