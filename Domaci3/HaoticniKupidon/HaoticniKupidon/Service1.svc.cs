using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HaoticniKupidon
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IRegistration, ILetterExchange
    {
        public List<User> Users = new List<User>();
        public static Random random = new Random();
        private static Timer letterTimer;

        public Service1()
        {
            // Initialize a timer to send letters every minute (60000 milliseconds)
            letterTimer = new Timer(SendLettersToPlayers, null, TimeSpan.Zero, TimeSpan.FromSeconds(60000));
        }

        public bool InitSinglePerson(string name, string city, int age, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(city) || age <= 17 || string.IsNullOrWhiteSpace(phoneNumber))
            {
                Console.WriteLine("Ime mora biti sacinjeno od karaktera, isto vazi i za grad. Morate biti stariji od 18 godina. Telefon mora biti validan");
                return false;
            }

            User korisnik = new User(name, city, age, phoneNumber);
            Users.Add(korisnik);
            return true;
        }

        private void SendLettersToPlayers(object state)
        {
            foreach (var player in Users)
            {
                // Only send a letter if the player isn’t waiting for confirmation
                if (!player.WaitingForConfirmation)
                {
                    SendRandomLetter(player);
                }
            }
        }
        public void RegisterForLetters(string usersName)
        {
            // Start background task to send letters every minute
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var user in Users)
                    {
                        if (!user.WaitingForConfirmation)
                        {
                            SendRandomLetter(user);
                        }
                    }
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }
            });
        }
        private void SendRandomLetter(User recipient)
        {
            // Get other players as potential senders, excluding the recipient and those waiting for confirmation
            var potentialSenders = Users
                .Where(p => p.Name != recipient.Name && !p.WaitingForConfirmation)
                .ToList();

            if (potentialSenders.Count == 0) return;

            var sender = potentialSenders[random.Next(potentialSenders.Count)];
            var message = GetRandomMessage();

            recipient.WaitingForConfirmation = true;

            
            var callback = OperationContext.Current.GetCallbackChannel<ICallback>();
            callback.ReceiveLetter(sender.Name, message, message != "Ne zelim da se upoznamo" ? sender.PhoneNumber : null);
        }
        private string GetRandomMessage()
        {
            var messages = new[] {
            "Zelim da se upoznamo",
            "Ne zelim da se upoznamo",
            "Iscekujem nas susret!"
            };
            return messages[random.Next(messages.Length)];
        }
        public void ConfirmLetterReceived(string userName)
        {
            var user = Users.FirstOrDefault(p => p.Name == userName);
            if (user != null)
            {
                user.WaitingForConfirmation = false;
            }
        }

    }
    public class User
    {
        public string Name { get; }
        public string City { get; }
        public int Age { get; }
        public string PhoneNumber { get; }
        public bool WaitingForConfirmation { get; set; }

        public User(string name, string city, int age, string phoneNumber)
        {
            Name = name;
            City = city;
            Age = age;
            PhoneNumber = phoneNumber;
            WaitingForConfirmation = false;
        }
    }

}
