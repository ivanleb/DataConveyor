using System;
using System.Threading;

namespace DataConveyor
{
    public abstract class ConsumerConveyorBlock<TInput> : IInputConveyorBlock<TInput>
         where TInput : class
    {
        private readonly Action<TInput> _dataConsumer;
        private IConnector<TInput> _dataSource;
        private WaitHandle _inputPulse;

        protected ConsumerConveyorBlock(Action<TInput> dataConsumer)
        {
            _dataConsumer = dataConsumer;
        }

        public void Connect(IConnector<TInput> inputBlock)
        {
            _dataSource = inputBlock;
            _inputPulse = inputBlock.Pulse;
        }

        public void Run(Object state)
        {
#if DEBUG
            int i = 0;
#endif
            while (true)
            {
#if DEBUG
                Console.WriteLine("Consumer loop: " + i++);
#endif
                _inputPulse.WaitOne();
                Consume();
            }
        }

        private void Consume()
        {
            TInput inputData = _dataSource.Pull();
#if DEBUG
            Console.WriteLine("Consume: " + inputData);
#endif
            if (inputData != default)
                _dataConsumer.Invoke(inputData);
        }

        public void Dispose()
        {
            _dataSource.Dispose();
        }
    }
}
