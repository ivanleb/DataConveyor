using System;

namespace DataConveyor
{
    public interface IConnector<T>
    {
        Guid Id { get; }
        T? Pull();
        void Push(T? item);
        Int32 Capacity { get; }
        Int32 Size { get; }
    }
}