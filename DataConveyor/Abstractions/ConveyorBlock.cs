using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConveyorBlock<TInput, TOutput> : IConveyorBlock<TInput, TOutput> 
        where TInput : class
        where TOutput : class
    {
        private Func<TInput, TOutput> _dataHandler;

        private AutoResetEvent _inputPulse;
        private AutoResetEvent _outputPulse;

        public IConnector<TInput> DataSource { get; private set; }
        public IConnector<TOutput> DataSink { get; private set; }

        public ConveyorBlock(Func<TInput, TOutput> dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void Connect(IConnector<TInput> inputBlock)
        {
            DataSource = inputBlock;
            _inputPulse = inputBlock.Pulse;
        }

        public void Connect(IConnector<TOutput> outputBlock)
        {
            DataSink = outputBlock;
            _outputPulse = outputBlock.Pulse;
        }

        private void DoConveyorStep()
        {
            TInput inputData = DataSource.Pull();
            {
                TOutput outputData = _dataHandler.Invoke(inputData);
                DataSink.Push(outputData);
            }
        }

        public void Run(Object state)
        {
            while (true)
            {
                _inputPulse.WaitOne();
                DoConveyorStep();
                _outputPulse.Set();
            }
        }

        public void Dispose()
        {
            _outputPulse.Dispose();
            _inputPulse.Dispose();
        }
    }
}
