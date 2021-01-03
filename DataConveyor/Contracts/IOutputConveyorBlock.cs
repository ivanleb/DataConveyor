namespace DataConveyor
{
    public interface IOutputConveyorBlock<TOutput> : IBlock
        where TOutput : class
    {
        void Connect(IConnector<TOutput> outputConnector);
    }
}
