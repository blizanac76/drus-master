using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PubSubApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPubSub" in both code and config file together.
    [ServiceContract]
    public interface IPub
    {
        [OperationContract(IsOneWay =true)]
        void SendMessage(string message);
        [OperationContract(IsOneWay = true)]
        void SendMessage2(string message, Korisnik k);

        [OperationContract]
        Korisnik GetRandomUser(string currentUser);
    }
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface ISub
    {
        [OperationContract(IsOneWay =true)]
        void InitSub(string username, string password);
        [OperationContract(IsOneWay = true)]
        void InitSinglePerson(string name, string surname, string city, string phone, int age);
    }
    public interface ICallback
    {
        [OperationContract(IsOneWay =true)]
        void MessageArrived(string message);

        [OperationContract(IsOneWay = true)]
        void MessageArrived2(string message, Korisnik k);
    }
    [DataContract]
    public class Korisnik
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public int age { get; set; }

    }

}
