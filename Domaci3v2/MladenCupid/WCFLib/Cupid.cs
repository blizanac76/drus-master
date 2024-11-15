using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;
using System.Timers;

namespace WCFLib
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  public class Cupid : ICupid
  {
    
    ICupidDuplexCallback callback = null;
    //koristi se za slanje ljubavnih pisama na intercalu
    System.Timers.Timer _timer = null;
    int i = 0;
    // Helper sluzi za validaciju podataka novog usera
    Helper helper = null;
    //trenutni korisnik
    private User _thisUser = null;
    //inicijalizuje se callback i helper klasa pokrece se loop za matchmaking gde se upoznaju ljudi
    public Cupid()
    {
      var curr = OperationContext.Current;
      callback = OperationContext.Current.GetCallbackChannel<ICupidDuplexCallback>();
      helper = new Helper();
    }

    public void GetData(int value)
    {
      callback.PrintShit(string.Format("You entered: {0}", value));
    }

    public void InitSinglePerson(User user)
    {
      
      //File.WriteAllText("debug.txt", "before loop");
      Loop.Start();
      //da li su dobri podaci
      bool res = helper.ValidateUser(ref user);
      // postoji korsinik
      if (_thisUser != null)
        res = false;
      callback.InitSinglePersonR(res);
      
      if (!res)
        return;

      _thisUser = new User();
      _thisUser.BrTelefona = user.BrTelefona;
      // moze se optimizovati, 
      // Ovo nije obavezno, nego samo kul tako da bude za koji bod
      //da se registracija korisnika izvrsi asinhrono ne blokira main nit
      //
      ThreadPool.QueueUserWorkItem(_ =>
      {
          //dodavanje u bazu db korisnika (db.users)
        foreach (KeyValuePair<User, Data> kvp in DB.Users)
        {
          kvp.Value.Callback.Announce(user.Ime);
        }

        DB.AddUser(user, callback);
      });
    }

    // odgovor na ljubavno pismo
    public void LetterResponse(int value, string responseTo)
    {
      // value je odgovor na pismo, 
      //glup nacin ali jednostavan
      //korisnik ovako potvrdjuje da li je dostupan za nova pisma
      //responseTo ipak ne treba :)
      
      if (value < 0 || value > 2)
        return;
      DB.Users[_thisUser].Available = true;
    }
  }
}
