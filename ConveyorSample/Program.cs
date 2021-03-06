﻿using DataConveyor.Blocks;
using DataConveyor;
using System;
using System.Threading;

namespace ConveyorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException();

            String sourcePath = args[0];
            String targetPath = args[1];

            ConnectionSpec spec = new ConnectionSpec(100);

            var source = LineReaderBlock.Create(sourcePath)
                        .CreateConveyor(spec);

            var sink = LineWriterBlock.Create(targetPath);

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
                        Console.WriteLine(conveyor.PauseResume());
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
