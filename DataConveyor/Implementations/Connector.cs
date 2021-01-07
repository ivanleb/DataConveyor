using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DataConveyor
{
    public class Connector<T> : IConnector<T>
        where T : class
    {
        private readonly ILog _log;
        private readonly ConcurrentQueue<T> _cache;
        private readonly Int32 _maxCacheElements = 1;

        private  String _connectorId = Guid.NewGuid().ToString().Substring(0, 5) + " Connector: ";
        private Boolean _isDisposed;
        private ManualResetEvent _inputPulse { get; }
        private ManualResetEvent _outputPulse { get; }

        public Connector(int maxCacheElements, ILog log)
        {
            _cache = new ConcurrentQueue<T>();
            _inputPulse = new ManualResetEvent(true);
            _outputPulse = new ManualResetEvent(true);
            _maxCacheElements = maxCacheElements;
            _log = log;
        }

        public T Pull()
        {
            _log.Info($"{_connectorId} pull, before OutputPulse.WaitOne");

            _outputPulse.WaitOne();

            T item;
            if (_cache.TryDequeue(out item))
            {
                _log.Info($"{_connectorId} pull, Dequeue element" );

                if (_cache.IsEmpty)
                    CloseOutputOpenInput();

                return item;
            }

            _log.Info($"{_connectorId} pull, cannot Dequeue element");

            CloseOutputOpenInput();
            return default(T);
        }

        public void Push(T item)
        {
            _log.Info($"{_connectorId} push, before InputPulse.WaitOne");

            _inputPulse.WaitOne();

            _log.Info($"{_connectorId} push, after InputPulse.WaitOne");

            _cache.Enqueue(item);

            _outputPulse.Set();

            if (_cache.Count > _maxCacheElements)
            {
                _log.Info($"{_connectorId} push, max limit exceed ");

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
