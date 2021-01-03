using DataConveyor;
using System;
using System.Linq;

namespace ConveyorBlocks
{
    public class StringReverserBlock : ConveyorBlock<String, String>
    {
        public static StringReverserBlock Create() 
        {
            Func<String, String> handler = (source) => new String(source.Reverse().ToArray());
            return new StringReverserBlock(handler);
        }
        private StringReverserBlock(Func<string, string> dataHandler) : base(dataHandler)
        {
        }
    }
}
