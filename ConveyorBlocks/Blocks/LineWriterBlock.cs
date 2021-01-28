﻿using DataConveyor;
using System;
using System.IO;

namespace ConveyorBlocks
{
    public class LineWriterBlock : ConsumerConveyorBlock<String>
    {
        private StreamWriter _stream;
        public static LineWriterBlock Create(string filePath) 
        {
            StreamWriter stream = new StreamWriter(File.OpenWrite(filePath));
            Action<string> dataConsumer = s => 
            {
                stream.WriteLine();
            };
            return new LineWriterBlock(stream, dataConsumer);
        }


        private LineWriterBlock(StreamWriter stream, Action<string> dataConsumer) 
            : base(dataConsumer)
        {
            _stream = stream;
        }

        public void Close() 
        {
            _stream.Close();
            Dispose();
        }
    }
}