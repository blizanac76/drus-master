using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


//Servisna aplikacijaima metodu za Inicijalizaciju osobe i metodu za obavestavanje prijema pisma.
//Ugovor
namespace CupidService
{
    [ServiceContract]
    public interface ICupidService
    {
        [OperationContract]
        void InitSinglePerson(Loverboy loverboy);

        [OperationContract]
        void ConfirmLetterReceived();

    }
}
