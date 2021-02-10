using System;

namespace DataConveyor
{
    public class ProcessingBlock<TInput, TOutput> : ConveyorBlock<TInput, TOutput>
    {
        public String Name { get; set; }
        public ProcessingBlock(String name, Func<TInput, TOutput> dataHandler) : base(dataHandler)
        {
            Name = name;
        }
    }
}
