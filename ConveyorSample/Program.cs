using ConveyorBlocks;
using DataConveyor;
using System;

namespace ConveyorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratorBlock generator = GeneratorBlock.Create();
            StringReverserBlock middleHandler = StringReverserBlock.Create();
            WriterBlock writer = WriterBlock.Create();

            IConveyorBuilder builder = new ConveyorBuilder().Start(generator);
            if (builder.TryAdd(middleHandler))
            {
                var conveyor = builder.End(writer);
                conveyor.Run();
            }
            while (true)
            {

            }
        }
    }
}
