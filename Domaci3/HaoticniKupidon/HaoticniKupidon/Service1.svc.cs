using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Timers;

namespace HaoticniKupidon
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public System.Timers.Timer _timer;

        [OperationContract]
        public void AddClient(string ime, string grad, int godine, string broj)
        {
            //todo
        }

        public void SendLetter(Object source, ElapsedEventArgs e)
        {
            //salje pisma klijentima koji zele da nadju partnera

        }

        private void SetTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += SendLetter;

        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
