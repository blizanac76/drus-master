using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace CupidService
{
    [ServiceContract]
    //Callback servisa da je primio pismo Loverboya
    public interface ICupidCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveLoveLetter(Loverboy sender, string message);
    }
}
