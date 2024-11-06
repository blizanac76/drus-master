using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CupidService
{
    internal interface ICupidService
    {
        [OperationContract]
        void InitSinglePerson(Loverboy loverboy);

        [OperationContract]
        void ConfirmLetterReceived();

    }
}
