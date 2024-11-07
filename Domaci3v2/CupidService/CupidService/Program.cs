using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CupidService
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost s = new ServiceHost(typeof(CupidService));
            s.Open();
            Console.WriteLine("Press ENTER to end service");
            Console.ReadLine();
            s.Close();
        }
    }

}
