using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kupidon
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(ICallback), SessionMode = SessionMode.Required)]
    public interface ISub
    {

        [OperationContract(IsOneWay = true)]
        void initSinglePerson(Korisnik korisnik);

    }

    [ServiceContract]
    public interface IPub
    {
        [OperationContract(IsOneWay = true)]
        void sendLetters(string poruka);
    }

    [ServiceContract]
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void OnNotified(string message, List<Korisnik> korisnici);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.

    [DataContract]
    public class Korisnik
    {
        [DataMember]
        public string Ime { get; set; }

        [DataMember]
        public string Prezime {  get; set; }

        [DataMember]
        public string Grad { get; set; }

        [DataMember]
        public string BrojTelefona { get; set; }

        [DataMember]
        public int Godine { get; set; }

        [DataMember]
        public bool PrimioPoruku { get; set; }
    }
}
