using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFLib
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
  [ServiceContract(Namespace = "Cupid", SessionMode = SessionMode.Required, CallbackContract =typeof(ICupidDuplexCallback))]
  public interface ICupid
  {
    [OperationContract(IsOneWay =true)]
    void GetData(int value);
    [OperationContract(IsOneWay =true)]
    void InitSinglePerson(User user);
    
    [OperationContract(IsOneWay = true)]
    void LetterResponse(int value, string responseTo);

    //[OperationContract]
    //CompositeType GetDataUsingDataContract(CompositeType composite);

    // TODO: Add your service operations here
  }

  // Use a data contract as illustrated in the sample below to add composite types to service operations.
  // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCFLib.ContractType".
  
}
