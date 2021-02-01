using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConsumerConveyorBlock<TInput> : IInputConveyorBlock<TInput>
    {
        private readonly Action<TInput> _dataConsumer; 
        private CancellationTokenSource _cts;
        private ManualResetEventSlim _pauseEvent;
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
            _cts = new CancellationTokenSource();
            _pauseEvent = new ManualResetEventSlim(true);
        }

        public void Run(Object state)
        {
            if (!_pauseEvent.IsSet)
            {
                _pauseEvent.Set();
                return;
            }

            while (!_cts.Token.IsCancellationRequested)
            {
                _pauseEvent.Wait();
                Consume();
            }
        }

        private void Consume()
        {
            TInput? inputData = _dataSource.Pull();
            if(inputData != null)
                _dataConsumer.Invoke(inputData);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void Pause()
        {
            _pauseEvent.Reset();
        }

        public void Dispose()
        {
            _cts.Dispose();
            _pauseEvent.Dispose();
        }
    }
}
