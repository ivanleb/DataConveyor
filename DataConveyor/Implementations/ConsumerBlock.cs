using System;

namespace DataConveyor
{
    public class ConsumerBlock<TInput> : ConsumerConveyorBlock<TInput>
    {
        public String Name { get; }
        public ConsumerBlock(String name, Action<TInput> dataConsumer) : base(dataConsumer)
        {
            Name = name;
        }
    }
}
