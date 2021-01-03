namespace DataConveyor
{
    public class ConnectionMaker : IConnectionMaker
    {
        public IConnectionMaker Connect<T>(IInputConveyorBlock<T> input, IOutputConveyorBlock<T> output)
            where T : class
        {
            new Connector<T>()
                .Connect(output)
                .Connect(input);
            return this;
        }
    }
}
