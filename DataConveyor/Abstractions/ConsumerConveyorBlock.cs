using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConsumerConveyorBlock<TInput> : IInputConveyorBlock<TInput>
    {
        private readonly Action<TInput> _dataConsumer; 
        private CancellationTokenSource _cts;
        private ManualResetEvent _pauseEvent;
        private Boolean _isPaused;
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
            _pauseEvent = new ManualResetEvent(true);
        }

        public void Run(Object state)
        {
            if (_isPaused)
            {
                _pauseEvent.Set();
                _isPaused = false;
                return;
            }

            while (!_cts.Token.IsCancellationRequested)
            {
                _pauseEvent.WaitOne();
                Consume();
            }
        }

        private void Consume()
        {
            TInput inputData = _dataSource.Pull();
            _dataConsumer.Invoke(inputData);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void Pause()
        {
            _pauseEvent.Reset();
            _isPaused = true;
        }

        public void Dispose()
        {
            _dataSource.Dispose();
            _cts.Dispose();
            _pauseEvent.Dispose();
        }
    }
}
