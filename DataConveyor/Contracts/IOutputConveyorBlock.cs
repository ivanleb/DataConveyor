namespace DataConveyor
{
    public interface IOutputConveyorBlock<TOutput> : IBlock
    {
        IConnector<TOutput> DataSink { get; }
        void Connect(IConnector<TOutput> outputBlock);
    }
}
