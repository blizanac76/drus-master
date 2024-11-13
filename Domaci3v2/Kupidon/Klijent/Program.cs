using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceReference1;

namespace Klijent
{
    class PlayerCallback : ISubCallback
    {
        public void OnNotified(string poruka, Korisnik[] korisnici)
        {
            string[] poruke =
            {
                    "Zelim da se vidimo",
                    "Ne zelim da se vidimo",
                    "Radujem se nasem susretu"
                 };
            Random rand = new Random();
            poruka = poruke[rand.Next(poruke.Length)];

            if (!(poruka == "Ne zelim da se vidimo"))
            {
                for (int i = 0; i < korisnici.Length; i++)
                {
                    Console.WriteLine($"{korisnici[i].Ime} {korisnici[i].Prezime} iz {korisnici[i].Grad} starosti {korisnici[i].Godine}" +
                        $"zeli da se upoznate! Veli: {poruka} Moj broj je{korisnici[i].BrojTelefona}");

                }
            }
            else
            {
                Console.WriteLine("Kupidon trazi osobu koja je zainteresovana");
            }
            //sendLetter(poruka, korisnici);

        }
    }
    internal class Program
    {
        static SubClientBase? ServiceReference1;
        public static Korisnik? korisnik;
        static void Main(string[] args)
        {
            InstanceContext context = new(new PlayerCallback());
            WSDualHttpBinding binding = new();
            EndpointAddress endpointAddress = new("http://localhost:55637/Service.svc/sub");
            ServiceReference1 = new(context, binding, endpointAddress);
          

            initSinglePerson();
            Console.ReadLine();
        }




        static void initSinglePerson()
        {
            Console.WriteLine("Unesite Vase Ime");
            string Ime1 = Console.ReadLine();

            Console.WriteLine("Unesite Vase Prezime");
            string Prezime1 = Console.ReadLine();

            Console.WriteLine("Unesite Vase godine: ");
            int Godine1 = int.TryParse(Console.ReadLine(), out int godina) ? godina : 0;

            Console.WriteLine("Unesite broj telefona");
            string BrojTelefona1 = Console.ReadLine();

            Console.WriteLine("Unesite vas Grad odakle ste");
            string Grad1 = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(Ime1) || string.IsNullOrWhiteSpace(Prezime1) || Godine1 <= 0 || string.IsNullOrWhiteSpace(BrojTelefona1) || string.IsNullOrWhiteSpace(Grad1))
            {
                Console.WriteLine("All fields must be properly filled. Please try again.");
                return;
            }

            Korisnik korisnik = new Korisnik()
            {
                Ime = Ime1,
                Prezime = Prezime1,
                Godine = Godine1,
                BrojTelefona = BrojTelefona1,
                Grad = Grad1
            };

            ServiceReference1?.initSinglePerson(korisnik);
            Console.WriteLine("Successfully registered to the matchmaking service!");
        }
    }

}
