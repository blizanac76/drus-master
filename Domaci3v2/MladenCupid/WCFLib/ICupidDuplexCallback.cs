using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
//[Operationontract] = dostupno Klijentima
namespace WCFLib
{
  public interface ICupidDuplexCallback
  {
    //sluze za slanje poruka i feedback korisnicima (npr ljubavna pisma ili feedback za registracija)
    [OperationContract(IsOneWay = true)]
    void PrintShit(string value);

    // 0 fail , 1 success
    [OperationContract(IsOneWay = true)]
    void InitSinglePersonR(bool success);

    [OperationContract(IsOneWay = true)]
    void Announce(string name);

    [OperationContract(IsOneWay = true)]
    void GetLetter(User u);
  }
}
