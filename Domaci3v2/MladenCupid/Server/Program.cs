using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Timers;
using WCFLib;
namespace Klijenrt
{
  internal class Program
  {
    private static Timer _timer;
    static void Main(string[] args)
    {
      // See https://aka.ms/new-console-template for more information
      Uri baseAddress = new Uri("http://localhost:8005/WCFLib/");

      ServiceHost selfHost = new ServiceHost(typeof(Cupid), baseAddress);

      selfHost.AddServiceEndpoint(typeof(ICupid), new WSDualHttpBinding(), "Cupid");

      ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
      smb.HttpGetEnabled = true;
      selfHost.Description.Behaviors.Add(smb);
      selfHost.Open();
     



      Console.WriteLine("The service is ready.");

      // Close the ServiceHost to stop the service.
      Console.WriteLine("Press <Enter> to terminate the service.");
      Console.WriteLine();
      Console.ReadLine();
      selfHost.Close();
    }

    static void SetTimer()
    {
      

    }

    static void ElapsedTimer(object obj, ElapsedEventArgs e)
    {
      
    }

  }

  
}
