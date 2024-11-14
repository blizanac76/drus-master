using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLib
{
  public static class DB
  {
    private static ConcurrentDictionary<User, Data> db = null;
    //private static ConcurrentDictionary<User, byte> userList = new ConcurrentDictionary<string, byte>();
    static DB()
    {
      db = new ConcurrentDictionary<User, Data>();
      //throw new Exception("DB");
    }
    
    public static ConcurrentDictionary<User, Data> Users { get { return db; } }
    //public static ConcurrentDictionary<string, byte> UserList { get { return userList; } }
    public static void AddUser(User u, ICupidDuplexCallback callback) => db[u] = new Data(callback, true);


  }

  public class User
  {
    public string Ime { get; set;}
    public string Grad { get; set;}
    public int Godine { get; set;}
    public string BrTelefona { get; set;}

    public override bool Equals(object obj)
    {
      return this.BrTelefona == (obj as User).BrTelefona;
    }
    //
    public override int GetHashCode()
    {
      return this.BrTelefona.GetHashCode();
    }
  }

  public class Data
  {
    public Data(ICupidDuplexCallback callback, bool available)
    {
      Callback = callback;
      Available = available;
    }

    public ICupidDuplexCallback Callback { get; set; }
    public bool Available { get; set; } = true; 
  }
}
