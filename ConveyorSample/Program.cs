using ConveyorBlocks;
using DataConveyor;
using System;
using System.Threading;

namespace ConveyorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionSpec spec = new ConnectionSpec(10);

            Conveyor conveyor =
                GeneratorBlock.Create()
                .Connect(StringReverserBlock.Create(), spec, new Conveyor())
                .Connect(StringReverserBlock.Create(), spec)
                .Connect(StringReverserBlock.Create(), spec)
                .Connect(WriterBlock.Create(), spec)
                .Run();

            Thread.Sleep(TimeSpan.FromMinutes(60));

        }
    }
}
