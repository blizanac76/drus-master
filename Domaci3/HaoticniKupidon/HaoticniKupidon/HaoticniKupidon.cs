using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;


namespace HaoticniKupidon
{
    public class HaoticniKupidon: IHaoticniKupidon
    {

        //Korisnici su smesteni u recnik. Kljuc je klasa Loverboy koja predstavlja svakog korisnika. Vrednost je
        //Callback Servisa
        private Dictionary<Loverboy, IHaoticniKupidonCallback> prijavljeniLoverboy = new Dictionary<Loverboy, IHaoticniKupidonCallback>();
        //Poruke koje Loverboy ispisuje drugom Loverboyu, korisniku
        private List<string> poruke = new List<string>
        {
             "Zelim da se upoznamo",
             "Ne zelim da se upoznamo",
             "Radujem se nasem susretu!"
        };

        public void SlanjePisma()
        {

            foreach (var loverboyy in prijavljeniLoverboy.Keys.ToList())
            {
                //da ne salje samom sebi, pravi se nova lista bez tog loverboya
                var dostupniLoverboyi = prijavljeniLoverboy.Keys.Where(p => p != loverboyy).ToList();

                if (dostupniLoverboyi.Count == 0) continue;

                var posiljalac = dostupniLoverboyi[new Random().Next(dostupniLoverboyi.Count)];
                var poruka = poruke[new Random().Next(poruke.Count)];

                var callback = prijavljeniLoverboy[loverboyy];
                //metoda prijema pisma
            }
        }
        //public void PosaljiPismo()
        //{


        //}
        public void initSinglePerson()
        {
            //Korisnik je uneo svoje podatke i poziva se ova metoda za proslednjivanje istih iz klijent klase KupidonKlijent.cs

        }
    }
}