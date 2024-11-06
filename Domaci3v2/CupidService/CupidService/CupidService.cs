using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CupidService
{
    public class CupidService : ICupidService
    {
        private readonly Dictionary<Loverboy, ICupidCallback> registeredPlayers = new();
        private readonly List<string> messages = new List<string>
    {
        "I’d like to get to know you",
        "I’m not interested in meeting",
        "I’m looking forward to our meeting!"
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
                Console.WriteLine($"Player {loverboy.Name} is already registered.");
            }
            else
            {
                registeredPlayers.Add(loverboy, callback);
                Console.WriteLine($"{loverboy.Name} from {loverboy.City} has registered.");
            }
        }

        private void SendLoveLetter(object state)
        {
            foreach (var receiver in registeredPlayers.Keys.ToList())
            {
                // Prevent sending letter to themselves
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
            Console.WriteLine("Letter has been confirmed as received.");
            // Additional logic can be implemented here to handle letter acknowledgment if needed.
        }
    }
}
