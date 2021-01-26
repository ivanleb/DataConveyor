﻿using System;

namespace DataConveyor
{
    public abstract class ConsumerConveyorBlock<TInput> : IInputConveyorBlock<TInput>
         where TInput : class
    {
        private readonly ILog _log;
        private readonly Action<TInput> _dataConsumer;
        private IConnector<TInput> _dataSource;

        IConnector<TInput> IInputConveyorBlock<TInput>.Connector 
        { 
            get => _dataSource; 
            set => _dataSource = value; 
        }

        protected ConsumerConveyorBlock(Action<TInput> dataConsumer, ILog log)
        {
            _dataConsumer = dataConsumer;
            _log = log;
        }

        public void Run(Object state)
        {
            int i = 0;
            while (true)
            {
                _log.Info("Consumer loop: " + i++);
                Consume();
            }
        }

        private void Consume()
        {
            TInput inputData = _dataSource.Pull();

            _log.Info("Consume: " + inputData);

            if (inputData != default)
                _dataConsumer.Invoke(inputData);
        }

        public void Dispose()
        {
            _dataSource.Dispose();
        }
    }
}
