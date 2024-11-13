using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kupidon
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Kupidon" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Kupidon.svc or Kupidon.svc.cs at the Solution Explorer and start debugging.
    public class Kupidon : IPub, ISub
    {
        public delegate void OnNotifiedDelegate(string poruka, List<Korisnik> korisnici);
        public static event OnNotifiedDelegate OnNotifiedEvent;

        static List<Korisnik> korisnici = new List<Korisnik>();

        public void sendLetters(string poruka)
        {
            if (korisnici.Count > 1) // Ensure there are at least two users
            {
                Random rand = new Random();
                foreach (var receiver in korisnici)
                {
                    Korisnik sender;
                    do
                    {
                        sender = korisnici[rand.Next(korisnici.Count)];
                    } while (sender == receiver); // Exclude self-sending

                    string message = $"{sender.Ime} {sender.Prezime} iz {sender.Grad} says: {poruka}";
                    List<Korisnik> receiverList = new List<Korisnik> { receiver }; // Notify only the current receiver
                    CallbackUsers(message, receiverList);
                }
            }
        }

        public void initSinglePerson(Korisnik korisnik)
        {
            ICallback callback = OperationContext.Current.GetCallbackChannel<ICallback>();
            korisnici.Add(korisnik);
            OnNotifiedEvent += callback.OnNotified;
        }

        static void CallbackUsers(string poruka, List<Korisnik> korisnici)
        {
            OnNotifiedEvent?.Invoke(poruka, korisnici);
        }
    }
}
