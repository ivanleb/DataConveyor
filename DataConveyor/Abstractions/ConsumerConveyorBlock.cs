using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConsumerConveyorBlock<TInput> : IInputConveyorBlock<TInput>
    {
        private Action<TInput> _dataConsumer;

        public IConnector<TInput> DataSource { get; private set; }

        protected ConsumerConveyorBlock(Action<TInput> dataConsumer)
        {
            _dataConsumer = dataConsumer;
        }

        public AutoResetEvent _inputPulse;

        public void Connect(IConnector<TInput> inputBlock)
        {
            DataSource = inputBlock;
            _inputPulse = inputBlock.Pulse;
        }

        private void Consume()
        {
            TInput data = DataSource.Pull();
            _dataConsumer.Invoke(data);
        }

        public void Run(Object state)
        {
            while (true)
            {
                _inputPulse.WaitOne();
                Consume();
            }
        }

        public void Dispose()
        {
            _inputPulse.Dispose();
        }
    }
}
