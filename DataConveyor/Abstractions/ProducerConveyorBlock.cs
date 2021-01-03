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
            while (true)
            {
                _outputPulse.Reset();
                Produce();
                Thread.Sleep(_minProduceTime);
                _outputPulse.Set();
            }
        }

        private void Produce()
        {
            _dataSink.Push(_dataGenerator.Invoke());
        }

        public void Dispose()
        {
            _dataSink.Dispose();
        }
    }
}
