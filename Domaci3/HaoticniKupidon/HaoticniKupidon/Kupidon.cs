using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Timers;

namespace HaoticniKupidon
{
    
    public class Kupidon
    {
        public static System.Timers.Timer _timer;
        
        internal static void SetTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += Nesto;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        internal static void Nesto(Object source, ElapsedEventArgs e)
        {
            //nesto sto se radi na svaki tajmer otkucaj
            //kupidon salje pisma na svaki tajmer otkucaj
        }

        
    }
}