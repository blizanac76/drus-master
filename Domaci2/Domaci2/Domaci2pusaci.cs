using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//mladen Blizanac e2 87 / 2024
//• Cigarette smokers problem
//• Postoji jedan agent i tri nervozna pušača. Agent poseduje rezerve tri
//neophodna predmeta za lečenje nervoze: papir, duvan i šibice. Jedan
//od pušača ima beskonačne zalihe papira, drugi – duvana, a treći -
//šibica. Agent počinje tako što dva različita predmeta stavlja na sto,
//jedan po jedan. Pušač, kome baš ta dva predmeta fale, uzima ih,
//zavija i pali cigaretu i uživa. Nakon toga obaveštava agenta da je
//završio, a agent onda stavlja dva nova predmeta na sto, itd.

namespace Domaci2
{
    internal class Domaci2pusaci
    {
        //agent uzluzuje samo 1 pusaca pa je max vrednost semafora 1
        internal static  SemaphoreSlim agentSemaphore = new SemaphoreSlim(1,1);
        // pusac Semaphores - svaki pusac ima svoj semafor kako bi upravljali redosledom rada - niz semaphore
        internal static  SemaphoreSlim[] pusaciSemaphores = { new SemaphoreSlim(0), new SemaphoreSlim(0), new SemaphoreSlim(0) };

        internal enum DeoCigare { Papir, Duvan, Sibica }
        internal static  string[] DeoCigareString = { "Papir", "Duvan", "Sibica" };

        internal static int likSaPapirom = 0;
        internal static int likSaDuvanom = 1;
        internal static int likSaSibicom = 2;

        static async Task Main()
        {
            //// startuje agentovu nit koja koristi funkciju Agent
            Task agentTask = Task.Run(() => Agent());
            //// stattuje niti za svakog pusaca sa razlicitim delovima cigare koje imaju
            Task[] pusacTask = {
            Task.Run(() => Pusac(likSaPapirom, "Papir")),
            Task.Run(() => Pusac(likSaDuvanom, "Duvan")),
            Task.Run(() => Pusac(likSaSibicom, "Sibica"))
        };
            //zapravo beskonacan await jer agent i pusaci rade u while true
            //kreira task tek kada se svi dati taskovi zavrse. Posto agent vrti u while(true) ovo beskonacno ceka
            //await ceka asinhroni zadatak da se zavrsi, bez blokiranja ,main niti;
            await Task.WhenAll(new[] { agentTask }.Concat(pusacTask).ToArray());

        }
        //agent je zapravo 'glavni' semafor
        internal static async Task Agent()
        {
            Random random = new Random();
            while (true)
            {
                await agentSemaphore.WaitAsync();

                //agent random stavlja 2 dela cigare, prvo random prvi deo. Drugi deo ne sme da bude isti kao prvi.
                DeoCigare prviDeo = (DeoCigare)random.Next(0, 3);
                //DeoCigare drugiDeo = (DeoCigare)random.Next(1, 3);
                DeoCigare drugiDeo;
                do
                {
                    drugiDeo = (DeoCigare)random.Next(0, 3);
                } while (drugiDeo == prviDeo);

                Console.WriteLine($"agent stavlja {DeoCigareString[(int)prviDeo]} i {DeoCigareString[(int)drugiDeo]} na sto");

                //u zavisnosti sta kojem agentu treba, taskovi se obavljaju na osnovu toga
                //switch je bolje resenje mozda
                if ((prviDeo == DeoCigare.Papir && drugiDeo == DeoCigare.Duvan) || (prviDeo == DeoCigare.Duvan && drugiDeo == DeoCigare.Papir))
                {
                    pusaciSemaphores[likSaSibicom].Release(); //release je inkrement semafora. otvara mesto u semaforu za pusaca kojem fale ova 2 dela
                }
                else if ((prviDeo == DeoCigare.Papir && drugiDeo == DeoCigare.Sibica) || (prviDeo == DeoCigare.Sibica && drugiDeo == DeoCigare.Papir))
                {
                    pusaciSemaphores[likSaDuvanom].Release(); 
                }
                else if ((prviDeo == DeoCigare.Duvan && drugiDeo == DeoCigare.Sibica) || (prviDeo == DeoCigare.Sibica && drugiDeo == DeoCigare.Duvan))
                {
                    pusaciSemaphores[likSaPapirom].Release(); 
                }
            }
        }

        internal static async Task Pusac(int pusacId, string pusacDeoCigare)
        {
            //pusaci cekaju da dobiju delove
            while (true)
            {
                await pusaciSemaphores[pusacId].WaitAsync(); // cekanje agenta da stavi na sto delove

                
                Console.WriteLine($"pusac koji ima {pusacDeoCigare} uzima delove sa stola, pravi cigaretu i pusi");
                await Task.Delay(5000); 

                Console.WriteLine($"pusac koji ima {pusacDeoCigare} je gotov i odlazi");
                agentSemaphore.Release(); //semafor ++
            }
        }

    }
}
