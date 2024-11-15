using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PubSubApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PubSub" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PubSub.svc or PubSub.svc.cs at the Solution Explorer and start debugging.
    
    public class PubSub : IPub, ISub
    {
        //
        delegate void MessageDeleagte(string message);
        static event MessageDeleagte MessageArrivedEvent;
        //delegat za metodu sa string i korisnik paramterima
        delegate void MessageDeleagte2(string message, Korisnik k);
        static event MessageDeleagte2 MessageArrivedEvent2;

        Random rng = new Random();

        public static Dictionary<string, string> Users = new Dictionary<string, string>();
        //niz korisnika
        public static Dictionary<int, Korisnik> Korisnici = new Dictionary<int, Korisnik>();
        public void InitSub(string username, string password)
        {
            if(!Users.ContainsKey(username))
            {
                Users.Add(username, password);
                MessageArrivedEvent += OperationContext.Current.GetCallbackChannel<ICallback>().MessageArrived;
            }
        }
        //
        public void InitSinglePerson(string name, string surname, string cuty, string phone, int age)
        {
            //dodavanje novog korisnika
            //generise se random id neki za listu
            //sluzi da se ne salje istom korisniku poruka od samog sebe
            //moze da bude ispravno za mali borj korisnika
           
            Console.WriteLine("InitSinglePerson()");
            int rand1 = rng.Next(10000);
            Korisnik k = new Korisnik
            {
                name = name,
                surname = surname,
                phone = phone,
                age = age,
                city = cuty
            };
            //Korisnik k = new Korisnik();
            //k.name = name;
            //k.surname = surname;
            //k.phone = phone;
            //k.age = age;
            //k.city = cuty;
            if (!Korisnici.ContainsKey(rand1))
            {
                Console.WriteLine("Korisnik se dodaje u listu");
                Korisnici.Add(rand1, k);
                Console.WriteLine("Korisnik added to list.");

                // Registracija callbacka ako vec nije registrovan
                if (MessageArrivedEvent2 == null ||
                    !MessageArrivedEvent2.GetInvocationList().Any(d => d.Target == OperationContext.Current.GetCallbackChannel<ICallback>()))
                {
                    Console.WriteLine("Dodavanje callbacka na MessageArrivedEvent2");
                    MessageArrivedEvent2 += OperationContext.Current.GetCallbackChannel<ICallback>().MessageArrived2;
                    Console.WriteLine("Callback registovan za novog suba");
                }
            }

            //Console.WriteLine("Korisnik se dodaje u listu");
            //Korisnici.Add(rand1, k);
            //Console.WriteLine("Korisnik je dodat u listu ide MessageArrivEvent2()");
            //MessageArrivedEvent2 += OperationContext.Current.GetCallbackChannel<ICallback>().MessageArrived2;
        }
        public void SendMessage(string message)
        {
            //string[] poruke =
            //{
            //        "Zelim da se vidimo",
            //        "Ne zelim da se vidimo",
            //        "Radujem se nasem susretu"
            //};
            //Random rand = new Random();
            //string poruka = poruke[rand.Next(poruke.Length)];

            MessageArrivedEvent?.Invoke($"Poruka: {message} arrived at {DateTime.Now}.");
        }
        //slanje poruke korisniku, poruka moze biti jedna od ove 3
        public void SendMessage2(string message, Korisnik k)
        {
            string[] poruke =
            {
                "Zelim da se vidimo",
                "Ne zelim da se vidimo",
                "Radujem se nasem susretu"
            };
            Random rand = new Random();
            string poruka = poruke[rand.Next(poruke.Length)];

            // invoke ovog eventa stringom i korisnikom
            MessageArrivedEvent2?.Invoke($"Poruka: {poruka}", k);
        }

        private string lastUserPhone = ""; // pracenje zadnjeg korisnika
        public Korisnik GetRandomUser(string currentUserPhone)
        {

            Console.WriteLine("Bira se nasumicni korisnik...");
            //pokusaj da eliminisem da se istom korisniku salje svoj podatak
            var otherUsers = Korisnici.Where(k => k.Value.phone != currentUserPhone).ToList();
            otherUsers = otherUsers.Where(k => k.Value.phone != lastUserPhone).ToList();

            if (Korisnici.Count > 0)
            {
                var randomIndex = new Random().Next(otherUsers.Count);
                var randomUser = otherUsers[randomIndex].Value;
                lastUserPhone = randomUser.phone;

                Console.WriteLine($"Korisnik: {randomUser.name} {randomUser.surname} iz {randomUser.city}");

                return randomUser;
            }

            
            return null;
        }
    }
}
