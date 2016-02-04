namespace HelloLockless
{
    class Program
    {
        static void Main(string[] args)
        {
            var a1 = new NoLockApp();
            a1.Run();

            var a2 = new NativeLockApp();
            a2.Run();

            var a3 = new InterlockedApp();
            a3.Run();
        }
    }
}
