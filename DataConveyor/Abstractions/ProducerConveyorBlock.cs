using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ProducerConveyorBlock<TOutput> : IOutputConveyorBlock<TOutput>
    {
        private readonly Func<(TOutput Data, Boolean IsStopped)> _dataGenerator; 
        private CancellationTokenSource _cts;
        private ManualResetEvent _pauseEvent;
        private Boolean _isPaused;
        private IConnector<TOutput> _dataSink;

        IConnector<TOutput> IOutputConveyorBlock<TOutput>.Connector 
        { 
            get => _dataSink; 
            set => _dataSink = value; 
        }

        public Guid Id { get; }

        protected ProducerConveyorBlock(Func<(TOutput Data, Boolean IsStopped)> dataGenerator)
        {
            _dataGenerator = dataGenerator;
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
                Produce();
            }
        }

        private void Produce()
        {
            (TOutput? data, Boolean IsStopped) = _dataGenerator.Invoke();

            if(data != null)
                _dataSink.Push(data);

            if (IsStopped)
                _cts.Cancel();
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
            _dataSink.Dispose();
            _cts.Dispose();
            _pauseEvent.Dispose();
        }
    }
}
