using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Dictionary<User, string> dict = new Dictionary<User, string>();
      User u1 = new User()
      {
        i = 0,
        test = "u1"
      };

      User u2 = new User()
      {
        i = 1,
        test = "u2"
      };

      User u3 = new User()
      {
        i = 3,
        test = "u1"
      };

      dict[u1] = "u1";
      dict[u2] = "u2";
      dict[u3] = "u3";

      int i = 0;

    }



  }

  public class User
  {
    public int i { get; set; }
    public string test { get; set; }

    public override bool Equals(object obj)
    {
      return this.test == (obj as User).test;
    }
  }

}
