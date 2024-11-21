using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

using System.Threading;

namespace ConsoleApp1
{
    public class Original_RNG
    {
        public static void Main(string[] args)
        {
            //Test();
            string lok = "random_brojevi3.txt";
            if (File.Exists(lok))
            {
                File.Delete(lok);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            using (StreamWriter qwerty = new StreamWriter(lok))
            {
                for (int i = 0; i < 20000; i++)
                {
                    int randombroj = getXOR()+2;
                    qwerty.WriteLine(randombroj);
                }
            }
            stopwatch.Stop();
            Console.WriteLine("brojevi su u fajlu random_brojevi.txt");
            Console.WriteLine($"Proteklo vreme: {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadLine();

            //int xorBroj = getXOR();
            //Console.WriteLine($"(Jednocifren) Generisan broj:   {xorBroj}");


            //for (int i = 0; i < 20; i++)
            //{
            //    xorBroj = getXOR();
            //    Console.WriteLine($"Generisan broj {i + 1}.:     {xorBroj}");
            //}
            //Console.ReadLine();


        }

        public static void Test()
        {
            while (true)
            {
                long vreme = DateTime.Now.Ticks;
                int x0 = (int)(vreme % int.MaxValue);
                Console.WriteLine($"vreme: {vreme} pocetni broj: {x0}");

                // novi modul za deljenje je zasnovan na milisekundama
                int novi_mod = (int)(DateTime.Now.Millisecond * 1000); //*1000 jer mali modul ne valja
                Console.WriteLine($"novi modul: {novi_mod}");
                // broj procesa na racunaru
                int broj_procesa = Process.GetProcesses().Length;
                Console.WriteLine($"Broj procesa: {broj_procesa}");
                Thread.Sleep(2000);
            }

        }

        public static int getZadnjaCifraVreme()
        {
            long vreme = DateTime.Now.Ticks;
            int x = (int)(vreme % int.MaxValue);
            int broj = x % 10; 
            //Console.WriteLine($"Broj je generisan");
            //Console.ReadLine();
            return broj;
        }

        public static int getCifraProcesi()
        {
            int x = Process.GetProcesses().Length;
            int prvaCifra = (x / 10) % 10;
            int drugaCifra = x % 10;
            int baza = prvaCifra + getZadnjaCifraVreme();
            int stepen = (int)Math.Pow(baza, drugaCifra);
            int zadnjacifra = stepen % 10;
            return zadnjacifra;
            //Console.WriteLine($"Broj procesa: {zadnjacifra}");

        }
        public static int getXOR()
        {
            int broj1 = getZadnjaCifraVreme();
            int broj2 = getCifraProcesi();

            
            int xorBroj = broj1 ^ broj2;           
            int proizvod = (broj1 * broj2) % 10; 
            int kombinacija;

            // kombinacije
            if (broj1 % 2 == 0)
            {
                kombinacija = (broj1 + xorBroj + proizvod) % 10;
            }
            else
            {
                kombinacija = Math.Abs((broj1 - xorBroj + proizvod) % 10);
            }

            // ako je broj negativan vrati negativ od toga, a ako nije daj taj pozitivan 
            return kombinacija < 0 ? -kombinacija : kombinacija;
        }
    }
}
