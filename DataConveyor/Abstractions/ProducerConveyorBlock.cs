using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ProducerConveyorBlock<TOutput> : IOutputConveyorBlock<TOutput>
        where TOutput : class
    {
        private readonly Func<TOutput> _dataGenerator;
        private IConnector<TOutput> _dataSink;

        IConnector<TOutput> IOutputConveyorBlock<TOutput>.Connector 
        { 
            get => _dataSink; 
            set => _dataSink = value; 
        }

        public Guid Id { get; }

        protected ProducerConveyorBlock(Func<TOutput> dataGenerator)
        {
            _dataGenerator = dataGenerator;
            Id = Guid.NewGuid();
        }

        public void Run(Object state)
        {
            while (true)
            {
                Produce();
            }
        }

        private void Produce()
        {

            TOutput data = _dataGenerator.Invoke();
            _dataSink.Push(data);
        }

        public void Dispose()
        {
            _dataSink.Dispose();
        }
    }
}
