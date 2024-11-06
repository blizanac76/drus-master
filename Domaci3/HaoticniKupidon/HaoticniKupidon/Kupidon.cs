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
        public System.Timers.Timer _timer;
        
        private void SetTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += nesto;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        
    }
}