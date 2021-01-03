using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ProducerConveyorBlock<TOutput> : IOutputConveyorBlock<TOutput>
    {
        private Func<TOutput> _dataGenerator;
        private TimeSpan _minProduceTime;

        public IConnector<TOutput> DataSink { get; private set; }

        protected ProducerConveyorBlock(Func<TOutput> dataGenerator, TimeSpan minProduceTime)
        {
            _dataGenerator = dataGenerator;
            _minProduceTime = minProduceTime;
        }

        public AutoResetEvent OutputPulse { get; private set; }

        public void Connect(IConnector<TOutput> outputBlock)
        {
            DataSink = outputBlock;
            OutputPulse = outputBlock.Pulse;
        }

        private void Produce()
        {
            DataSink.Push(_dataGenerator.Invoke());
        }

        public void Run(Object state)
        {
            while (true)
            {                
                Produce();
                Thread.Sleep(_minProduceTime);
                OutputPulse.Set();
            }
        }

        public void Dispose()
        {
            OutputPulse.Dispose();
        }
    }
}
