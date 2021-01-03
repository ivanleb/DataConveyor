﻿namespace DataConveyor
{
    public class ConnectionMaker : IConnectionMaker
    {
        public IConnectionMaker Connect<T>(IInputConveyorBlock<T> input, IOutputConveyorBlock<T> output)
            where T : class
        {
            Connector<T> connector = new Connector<T>();
            connector
                .Connect(output)
                .Connect(input);
            return this;
        }
    }
}
