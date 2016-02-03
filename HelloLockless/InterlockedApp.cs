using System;
using System.Threading;

namespace HelloLockless
{
    public class InterlockedApp : AppBase
    {
        public void Run()
        {
            Console.WriteLine("Interlocked Increment");
            DateTime start = DateTime.Now;
            for (int i = 0; i < MAX; ++i)
            {
                Interlocked.Increment(ref j);
            }
            Console.Write(j);
            DateTime finish = DateTime.Now;
            Console.Write(" increments Time taken = {0}", finish - start);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}