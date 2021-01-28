using System;

namespace DataConveyor
{
    public interface IConnector<T> : IDisposable
    {
        T Pull();
        void Push(T item);
        Int32 Capacity { get; }
        Int32 Length { get; }
    }
}