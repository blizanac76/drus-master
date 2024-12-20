﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WCFLib
{
  //upravlja tajmerom za slanje ljubavnih pisama na svakih 10 sekundi, minut je predugo
  public static class Loop
  {
    private static System.Timers.Timer _timer = null;
    private static object lockObj = new object();
    private static bool timerStarted = false;
    static Loop()
    {
      //throw new Exception();
    }
    //start tajmera
    public static void Start()
    { 
      if (timerStarted) return;
      lock (lockObj)
      {
        if (timerStarted) return;
        StartTimer();
        //ne nikako
        //throw new Exception();
        timerStarted = true;
      }
    }
    // kad istekne vreme ovo se desi - nasumicnom korisniku se salje ljubavno pismo
    private static void ElapsedTimer(object sender, ElapsedEventArgs e)
    {
     // throw new Exception();
      
      ThreadPool.QueueUserWorkItem(_ =>
      {
        Random r = new Random();
        // 
        //Debugger.Launch();
        var sr = AppDomain.CurrentDomain.FriendlyName;
        List<User> userList = DB.Users.Keys.ToList();
        if (userList.Count <= 1)
          return;

        foreach (var user in userList)
        {
          if (!DB.Users[user].Available)
            continue;
          // :) 
          //int i = r.Next(userList.Count - 1);
          //if (userList[i] == user)
          //  i = (userList.Count < (i + 1)) ? i : i - 1;
          //userList[i] = user;
          // ovo bi moglo lepse, previse accessa i novi objekat ...
          DB.Users[user].Callback.GetLetter(user);
          DB.Users[user] = new Data(DB.Users[user].Callback, false);
        }
      });
    }
    //inicijalizacija tajmera
    static void StartTimer()
    {
      _timer = new System.Timers.Timer(1000 * 10);
      _timer.Elapsed += ElapsedTimer;
      _timer.AutoReset = true;
      _timer.Enabled = true;
    }
  }
}
