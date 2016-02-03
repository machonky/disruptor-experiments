using System;
using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;

namespace HelloDisruptor
{
    public class App : AppBase
    {
        public class IncrementHandler : IEventHandler<IncrementCounterCommand>
        {
            public int Counter;

            public void OnNext(IncrementCounterCommand data, long sequence, bool endOfBatch)
            {
                ++Counter;
            }
        }

        public void Run()
        {
            Console.WriteLine("Disruptor");

            int bufferSize = 2 << 10;
            var objIdService = new ObjectIdService();
            var clock = new UtcClockService();

            var claimStrategy = new SingleThreadedClaimStrategy(bufferSize);
            var waitStrategy = new SleepingWaitStrategy();
            var incrementHandler = new IncrementHandler();

            var disruptor = new Disruptor<IncrementCounterCommand>(() => new IncrementCounterCommand(objIdService.NewGuid(), clock.UtcNow), 
                claimStrategy, waitStrategy, TaskScheduler.Default);

            disruptor.HandleEventsWith(incrementHandler);

            var eventPublisher = new EventPublisher<IncrementCounterCommand>(disruptor.RingBuffer);

            disruptor.Start();

            var start = DateTime.Now;
            for (int i = 0; i <= MAX; ++i)
            {
                eventPublisher.PublishEvent(Translate);
            }

            Console.Write(incrementHandler.Counter);
            var finish = DateTime.Now;
            var totalSeconds = (finish - start).TotalSeconds;
            Console.Write(" increments Time taken = {0} {1} M-ops/s", totalSeconds, (incrementHandler.Counter/totalSeconds)/1000000);
            Console.WriteLine();
            Console.ReadKey();
        }

        private IncrementCounterCommand Translate(IncrementCounterCommand arg1, long arg2)
        {
            // the command is just a trigger in this instance
            return arg1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new App();
            a.Run();
        }
    }
}
