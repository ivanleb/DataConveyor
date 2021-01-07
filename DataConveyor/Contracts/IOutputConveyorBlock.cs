using System;

namespace DataConveyor
{
    public interface IOutputConveyorBlock<TOutput> : IBlock
        where TOutput : class
    {
        IConnector<TOutput> Connector { get; internal set; }
        Boolean TryConnect(IInputConveyorBlock<TOutput> inputBlock, ConnectionSpec spec);
    }
}
