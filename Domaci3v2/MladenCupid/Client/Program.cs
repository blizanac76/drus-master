using Client.ServiceReference1;
using WCFLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Threading;

namespace Client
{
  internal class Program
  {
    static void Main(string[] args)
    {
      bool test = true;
      //client.GetData(10);
      User u = null;

      if (test)
      {
        Random r = new Random();
        string id = r.Next(100000000, 900000000).ToString();
        u = new User();
        u.Ime = $"Mladen{id}";
        u.Grad = "Curug";
        u.BrTelefona = id;
        u.Godine = 18;
      }
      Console.WriteLine($"Ja sam ");
      CallBackHandler handler = new CallBackHandler();
      handler.Start(u);
      //Console.WriteLine("Unesite Ime: ");
      //string? ime = Console.ReadLine();
      //while (string.IsNullOrWhiteSpace(ime))
      //{
      //  Console.WriteLine("Unesite Ime: ");
      //  ime = Console.ReadLine();
      //}
      //ime = ime.Trim();

      //Console.WriteLine("Unesite Grad: ");
      //string? grad = Console.ReadLine();
      //while (string.IsNullOrWhiteSpace(grad))
      //{
      //  Console.WriteLine("Unesite Grad: ");
      //  grad = Console.ReadLine();
      //}
      //grad = grad.Trim();

      //Console.WriteLine("Unesite Godine: ");
      //string? godineS = Console.ReadLine();
      //bool validneGodine = UInt32.TryParse(godineS, out uint godine);
      //if (validneGodine && godine > 150)
      //  validneGodine = false;

      //while (!validneGodine)
      //{
      //  Console.WriteLine("Unesite Pravilne Godine: ");
      //  godineS = Console.ReadLine();
      //  validneGodine = UInt32.TryParse(godineS, out godine);
      //  if (validneGodine && godine > 150)
      //    validneGodine = false;
      //}

      //Console.WriteLine("Unesite Broj Telefona: ");
      //string? broj = Console.ReadLine();
      //while (string.IsNullOrWhiteSpace(broj))
      //{
      //  Console.WriteLine("Unesite Broj Telefona: ");
      //  broj = Console.ReadLine();
      //}

      //Console.WriteLine($"Ime: {ime}, Grad: {grad}, Godine: {godine}, BrTelefona: {broj}");
      Console.ReadKey();
    }

    public class CallBackHandler : ICupidCallback
    {
      private CupidClient client;
      public CallBackHandler()
      {
        var instanceContext = new InstanceContext(this);
        client = new CupidClient(instanceContext);
      }

      public void Start(User u)
      {
        client.InitSinglePerson(u);
      }
      public void Announce(string name)
      {
        Console.WriteLine($"**************** {name} se pridruzio! **************");
      }

      public void GetLetter(User u)
      {
        ThreadPool.QueueUserWorkItem(_ =>
        {
          Console.WriteLine($"{u.Ime} {u.Godine} iz {u.Grad}. Broj telefona: {u.BrTelefona}");
          Console.WriteLine("Potvrdi prijem poruke:");
          Console.WriteLine("1. Zainteresovan/a sam! Potvrdjujem");
          Console.WriteLine("2. Potvrda!");
          string odgS = Console.ReadLine().Trim();
          while (odgS != "1" && odgS != "2")
          {
            Console.WriteLine("Nepravilan odgovor! Pokusajte ponovo!");
            Console.WriteLine("Odgovori:");
            Console.WriteLine("1. Zainteresovan/a sam! Potvrdjujem");
            Console.WriteLine("2. Potvrda!");
            odgS = Console.ReadLine().Trim();
          }

          client.LetterResponse(Convert.ToInt32(odgS), u.Ime);
        });
      }

      public void InitSinglePersonR(bool success)
      {
        State.loggedIn = success;
        string res = "";
        res = (success == true ? "Uspesna Registracija!" : res = "Neuspesna Registracija");
        Console.WriteLine(res);

      }

      public void PrintShit(string value)
      {
        Console.WriteLine(value);
      }
    }
  }
}
