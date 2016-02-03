using System;

namespace HelloLockless
{
    public class NoLockApp : AppBase
    {
        public void Run()
        {
            Console.WriteLine("No locks");
            DateTime start = DateTime.Now;
            for (int i = 0; i < MAX; ++i)
            {
                ++j;
            }
            Console.Write(j);
            DateTime finish = DateTime.Now;
            Console.Write(" increments Time taken = {0}", finish - start);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}