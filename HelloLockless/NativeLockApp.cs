using System;

namespace HelloLockless
{
    public class NativeLockApp : AppBase
    {
        public void Run()
        {
            Console.WriteLine("Native Locks");
            object lockToken = new object();
            DateTime start = DateTime.Now;
            for (int i = 0; i < MAX; ++i)
            {
                lock(lockToken)
                {
                    ++j;
                }
            }
            Console.Write(j);
            DateTime finish = DateTime.Now;
            Console.Write(" increments Time taken = {0}", finish - start);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}