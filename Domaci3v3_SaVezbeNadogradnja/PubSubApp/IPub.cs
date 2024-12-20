﻿using System;
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
        //metode sa 2 na kraju su moje
        [OperationContract(IsOneWay =true)]
        void SendMessage(string message);
        //salje poruku
        [OperationContract(IsOneWay = true)]
        void SendMessage2(string message, Korisnik k);
        //random korisnik ce slati poruku i svoje podatke nekom drugom korisniku
        [OperationContract]
        Korisnik GetRandomUser(string currentUser);
    }
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface ISub
    {
        [OperationContract(IsOneWay =true)]
        void InitSub(string username, string password);
        //inicijalizacija korisnika
        [OperationContract(IsOneWay = true)]
        void InitSinglePerson(string name, string surname, string city, string phone, int age);
    }
    public interface ICallback
    {
        [OperationContract(IsOneWay =true)]
        void MessageArrived(string message);
        //callback nazad da je stigla poruka

        [OperationContract(IsOneWay = true)]
        void MessageArrived2(string message, Korisnik k);
    }
    //struktura za serijalizaciju, korisnik 
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
