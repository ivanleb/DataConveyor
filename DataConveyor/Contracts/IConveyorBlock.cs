namespace DataConveyor
{

    public interface IConveyorBlock<TInput, TOutput> : IInputConveyorBlock<TInput>, IOutputConveyorBlock<TOutput>
        where TInput : class
        where TOutput : class
    {

    }
}