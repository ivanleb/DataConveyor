using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ProducerConveyorBlock<TOutput> : IOutputConveyorBlock<TOutput>
        where TOutput : class
    {
        private readonly ILog _log;
        private readonly Func<TOutput> _dataGenerator;
        private IConnector<TOutput> _dataSink;

        protected ProducerConveyorBlock(Func<TOutput> dataGenerator, ILog log)
        {
            _dataGenerator = dataGenerator;
            _log = log;
        }

        public IConnector<TOutput> Connect(IConnector<TOutput> outputBlock)
        {
            if(_dataSink == null)
                _dataSink = outputBlock;
            return _dataSink;
        }

        public void Run(Object state)
        {
            int i = 0;
            while (true)
            {
                _log.Info("Producer loop: " + i++);
                Produce();
            }
        }

        private void Produce()
        {

            TOutput data = _dataGenerator.Invoke();
            _log.Info("Produce: " + data );
            _dataSink.Push(data);
        }

        public void Dispose()
        {
            _dataSink.Dispose();
        }
    }
}
