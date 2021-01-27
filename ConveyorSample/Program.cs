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
                .CreateConveyor(spec)
                .Connect(StringReverserBlock.Create(), spec)
                .Connect(StringReverserBlock.Create(), spec)
                .Connect(StringReverserBlock.Create(), spec)
                .Connect(WriterBlock.Create(), spec)
                .Run();


            new Thread(()=> 
            {
                while (true)
                {
                    var keyInfo = Console.ReadKey();
                    if (keyInfo.KeyChar == 'p')
                        conveyor.PauseResume();
                    else if (keyInfo.KeyChar == 's')
                    {
                        conveyor.Stop();
                        break;
                    }
                }
            }).Start();

        }
    }
}
