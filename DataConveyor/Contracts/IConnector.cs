using System;

namespace DataConveyor
{
    public interface IConnector<T> : IDisposable
        where T : class
    {
        T Pull();
        void Push(T item);
    }
}