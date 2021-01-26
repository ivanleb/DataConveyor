using DataConveyor;
using System;

namespace ConveyorBlocks
{
    public class WriterBlock : ConsumerConveyorBlock<String>
    {
        public static WriterBlock Create() 
        {
            Action<String> consumer = d => { Console.WriteLine(d); };
            return new WriterBlock(consumer);
        }
        private WriterBlock(Action<string> dataConsumer) : base(dataConsumer)
        {
        }
    }
}
