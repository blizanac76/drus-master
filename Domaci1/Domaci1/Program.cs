using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    static Random random = new Random(); 

    // Random brojevi od 100 do 0
    static List<int> Generatorbrojeva(int size)
    {
        List<int> brojevi = new List<int>(size);
        for (int i = 0; i < size; i++)
        {

            brojevi.Add(random.Next(0, 100+1)); 

        }

        return brojevi;
    }

    // sinhron nacin
    static double Sinhronnacin(List<int> brojevi)
    {
        //int zbir = 0;  //ne staje ceo zbir lol
        long zbir = 0;
        foreach (var broj in brojevi)
        {
            zbir += broj;
        }
        //elementi = brojevi.Count;
        //return zbir / elementi
        //double jer nije ceo broj
        return (double)zbir / brojevi.Count;
    }

    // async metoda za racunanje. ovaj nacin ne blokira glavnu nit
    static async Task<double> Asinhronnacin(List<int> brojevi)
    {
        //int zbir = 0;  //ne staje ceo zbir lol
        long zbir = 0;

        // async- await
        await Task.Run(() =>
        {
            foreach (var broj in brojevi)
            {
                zbir += broj;
            }
        });
        //double jer nije ceo broj
        return (double)zbir / brojevi.Count;
    }

    // paralelni nacin uz pomoc vise niti
    static double Paralelannacin(List<int> brojevi, int brojtaskova)
    {
        //int zbir = 0;  //ne staje ceo zbir lol
        long ukupnasuma = 0;
        int velicinaDela = brojevi.Count / brojtaskova; 
        //zadatak se deli na jednake celine

        //lista taskova
        List<Task<long>> taskovi = new List<Task<long>>();

        // svaki deo se sumira zasebno, a ima ih onoliko koliko smo dali staskova na parametar funkciji
        for (int i = 0; i < brojtaskova; i++)
        {

            //kretanje kroz delove zahteva iteraciju kroz iste. start i kraj se krecu kroz te delove i markiraju gde je 
            // pocetak a gde kraj svakog dela
            int start = i * velicinaDela;
            int kraj;
            //kada dodjemo do zadnjeg dela kraj je poslednji element
            if (i == brojtaskova - 1)
            {
                kraj = brojevi.Count;
            }
            else
            {
                
                kraj = start + velicinaDela;
            }
            //dodavanje liste taskova. task/zadatak je da se izracuna suma tog malog dela
            taskovi.Add(Task.Run(() =>
            {
                //int ima opseg do 2.2 milijarde, ako podelimo na 2 taska moze otici iz opsega ppa je i ovde long
                long zbir = 0;
                //u tom fiksnom delu, definisanom startom i krajem nadji sumu
                for (int j = start; j < kraj; j++)
                {
                    zbir += brojevi[j];
                }
                return zbir;
            }));
        }

        // waitall ocekuje listu
        //ovo ceka da se svi taskovi izvrse i saberu svoje delove pre nego sto se izrvisi konacna suma
        Task.WaitAll(taskovi.ToArray());

        foreach (var task in taskovi)
        {
            ukupnasuma += task.Result;
        }
        //double jer nije ceo broj
        return (double)ukupnasuma / brojevi.Count;
    }

    static void Main(string[] args)
    {
        int kolicina_brojeva = 1000000000;
        Console.WriteLine("Initializing list...");
        var list = Generatorbrojeva(kolicina_brojeva);

        // pali se i grasi, resetuje stoperica pre i posle svakog nacina izvrsenja i gleda se vreme
        Stopwatch stopwatch = new Stopwatch();

        // 1 nit, sinhrono, serijski
        stopwatch.Start(); 
        double prosekSinh = Sinhronnacin(list);
        stopwatch.Stop();
        Console.WriteLine($"Prosecno za sinhrono izvrsavanje:: {prosekSinh}");
        Console.WriteLine($"vreme izvrsavanja sihnronog programa: {stopwatch.ElapsedMilliseconds} milisekundi");
        Console.WriteLine("\n\n");
        //asinhron nacin
        stopwatch.Reset(); 
        stopwatch.Start();

        //var jer:
        //Error(active)  CS0029 Cannot implicitly convert type 'System.Threading.Tasks.Task<double>' to 'double'
        //taskovi su lista double vrednosti
        var prosekAsinhTask = Asinhronnacin(list);
        double prosekAsinh = prosekAsinhTask.Result; // Await the async result
        stopwatch.Stop();
        Console.WriteLine($"prosek za asinhron nacin: {prosekAsinh}");
        Console.WriteLine($"vreme izvrsavanja asinhr. programa : {stopwatch.ElapsedMilliseconds} milisek.");
        Console.WriteLine("\n\n");
        //paralelni nacin sa 2 5 10 100 niti


        int[] niti = { 2, 5, 10, 100 };
        foreach (int KolikoNit in niti)
        {
            stopwatch.Reset();
            stopwatch.Start();
            double prosecnavrednostParalelno = Paralelannacin(list, KolikoNit);
            stopwatch.Stop();
            Console.WriteLine("\n\n");
            Console.WriteLine($"prosek za paralelno. Broj niti je ({KolikoNit} niti. prosecna vrednost: {prosecnavrednostParalelno} \n");
            Console.WriteLine($"vreme za paralelno broj niti: {KolikoNit} niti. Vreme: {stopwatch.ElapsedMilliseconds} milisek.");
        }
    }
}
//Initializing list...
//Prosecno za sinhrono izvrsavanje:: 50.000799693
//vreme izvrsavanja sihnronog programa: 10086 milisekundi



//prosek za asinhron nacin: 50.000799693
//vreme izvrsavanja asinhr. programa : 5704 milisek.






//prosek za paralelno. Broj niti je (2 niti. prosecna vrednost: 50.000799693

//vreme za paralelno broj niti: 2 niti.Vreme: 2729 milisek.



//prosek za paralelno. Broj niti je (5 niti. prosecna vrednost: 50.000799693

//vreme za paralelno broj niti: 5 niti.Vreme: 1420 milisek.



//prosek za paralelno. Broj niti je (10 niti. prosecna vrednost: 50.000799693

//vreme za paralelno broj niti: 10 niti.Vreme: 1448 milisek.



//prosek za paralelno. Broj niti je (100 niti. prosecna vrednost: 50.000799693

//vreme za paralelno broj niti: 100 niti.Vreme: 1171 milisek.

//M:\Fakultet\drus\D1\Domaci1\Domaci1\bin\Debug\net8.0\Domaci1.exe (process 13416) exited with code 0 (0x0).
//To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
//Press any key to close this window . . .


//Program se najsporije izvrsava na sinhron nacin. Ubedljivo najvise vremena treba. Asinhron nacin je dosta brzi dok 
//deljenje procesa na vise niti ubrzava proces medjutim, razlika izmedju 10 i 100 niti je dosta mala, pa dodavanje brda niti 
//nece ubrzati proces kao ste ce deljenje na 2 ili 5 niti.
//
//
