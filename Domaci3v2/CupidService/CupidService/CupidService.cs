using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
//using System.Timers;
using System.Threading;


namespace CupidService
{
    public class CupidService : ICupidService
    {
        //Korisnici su smesteni u recnik
        private  Dictionary<Loverboy, ICupidCallback> registeredLoverboys = new Dictionary<Loverboy, ICupidCallback>();
        //Poruke koje Loverboy ispisuje drugom Loverboyu
        private  List<string> messages = new List<string>
    {
        "Zelim da se upoznamo",
        "Ne zelim da se upoznamo",
        "Radujem se nasem susretu!"
    };
        //timer kojim se Ljubavnopismo svaki sekund salje
        public Timer loveLetterTimer;

        public CupidService()
        {
            // konstruktor
            loveLetterTimer = new Timer(SendLoveLetter, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        public void InitSinglePerson(Loverboy loverboy)
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICupidCallback>();

            if (registeredLoverboys.ContainsKey(loverboy))
            {
                Console.WriteLine($"Loverboy {loverboy.Name} je vec prijavljen.");
            }
            else
            {
                registeredLoverboys.Add(loverboy, callback);
                Console.WriteLine($"{loverboy.Name} iz {loverboy.City} je registrovan.");
            }
        }

        private void SendLoveLetter(object state)
        {

            foreach (var receiver in registeredLoverboys.Keys.ToList())
            {
                //da ne salje samom sebi, pravi se nova lista bez tog loverboya
                var dostupniLoverboyi = registeredLoverboys.Keys.Where(p => p != receiver).ToList();

                if (dostupniLoverboyi.Count == 0) continue;

                var sender = dostupniLoverboyi[new Random().Next(dostupniLoverboyi.Count)];
                var message = messages[new Random().Next(messages.Count)];

                var callback = registeredLoverboys[receiver];
                callback.ReceiveLoveLetter(sender, message);
            }
        }

        public void ConfirmLetterReceived()
        {
            Console.WriteLine("Pismo poslato");
           
        }
    }
}
