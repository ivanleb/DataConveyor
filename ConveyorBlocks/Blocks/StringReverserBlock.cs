using DataConveyor;
using System;
using System.Linq;
using System.Threading;

namespace DataConveyor.Blocks
{
    public class StringReverserBlock : ConveyorBlock<String, String>
    {
        public static StringReverserBlock Create() 
        {
            Func<String, String> handler = (source) => 
            { 
                //Thread.Sleep(TimeSpan.FromSeconds(1));
                return new String(source.Reverse().ToArray()); 
            };

            return new StringReverserBlock(handler);
        }
        private StringReverserBlock(Func<string, string> dataHandler) 
            : base(dataHandler)
        {
        }
    }
}
