using System;

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

        public ConveyorBlock(Func<TInput, TOutput> dataHandler, ILog log)
        {
            _dataHandler = dataHandler;
            _log = log;
        }

        public IConnector<TInput> Connect(IConnector<TInput> inputBlock)
        {
            if (_dataSource == null)
                _dataSource = inputBlock;
            return _dataSource;
        }

        public IConnector<TOutput> Connect(IConnector<TOutput> outputBlock)
        {
            if (_dataSink == null)
                _dataSink = outputBlock;
            return _dataSink;
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
