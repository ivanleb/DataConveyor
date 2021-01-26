using System;

namespace DataConveyor
{
    public abstract class ConsumerConveyorBlock<TInput> : IInputConveyorBlock<TInput>
         where TInput : class
    {
        private readonly Action<TInput> _dataConsumer;
        private IConnector<TInput> _dataSource;

        IConnector<TInput> IInputConveyorBlock<TInput>.Connector 
        { 
            get => _dataSource; 
            set => _dataSource = value; 
        }

        public Guid Id { get; }

        protected ConsumerConveyorBlock(Action<TInput> dataConsumer)
        {
            _dataConsumer = dataConsumer;
            Id = Guid.NewGuid();
        }

        public void Run(Object state)
        {
            while (true)
                Consume();
        }

        private void Consume()
        {
            TInput inputData = _dataSource.Pull();

            if (inputData != default)
                _dataConsumer.Invoke(inputData);
        }

        public void Dispose()
        {
            _dataSource.Dispose();
        }
    }
}
