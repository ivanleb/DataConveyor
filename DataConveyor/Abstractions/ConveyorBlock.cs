using System;
using System.Reflection.Metadata.Ecma335;

namespace DataConveyor
{
    public abstract class ConveyorBlock<TInput, TOutput> : IConveyorBlock<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly ILog _log;
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

        public ConveyorBlock(Func<TInput, TOutput> dataHandler, ILog log)
        {
            _dataHandler = dataHandler;
            _log = log;
        }

        public Boolean TryConnect(IOutputConveyorBlock<TInput> outputBlock, ConnectionSpec spec)
        {
            return this.Connect(outputBlock, spec);
        }

        public Boolean TryConnect(IInputConveyorBlock<TOutput> inputBlock, ConnectionSpec spec)
        {
            return this.Connect(inputBlock, spec);
        }

        public void Run(Object state)
        {
            int i = 0;

            while (true)
            {
                _log.Info($" {_id} Handle loop: " + i++);

                DoConveyorStep();
            }
        }

        private void DoConveyorStep()
        {
            TInput inputData = _dataSource.Pull();
            _log.Info($" {_id} Handle: " + inputData);
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
