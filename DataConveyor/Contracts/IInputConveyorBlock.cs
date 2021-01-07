namespace DataConveyor
{
    public interface IInputConveyorBlock<TInput> : IBlock
        where TInput : class
    {
        IConnector<TInput> Connect(IConnector<TInput> inputConnector);
    }
}
