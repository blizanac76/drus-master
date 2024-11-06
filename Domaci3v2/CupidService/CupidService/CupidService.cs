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
        private readonly Dictionary<Loverboy, ICupidCallback> registeredPlayers = new Dictionary<Loverboy, ICupidCallback>();
        private readonly List<string> messages = new List<string>
    {
        "Zelim da se upoznamo",
        "Ne zelim da se upoznamo",
        "Radujem se nasem susretu!"
    };
        private readonly Timer loveLetterTimer;

        public CupidService()
        {
            // Start sending love letters every minute
            loveLetterTimer = new Timer(SendLoveLetter, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        public void InitSinglePerson(Loverboy loverboy)
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICupidCallback>();

            if (registeredPlayers.ContainsKey(loverboy))
            {
                Console.WriteLine($"Loverboy {loverboy.Name} je vec prijavljen.");
            }
            else
            {
                registeredPlayers.Add(loverboy, callback);
                Console.WriteLine($"{loverboy.Name} iz {loverboy.City} je registrovan.");
            }
        }

        private void SendLoveLetter(object state)
        {
            foreach (var receiver in registeredPlayers.Keys.ToList())
            {
                var eligibleSenders = registeredPlayers.Keys.Where(p => p != receiver).ToList();

                if (eligibleSenders.Count == 0) continue;

                var sender = eligibleSenders[new Random().Next(eligibleSenders.Count)];
                var message = messages[new Random().Next(messages.Count)];

                var callback = registeredPlayers[receiver];
                callback.ReceiveLoveLetter(sender, message);
            }
        }

        public void ConfirmLetterReceived()
        {
            Console.WriteLine("Pismo poslato");
           
        }
    }
}
