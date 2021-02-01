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
            ConnectionSpec spec = new ConnectionSpec(100);


            var source = LineReaderBlock.Create(@"C:\Users\il\Desktop\vim\VimService1.cs")
                        .CreateConveyor(spec);

            var sink = LineWriterBlock.Create(@"C:\Users\il\Desktop\vim\VimService1_reversed.cs");

            var branch1 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            var branch2 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            var branch3 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            var branch4 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);

            Conveyor conveyor = branch4.Run();


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
