using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HaoticniKupidon
{
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface ILetterExchange
    {
        [OperationContract(IsOneWay = true)]
        void RegisterForLetters(string playerName);

        [OperationContract(IsOneWay = true)]
        void ConfirmLetterReceived(string playerName);
    }
}
