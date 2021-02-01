using System;
using System.Collections.Generic;
using System.Threading;
#nullable enable
namespace DataConveyor
{
    public class Connector<T> : IConnector<T>
    {
        private readonly Queue<T?> _queue = new Queue<T?>();
        private readonly Object _locker = new Object();

        private Int32 _bufferMaxSize;

        public Int32 Capacity => _bufferMaxSize;

        public Int32 Size => _queue.Count;

        public Guid Id { get; } = Guid.NewGuid();

        public Connector(int bufferSize)
        {
            _bufferMaxSize = bufferSize;
        }

        public void Push(T? item)
        {
            lock (_locker)
            {
                while (_queue.Count >= _bufferMaxSize)
                    Monitor.Wait(_locker);

                _queue.Enqueue(item);

                if (_queue.Count == 1)
                    Monitor.PulseAll(_locker);
            }
        }

        public T? Pull()
        {
            lock (_locker)
            {
                while (_queue.Count == 0)
                    Monitor.Wait(_locker);

                T? item = _queue.Dequeue();

                if (_queue.Count == _bufferMaxSize - 1)
                    Monitor.PulseAll(_locker);

                return item;
            }
        }
    }
#nullable restore
}
