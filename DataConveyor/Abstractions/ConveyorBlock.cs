using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConveyorBlock<TInput, TOutput> : IConveyorBlock<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly Func<TInput, TOutput> _dataHandler;
        private ManualResetEvent _inputPulse;
        private ManualResetEvent _outputPulse;
        public IConnector<TInput> _dataSource;
        public IConnector<TOutput> _dataSink;

        public ConveyorBlock(Func<TInput, TOutput> dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void Connect(IConnector<TInput> inputBlock)
        {
            _dataSource = inputBlock;
            _inputPulse = inputBlock.Pulse;
        }

        public void Connect(IConnector<TOutput> outputBlock)
        {
            _dataSink = outputBlock;
            _outputPulse = outputBlock.Pulse;
        }

        public void Run(Object state)
        {
            while (true)
            {
                _inputPulse.WaitOne();
                _outputPulse.Reset();
                DoConveyorStep();
                _outputPulse.Set();
            }
        }

        private void DoConveyorStep()
        {
            TInput inputData = _dataSource.Pull();
            if(inputData != default)
            {
                TOutput outputData = _dataHandler.Invoke(inputData);
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
