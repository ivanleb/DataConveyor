namespace DataConveyor
{
    public interface IOutputConveyorBlock<TOutput> : IBlock
        where TOutput : class
    {
        IConnector<TOutput> Connect(IConnector<TOutput> outputConnector);
    }
}
