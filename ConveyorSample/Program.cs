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
            //ILog logger = new ConsoleLogger(onlyDebugLogging: true); ILog logger = new FileLogger("logfile.txt", onlyDebugLogging: true)
            using (ILog logger = new ConsoleLogger(onlyDebugLogging: true))
            {
                ConnectionSpec spec = new ConnectionSpec(10, logger);

                Conveyor conveyor =
                    GeneratorBlock.Create(logger)
                    .Connect(StringReverserBlock.Create(logger), spec, new Conveyor())
                    .Connect(StringReverserBlock.Create(logger), spec)
                    .Connect(StringReverserBlock.Create(logger), spec)
                    .Connect(WriterBlock.Create(logger), spec)
                    .Run();

                Thread.Sleep(TimeSpan.FromMinutes(60));
            }
    }
}
}
