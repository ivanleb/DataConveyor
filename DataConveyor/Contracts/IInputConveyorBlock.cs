namespace DataConveyor
{
    public interface IInputConveyorBlock<TInput> : IConnectableBlock<TInput>
    {
        IConnector<TInput> DataSource { get; }
    }
}
