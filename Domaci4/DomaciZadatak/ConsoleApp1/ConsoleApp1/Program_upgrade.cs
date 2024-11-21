using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;


namespace ConsoleApp1
{
    
    public class Program_upgrade
    {
        static void Main(string[] args)
        {
            List<int> brojevi = new List<int>();

            // koristi se trenutno vreme programa u kolisekundama
            long vreme = DateTime.Now.Ticks; 
            int x0 = (int)(vreme % int.MaxValue);

            // novi modul za deljenje je zasnovan na milisekundama
            int novi_mod = (int)(DateTime.Now.Millisecond * 1000); //*1000 jer mali modul ne valja
            // broj procesa na racunaru
            int broj_procesa = Process.GetProcesses().Length;

            int a = 1664525;
            //skaliranje da bude velik broj, meni stoji oko 250 procesa obicno 
            int c = broj_procesa*123456; 
            int m = novi_mod;

            
            int broj = LCG(a, x0, c, m);
            brojevi.Add(broj);
            Console.WriteLine($"Generisanje nasumicnih brojeva sa parametrima a={a}, c={c}, m={m}");
            Console.WriteLine($"{broj}");

            int i = 0;
            while (i < 20000)
            {
                broj = LCG(a, broj, c, m);
                brojevi.Add(broj);
                i++;
            }

            
            File.WriteAllLines("random_brojevi2.txt", brojevi.ConvertAll(x => x.ToString()));
            Console.WriteLine("write u fajl prosao.");
        }

        
        public static int LCG(int a, int xn, int c, int m)
        {
            // formula za lcg
            int x2 = (a * xn + c) % m;

           
            return x2 < 0 ? x2 + m : x2;
        }
    }
}
