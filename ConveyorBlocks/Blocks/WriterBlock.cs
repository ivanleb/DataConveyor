using DataConveyor;
using System;

namespace ConveyorBlocks
{
    public class WriterBlock : ConsumerConveyorBlock<String>
    {
        public static WriterBlock Create(ILog log) 
        {
            Action<String> consumer = d => { Console.WriteLine(d); };
            return new WriterBlock(consumer, log);
        }
        private WriterBlock(Action<string> dataConsumer, ILog log) : base(dataConsumer, log)
        {
        }
    }
}
