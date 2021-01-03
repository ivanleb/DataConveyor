namespace DataConveyor
{
    public interface IInputConveyorBlock<TInput> : IBlock
        where TInput : class
    {
        void Connect(IConnector<TInput> inputConnector);
    }
}
