using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlijentFramework.ServiceReference1;
using System.ServiceModel;


namespace KlijentFramework
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


    public class Program
    {
        static KlijentFramework.ServiceReference1.SubClient ServiceReference1;
        public static Korisnik korisnik;
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new PlayerCallback());
            WSDualHttpBinding binding = new WSDualHttpBinding();
            EndpointAddress endpointAddress = new EndpointAddress("http://localhost:50927/Service.svc/sub");
            ServiceReference1 = new SubClient(context, binding, endpointAddress);


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
