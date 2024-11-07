using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HaoticniKupidon
{
    [ServiceContract]
    public interface IRegistration
    {
        [OperationContract]
        bool InitSinglePerson(string name, string city, int age, string phoneNumber);




    }
}

