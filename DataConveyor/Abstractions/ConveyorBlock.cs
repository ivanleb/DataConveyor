using System;
using System.Reflection.Metadata.Ecma335;

namespace DataConveyor
{
    public abstract class ConveyorBlock<TInput, TOutput> : IConveyorBlock<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly Func<TInput, TOutput> _dataHandler;
        private readonly String _id = Guid.NewGuid().ToString().Substring(0, 5);

        private IConnector<TInput> _dataSource;
        private IConnector<TOutput> _dataSink;

        IConnector<TInput> IInputConveyorBlock<TInput>.Connector 
        { 
            get => _dataSource; 
            set => _dataSource = value; 
        }

        IConnector<TOutput> IOutputConveyorBlock<TOutput>.Connector
        {
            get => _dataSink;
            set => _dataSink = value;
        }

        public Guid Id { get; }

        public ConveyorBlock(Func<TInput, TOutput> dataHandler)
        {
            _dataHandler = dataHandler;
            Id = Guid.NewGuid();
        }

        public void Run(Object state)
        {
            while (true)
            {
                DoConveyorStep();
            }
        }

        private void DoConveyorStep()
        {
            TInput inputData = _dataSource.Pull();
            if (inputData != default)
            {
                TOutput outputData = _dataHandler.Invoke(inputData);
                if (outputData != default)
                    _dataSink.Push(outputData);
            }
        }

        public void Dispose()
        {
            _dataSource.Dispose();
            _dataSink.Dispose();
        }
    }
}
