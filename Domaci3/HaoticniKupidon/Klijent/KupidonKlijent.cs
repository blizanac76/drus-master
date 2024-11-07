using HaoticniKupidon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    public class KupidonKlijent
    {
        public void PrijavaKorisnika()
        {
            Console.Write("Unesite vase ime: ");
            var ime = Console.ReadLine();

            Console.Write("Unesite vas grad: ");
            var grad = Console.ReadLine();

            int godine;
            do
            {
                Console.Write("Unesite vase godine (pozitivan celobrojni broj): ");
            } while (!int.TryParse(Console.ReadLine(), out godine) || godine < 0);

            Console.Write("unesite broj telefona: ");
            var brojtelefona = Console.ReadLine();

            var loverboy = new Loverboy { Ime = ime, Grad = grad, Godine = godine, brTelefona = brojtelefona };
            //initSinglePerson(loverboy);
        }

        public void PrimiPismo(Loverboy posiljalac, string poruka)
        {
            Console.WriteLine($"Ljubavno pismo od {posiljalac.Ime} iz {posiljalac.Grad}: {poruka}");

            if (poruka != "Ne zelim da se upoznamo")
            {
                Console.WriteLine($"Broj telefona posaljioca: {posiljalac.brTelefona}");
            }

            Console.WriteLine("Primi posiljku...");
            Console.ReadKey();
            // metodaZaFeedBack o prijemu pisma
        }

    }
}
