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
    System.Timers.Timer _timer = null;
    int i = 0;
    Helper helper = null;

    private User _thisUser = null;
    
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
      bool res = helper.ValidateUser(ref user);
      // user exists
      if (_thisUser != null)
        res = false;
      callback.InitSinglePersonR(res);
      
      if (!res)
        return;

      _thisUser = new User();
      _thisUser.BrTelefona = user.BrTelefona;
      // Optimize this, either copy or use better data structure?
      // Ovo nije obavezno, nego samo kul tako da bude
      ThreadPool.QueueUserWorkItem(_ =>
      {
        foreach (KeyValuePair<User, Data> kvp in DB.Users)
        {
          kvp.Value.Callback.Announce(user.Ime);
        }

        DB.AddUser(user, callback);
      });
    }

    public void LetterResponse(int value, string responseTo)
    {
      // have 0 -2 values but for now just leave it be
      
      if (value < 0 || value > 2)
        return;
      DB.Users[_thisUser].Available = true;
    }
  }
}
