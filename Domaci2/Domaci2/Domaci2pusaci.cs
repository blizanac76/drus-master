using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      
        internal static  SemaphoreSlim agentSemaphore = new SemaphoreSlim(1,1); 
        internal static  SemaphoreSlim[] smokerSemaphores = { new SemaphoreSlim(0), new SemaphoreSlim(0), new SemaphoreSlim(0) };

        internal enum DeoCigare { Papir, Duvan, Sibica }
        internal static  string[] DeoCigareString = { "Papir", "Duvan", "Sibica" };

        internal static int likSaPapirom = 0;
        internal static int likSaDuvanom = 1;
        internal static int likSaSibicom = 2;

        static async Task Main()
        {
            Task agentTask = Task.Run(() => Agent());
            Task[] pusacTask = {
            Task.Run(() => Pusac(likSaPapirom, "Papir")),
            Task.Run(() => Pusac(likSaDuvanom, "Duvan")),
            Task.Run(() => Pusac(likSaSibicom, "Sibica"))
        };
            //zapravo beskonacan await jer agent i pusaci rade u while true
            await Task.WhenAll(new[] { agentTask }.Concat(pusacTask).ToArray());

        }

        internal static async Task Agent()
        {
            Random random = new Random();
            while (true)
            {
                await agentSemaphore.WaitAsync();

                
                DeoCigare prviDeo = (DeoCigare)random.Next(0, 3);
                DeoCigare drugiDeo;
                do
                {
                    drugiDeo = (DeoCigare)random.Next(0, 3);
                } while (drugiDeo == prviDeo);

                Console.WriteLine($"agent stavlja {DeoCigareString[(int)prviDeo]} i {DeoCigareString[(int)drugiDeo]} na sto");

                
                if ((prviDeo == DeoCigare.Papir && drugiDeo == DeoCigare.Duvan) || (prviDeo == DeoCigare.Duvan && drugiDeo == DeoCigare.Papir))
                {
                    smokerSemaphores[likSaSibicom].Release(); 
                }
                else if ((prviDeo == DeoCigare.Papir && drugiDeo == DeoCigare.Sibica) || (prviDeo == DeoCigare.Sibica && drugiDeo == DeoCigare.Papir))
                {
                    smokerSemaphores[likSaDuvanom].Release(); 
                }
                else if ((prviDeo == DeoCigare.Duvan && drugiDeo == DeoCigare.Sibica) || (prviDeo == DeoCigare.Sibica && drugiDeo == DeoCigare.Duvan))
                {
                    smokerSemaphores[likSaPapirom].Release(); 
                }
            }
        }

        internal static async Task Pusac(int smokerIndex, string itemName)
        {
            while (true)
            {
                await smokerSemaphores[smokerIndex].WaitAsync(); // cekanje agenta da stavi na sto delove

                // Smoker takes the items, rolls a cigarette, smokes it, and informs agent
                Console.WriteLine($"pusac koji ima {itemName} uzima delove sa stola, pravi cigaretu i pusi");
                await Task.Delay(1000); // Simulate smoking time

                Console.WriteLine($"pusac koji ima {itemName} je gotov i odlazi");
                agentSemaphore.Release(); // Signal agent to place new items
            }
        }

    }
}
