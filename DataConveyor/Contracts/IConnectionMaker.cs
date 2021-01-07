namespace DataConveyor
{
    public interface IConnectionMaker
    {
        IConnectionMaker Connect<T>(IOutputConveyorBlock<T> source, IInputConveyorBlock<T> target) where T : class;
    }
}
