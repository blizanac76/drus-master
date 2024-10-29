using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci2
{
    internal class Klasa
    {
        static void Main()
        {
            //SemaphoreSlimTest();
            SemaphoreTest();
            Console.ReadLine();

        }

        protected static void SemaphoreSlimTest()
        {
            var semaphore = new SemaphoreSlim(2, 5);
            for (int i = 0; i<10; i++)
            {
                Task.Run(() =>
                {
                    Console.WriteLine("Task {0} ceka", Task.CurrentId);
                    semaphore.Wait();
                    Console.WriteLine("Task {0} usao u semafor", Task.CurrentId);
                    Thread.Sleep(1000 * ((int)Task.CurrentId + 1));
                    Console.WriteLine("Task {0} se izvrsio i izasao je iz semafora (release)", Task.CurrentId);
                    semaphore.Release();

                });
            }
        }
        internal static void SemaphoreTest()
        {
            var semaphore = new Semaphore(2, 5, "systemSemaphore");
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    Console.WriteLine("Task {0} ceka", Task.CurrentId);
                    semaphore.WaitOne();
                    Console.WriteLine("Task {0} usao u semafor", Task.CurrentId);
                    Thread.Sleep(1000 * ((int)Task.CurrentId + 1));
                    Console.WriteLine("Task {0} se izvrsio i izasao je iz semafora (release)", Task.CurrentId);
                    semaphore.Release();

                });
            }
        }
    }
}
