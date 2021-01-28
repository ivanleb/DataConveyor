using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConveyorBlock<TInput, TOutput> : IConveyorBlock<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _dataHandler;
        private CancellationTokenSource _cts;
        private ManualResetEvent _pauseEvent;
        private Boolean _isPaused;

        private IConnector<TInput> _dataSource;
        private IConnector<TOutput> _dataSink;

        IConnector<TInput> IInputConveyorBlock<TInput>.Connector 
        { 
            get => _dataSource; 
            set => _dataSource = value; 
        }

        IConnector<TOutput> IOutputConveyorBlock<TOutput>.Connector
        {
            get => _dataSink;
            set => _dataSink = value;
        }

        public Guid Id { get; }

        public ConveyorBlock(Func<TInput, TOutput> dataHandler)
        {
            _dataHandler = dataHandler;
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
                DoConveyorStep();
            }
        }

        private void DoConveyorStep()
        {
            TInput inputData = _dataSource.Pull();
            TOutput outputData = _dataHandler.Invoke(inputData);
            _dataSink.Push(outputData);
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
            _dataSink.Dispose();
            _cts.Dispose();
            _pauseEvent.Dispose();
        }
    }
}
