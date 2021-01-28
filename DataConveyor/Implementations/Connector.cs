using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DataConveyor
{
    public class Connector<T> : IConnector<T>
    {
        private readonly ConcurrentQueue<T> _cache;
        private readonly Int32 _maxCacheElements = 1;

        private Boolean _isDisposed;
        private ManualResetEventSlim _inputPulse;
        private ManualResetEventSlim _outputPulse;

        public Int32 Capacity => _maxCacheElements;

        public Int32 Length => _cache.Count;

        public Connector(int maxCacheElements) 
            :this()
        {
            _maxCacheElements = maxCacheElements;
        }

        public Connector() 
        {
            _cache = new ConcurrentQueue<T>();
            _inputPulse = new ManualResetEventSlim(true);
            _outputPulse = new ManualResetEventSlim(true);
        }

        public T Pull()
        {
            _outputPulse.Wait();

            T item;
            if (_cache.TryDequeue(out item))
            {
                if (_cache.IsEmpty)
                    CloseOutputOpenInput();

                return item;
            }

            CloseOutputOpenInput();
            return default(T);
        }

        public void Push(T item)
        {
            _inputPulse.Wait();

            _cache.Enqueue(item);

            _outputPulse.Set();

            if (_cache.Count > _maxCacheElements)
            {
                _inputPulse.Reset();
            }
        }

        private void CloseOutputOpenInput()
        {
            _outputPulse.Reset();
            _inputPulse.Set();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _inputPulse.Dispose();
                _outputPulse.Dispose();
            }

            _isDisposed = true;
        }
    }
}
