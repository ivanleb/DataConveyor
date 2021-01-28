using DataConveyor;
using System;
using System.IO;

namespace ConveyorBlocks
{
    public class LineReaderBlock : ProducerConveyorBlock<String>
    {
        private StreamReader _stream;
        public static LineReaderBlock Create(string filePath) 
        {
            StreamReader stream = new StreamReader(File.OpenRead(filePath));
            return new LineReaderBlock(stream, () => stream.ReadLine());
        }

        private LineReaderBlock(StreamReader stream, Func<string> func) : base(func)
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
