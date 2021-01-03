using System.Collections.Concurrent;
using System.Threading;

namespace DataConveyor
{
    public class Connector<T> : IConnector<T>
    { 
        private readonly ConcurrentQueue<T> _cache;
        public AutoResetEvent Pulse { get; }

        public Connector()
        {
            _cache = new ConcurrentQueue<T>();
            Pulse = new AutoResetEvent(false);
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
            Pulse.Dispose();
        }
    }
}
