using System.Net;
using ServiceReference1;

namespace PublisherOfKupidon1
{
    public class Program
    {
        static PubClient PubClient = new();

        public static void Main(string[] args)
        {



            Console.WriteLine("Kupidon salje pisma korisnicima");

            string[] poruke =
            {
                    "Zelim da se vidimo",
                    "Ne zelim da se vidimo",
                    "Radujem se nasem susretu"
            };
            Random rand = new Random();
            while (true)
            {
                string poruka = poruke[rand.Next(poruke.Length)];
                Console.WriteLine($"{poruka}");

                // Call the sendLetters method here
                PubClient.sendLetters(poruka);
                Thread.Sleep(60 * 1000); //kupidon sends every minute
            }
        }

    }
}
