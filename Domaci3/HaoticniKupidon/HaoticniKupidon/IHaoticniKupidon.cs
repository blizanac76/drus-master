using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace HaoticniKupidon
{
    [ServiceContract]
    public interface IHaoticniKupidon
    {
        [OperationContract]
        void SlanjePisma();

        [OperationContract]
        void initSinglePerson();

    }
}
