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
            ILog logger = new ConsoleLogger(onlyDebugLogging: true);
            GeneratorBlock generator = GeneratorBlock.Create(logger);
            StringReverserBlock middleHandler = StringReverserBlock.Create(logger);
            StringReverserBlock middleHandler1 = StringReverserBlock.Create(logger);
            WriterBlock writer = WriterBlock.Create(logger);

            ConnectionSpec spec = new ConnectionSpec(10, logger);
            IConnectionMaker connectionMaker = new ConnectionMaker(logger, spec);
            IConveyorBuilder builder = new ConveyorBuilder(connectionMaker).Start(generator);

            if (builder.TryAdd(middleHandler) && builder.End(writer) && builder.TryInsert(generator, middleHandler1, writer))
            {
                var conveyor = builder.Build();
                conveyor.Run();
            }
            Thread.Sleep(TimeSpan.FromMinutes(60));
        }
    }
}
