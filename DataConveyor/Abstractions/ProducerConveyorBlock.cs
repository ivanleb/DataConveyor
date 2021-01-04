using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ProducerConveyorBlock<TOutput> : IOutputConveyorBlock<TOutput>
        where TOutput : class
    {
        private readonly Func<TOutput> _dataGenerator;
        private readonly TimeSpan _minProduceTime;
        private ManualResetEvent _outputPulse;
        private IConnector<TOutput> _dataSink;

        protected ProducerConveyorBlock(Func<TOutput> dataGenerator, TimeSpan minProduceTime)
        {
            _dataGenerator = dataGenerator;
            _minProduceTime = minProduceTime;
        }

        public void Connect(IConnector<TOutput> outputBlock)
        {
            _dataSink = outputBlock;
            _outputPulse = outputBlock.Pulse;
        }

        public void Run(Object state)
        {
#if DEBUG
            int i = 0;
#endif
            while (true)
            {
#if DEBUG
                Console.WriteLine("Producer loop: " + i++);
#endif
                _outputPulse.Reset();
                Produce();
                Thread.Sleep(_minProduceTime);
                _outputPulse.Set();
            }
        }

        private void Produce()
        {

            TOutput data = _dataGenerator.Invoke();
#if DEBUG
            Console.WriteLine("Produce: " + data );
#endif
            _dataSink.Push(data);
        }

        public void Dispose()
        {
            _dataSink.Dispose();
        }
    }
}
