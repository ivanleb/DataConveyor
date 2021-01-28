namespace DataConveyor
{

    public interface IConveyorBlock<TInput, TOutput> : IInputConveyorBlock<TInput>, IOutputConveyorBlock<TOutput>
    {

    }
}