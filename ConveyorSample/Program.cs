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


            var chain1 = LineReaderBlock.Create(@"C:\Users\il\Desktop\vim\VimService.cs")
            .CreateConveyor(spec)
            //.Connect(StringReverserBlock.Create(), spec)
            //.Connect(StringReverserBlock.Create(), spec)
            .Connect(StringReverserBlock.Create(), spec);

            //var chain3 = chain1.Connect(WriterBlock.Create(), spec);

            var chain3 = chain1.Connect(LineWriterBlock.Create(@"C:\Users\il\Desktop\vim\VimService_reversed.cs"), spec);

            Conveyor conveyor = chain3.Run();


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
