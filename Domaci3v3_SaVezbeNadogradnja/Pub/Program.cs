using Pub.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Pub
{
    internal class Program
    {
        static ServiceReference1.PubClient pubClient = new ServiceReference1.PubClient();
        static void Main(string[] args)
        {
            

            while (true)
            {
                //za test samo
                var currentUser = "mladen";

                // random korisnik sa servera
                var randomUser = pubClient.GetRandomUser(currentUser);
                if (randomUser != null)
                {
                    Console.WriteLine($"Ide poruka od: {randomUser.name} {randomUser.surname}");

                    // salje se poruka od tog korisnika
                    pubClient.SendMessage2("poruka", randomUser);
                }
                else
                {
                    Console.WriteLine("nema korisnika null.");
                }

                
                Thread.Sleep(5000);
            }
        }
    }
}
