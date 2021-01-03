using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DataConveyor
{
    public class Connector<T> : IConnector<T>
        where T : class
    { 
        private readonly ConcurrentQueue<T> _cache;
        private Boolean _isDisposed;

        public ManualResetEvent Pulse { get; }

        public Connector()
        {
            _cache = new ConcurrentQueue<T>();
            Pulse = new ManualResetEvent(false);
        }

        public T Pull()
        {
            T item;
            if (_cache.TryDequeue(out item))
                return item;

            return default(T);
        }

        public void Push(T item)
        {
            _cache.Enqueue(item);
        }

        public IConnector<T> Connect(IOutputConveyorBlock<T> outputBlock)
        {
            outputBlock.Connect(this);
            return this;
        }

        public IConnector<T> Connect(IInputConveyorBlock<T> inputBlock)
        {
            inputBlock.Connect(this);
            return this;
        }

        public void Dispose()
        {
            if(!_isDisposed)
                Pulse.Dispose();

            _isDisposed = true;
        }
    }
}
