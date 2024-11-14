using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLib
{
  public class Helper
  {
    public bool ValidateUser(ref User user)
    {
      // a lot of alloc but yeah
      StringBuilder sb = new StringBuilder();
      user.BrTelefona = user.BrTelefona.Trim();
      if (user.BrTelefona.Length > 15)
        return false;
      for (int i = 0; i < user.BrTelefona.Length; i++)
      {
        char c = user.BrTelefona[i];
        if (Char.IsDigit(c))
          sb.Append(c);
      };
      user.BrTelefona = sb.ToString();

      if (user.BrTelefona.Length < 8)
        return false;
      
      user.Ime = user.Ime.Trim();
      user.Grad = user.Grad.Trim();

      if (string.IsNullOrWhiteSpace(user.Ime) || user.Ime.Length > 24)
        return false;
      if (string.IsNullOrWhiteSpace(user.Grad) || user.Grad.Length > 50)
        return false;
      if (user.Godine < 18 || user.Godine > 150)
        return false;

      return true;
    }
  }
}
