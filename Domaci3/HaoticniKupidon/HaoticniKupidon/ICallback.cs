using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HaoticniKupidon
{
    internal interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveLetter(string senderName, string message, string senderPhone = null);
    }
}
