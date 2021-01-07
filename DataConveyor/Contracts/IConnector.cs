using System;
using System.Threading;

namespace DataConveyor
{
    public interface IConnector<T> : IDisposable
        where T : class
    {
        IConnector<T> Connect(IOutputConveyorBlock<T> outputBlock);
        IConnector<T> Connect(IInputConveyorBlock<T> inputBlock);
        T Pull();
        void Push(T item);
    }
}