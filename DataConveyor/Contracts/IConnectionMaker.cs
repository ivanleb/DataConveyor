namespace DataConveyor
{
    public interface IConnectionMaker
    {
        IConnectionMaker Connect<T>(IInputConveyorBlock<T> input, IOutputConveyorBlock<T> output);
    }
}
