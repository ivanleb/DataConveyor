namespace DataConveyor
{
    public interface IConnectableBlock<T> : IBlock
    {
        void Connect(IConnector<T> inputBlock);
    }
}
