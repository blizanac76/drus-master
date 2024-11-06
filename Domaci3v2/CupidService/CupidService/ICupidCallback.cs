using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

public interface ICupidCallback
{
    [OperationContract(IsOneWay = true)]
    void ReceiveLoveLetter(Loverboy sender, string message);
}
