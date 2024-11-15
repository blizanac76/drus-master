using Sub.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sub
{
    public class Callback : ServiceReference1.ISubCallback
    {
        public void MessageArrived(string message)
        {
            Console.WriteLine(message);
        }
        public void MessageArrived2(string message, ServiceReference1.Korisnik k)
        {
            Console.WriteLine("MessageArrived2()");
            // ovi cw su vise za pracenje koda kroz rad
            if (message.Contains("Ne zelim da se vidimo"))
            {
                Console.WriteLine($"{k.name} {k.surname} iz {k.city} kaze: {message}. Broj skriven.");
            }
            else
            {
                Console.WriteLine($"{k.name} {k.surname} iz {k.city} kaze: {message}. Kontakt: {k.phone}");
            }
        }
    }
    internal class Program
    {
        static ServiceReference1.SubClient subClient;
        static void Main(string[] args)
        {
            int x;
            Console.WriteLine("Unesi ime: ");
            string name = Console.ReadLine();
            Console.WriteLine("Unesi prezime: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Unesi grad: ");
            string city = Console.ReadLine();
            Console.WriteLine("Unesi broj telefona: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Unesi godine: ");
            string age = Console.ReadLine();
            if (!int.TryParse(age, out x))
            {
                Console.WriteLine("nije dobar broj");
            }
            else
            {
                //novi callback za korisnika preko kojeg se salju nazad informacije
                Console.WriteLine("Pravljenje novog Instance Context");
                InstanceContext ic = new InstanceContext(new Callback());
                Console.WriteLine("Pravljenje novog subclient");
                //novi subclient za ser referencu
                var subClient = new ServiceReference1.SubClient(ic);
                // subClient.InitSub()
                //unicijalizacija novog korisniak
                Console.WriteLine("Korisnikove informacije idu u initsingle person");
                subClient.InitSinglePerson(name, surname, city, phone, x);
                Console.ReadLine();
            }
        }
    }
}
