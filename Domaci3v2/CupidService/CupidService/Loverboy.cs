using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

//U WCF servisnim klasama, ugovor o podacima se definiše upotrebom DataContract atributa.
//Klasa ili struktura obeležena ovim atributom može imati jednog ili više članova obeleženih
//DataMember atributom, ukazujući na to da bi taj član trebalo da bude uključen u serijalizovanu
//vrednost te klase/strukture.
//Atributi Loverboy klase se kao struktura prenose, i zbog toga se koristi DataMember

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
