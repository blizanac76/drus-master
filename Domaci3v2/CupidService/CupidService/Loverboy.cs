using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CupidService
{
    public class Loverboy
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
