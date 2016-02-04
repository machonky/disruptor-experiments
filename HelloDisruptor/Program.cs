using System;
using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;

namespace HelloDisruptor
{
    public class App : AppBase
    {
        public class IncrementCounterHandler : IEventHandler<IncrementCounterCommand>
        {
            public int Counter;

            public void OnNext(IncrementCounterCommand data, long sequence, bool endOfBatch)
            {
                ++Counter;
            }
        }

        public class IncrementCounterTranslator : IEventTranslator<IncrementCounterCommand>
        {
            private readonly IObjectIdService _objectIdService;
            private readonly IUtcClockService _clockService;

            public IncrementCounterTranslator(IObjectIdService objectIdService, IUtcClockService clockService)
            {
                _objectIdService = objectIdService;
                _clockService = clockService;
            }

            public IncrementCounterCommand TranslateTo(IncrementCounterCommand eventData, long sequence)
            {
                eventData.Init(_objectIdService.NewGuid(),_clockService.UtcNow);
                return eventData;
            }
        }

        public void Run()
        {
            Console.WriteLine("Disruptor");

            int bufferSize = 2 << 10;
            var objIdService = new ObjectIdService();
            var clock = new UtcClockService();

            var claimStrategy = new SingleThreadedClaimStrategy(bufferSize);
            var waitStrategy = new YieldingWaitStrategy();
            var incrementHandler = new IncrementCounterHandler();

            var disruptor = new Disruptor<IncrementCounterCommand>(() => new IncrementCounterCommand(), 
                claimStrategy, waitStrategy, TaskScheduler.Default);

            disruptor.HandleEventsWith(incrementHandler);

            var eventPublisher = new EventPublisher<IncrementCounterCommand>(disruptor.RingBuffer);
            var eventTranslator = new IncrementCounterTranslator(objIdService, clock);

            disruptor.Start();

            var start = DateTime.Now;
            for (int i = 0; i <= Iterations; ++i)
            {
                eventPublisher.PublishEvent(eventTranslator.TranslateTo);
            }

            var finish = DateTime.Now;
            disruptor.Shutdown();

            var totalSeconds = (finish - start).TotalSeconds;
            var totalCount = incrementHandler.Counter;
            Console.Write("{0} increments Time taken = {1} {2} M-ops/s\n", totalCount, totalSeconds, totalCount / (1 * Million) / totalSeconds);

            Console.ReadKey();
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
