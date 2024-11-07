using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HaoticniKupidon
{
    public class Loverboy
    {
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Grad { get; set; }
        [DataMember]
        public int Godine { get; set; }
        [DataMember]
        public string brTelefona { get; set; }
    }
}