using System;

namespace DataConveyor
{
    public interface IInputConveyorBlock<TInput> : IBlock
        where TInput : class
    {
        IConnector<TInput> Connector { get; internal set; }
    }
}
