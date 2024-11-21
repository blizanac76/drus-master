using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> brojevi = new List<int>();
            //Problem kod LCG-a je sto upadne u loop ponavljanja brojeva ukoliko parametri nisu dovoljno veliki.
            //Cak i kad su dovoljno veliki, pored toga sto ce zahtevati mnogo vise vremena za pokretanje,
            //brojevi ce se i dalje ponavljati, iako ce sekvenca ciklusa biti veoma velika
            // za ovaj primer dole dobijaju se ovi brojevi:
            //8 43 53 103 23 118 98 163 158 133 iznova i iznova

            //int xn = 1;   
            //int a = 5;       
            //int c = 3;       
            //int m = 165;
            //

            int xn = 1;
            int a = 1664525;   
            int c = 1013904223; 
            int m = (int)Math.Pow(2, 31); 

            int broj = LCG(a, xn, c, m);
            brojevi.Add(broj);
            Console.WriteLine($"Generisanje nasumicnih brojeva sa parametrima a={a}, c={c}, m={m}");
            Console.WriteLine($"{broj}");
            int i = 0;
            while (i<200000)
            {
                broj = LCG(a, broj, c, m);
                brojevi.Add(broj);
               // Console.WriteLine($" {broj}");
                i++;
                //Thread.Sleep(100);
            }

            File.WriteAllLines("random_brojevi.txt", brojevi.ConvertAll(x => x.ToString()));

            Console.WriteLine("sacuvavanje proslo");
        }

        public static int LCG(int a, int xn, int c, int m)
        {
            //formula za lgc
            int x2 = (a * xn + c) % m;
            //za ogromne parametre se dobijaju i negativne vrednosti jer izlazi iz opsega 32bita
            //ako je broj negativan dodam m
            return x2 < 0 ? x2 + m : x2;
        }
    }
}
