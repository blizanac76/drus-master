using System;
using System.Numerics;
using System.ServiceModel;
using CupidService;


public class CupidClient : ICupidCallback
{
    private ICupidService serviceProxy;

    public void Run()
    {
        var context = new InstanceContext(this);
        var factory = new DuplexChannelFactory<ICupidService>(context, "CupidServiceEndpoint");

        serviceProxy = factory.CreateChannel();

        RegisterUser();
    }

    public void RegisterUser()
    {
        Console.Write("Enter your name: ");
        var name = Console.ReadLine();
        Console.Write("Enter your city: ");
        var city = Console.ReadLine();

        int age;
        do
        {
            Console.Write("Enter your age: ");
        } while (!int.TryParse(Console.ReadLine(), out age) || age < 0);

        Console.Write("Enter your phone number: ");
        var phoneNumber = Console.ReadLine();

        var loverboy = new Loverboy { Name = name, City = city, Age = age, PhoneNumber = phoneNumber };
        serviceProxy.InitSinglePerson(loverboy);
    }

    public void ReceiveLoveLetter(Loverboy sender, string message)
    {
        Console.WriteLine($"Love Letter from {sender.Name} in {sender.City}: {message}");

        if (message != "I'm not interested in meeting")
        {
            Console.WriteLine($"Sender's phone number: {sender.PhoneNumber}");
        }

        Console.WriteLine("Press any key to confirm receipt...");
        Console.ReadKey();
        serviceProxy.ConfirmLetterReceived();
    }
}