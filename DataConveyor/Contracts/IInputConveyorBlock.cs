using System;

namespace DataConveyor
{
    public interface IInputConveyorBlock<TInput> : IBlock
    {
        IConnector<TInput> Connector { get; internal set; }
    }
}
