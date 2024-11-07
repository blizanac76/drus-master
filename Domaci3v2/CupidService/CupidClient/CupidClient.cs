using System;
using System.Numerics;
using System.ServiceModel;
using CupidService;


public class CupidClient : ICupidCallback
{
    private ICupidService? serviceProxy;

    public void Run()
    {
        var context = new InstanceContext(this);
        var factory = new DuplexChannelFactory<ICupidService>(context, "CupidServiceEndpoint");


        serviceProxy = factory.CreateChannel();

        RegisterUser();
    }

    public void RegisterUser()
    {
        Console.Write("Unesite vase ime: ");
        var name = Console.ReadLine();

        Console.Write("Unesite vas grad: ");
        var city = Console.ReadLine();

        int age;
        do
        {
            Console.Write("Unesite vase godine: ");
        }while (!int.TryParse(Console.ReadLine(), out age) || age < 0);

        Console.Write("unesite broj telefona: ");
        var number_phone = Console.ReadLine();

        var loverboy = new Loverboy { Name = name, City = city, Age = age, PhoneNumber = number_phone };
        serviceProxy.InitSinglePerson(loverboy);
    }

    public void ReceiveLoveLetter(Loverboy sender, string message)
    {
        Console.WriteLine($"Ljubavno pismo od {sender.Name} iz {sender.City}: {message}");

        if (message != "Zelim da se upoznamo")
        {
            Console.WriteLine($"Broj telefona posaljioca: {sender.PhoneNumber}");
        }

        Console.WriteLine("Primi posiljku...");
        Console.ReadKey();
        serviceProxy.ConfirmLetterReceived();
    }
}