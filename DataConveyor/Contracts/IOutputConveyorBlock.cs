using System;

namespace DataConveyor
{
    public interface IOutputConveyorBlock<TOutput> : IBlock
    {
        IConnector<TOutput> Connector { get; internal set; }
    }
}
