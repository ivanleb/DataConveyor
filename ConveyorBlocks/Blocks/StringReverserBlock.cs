using DataConveyor;
using System;
using System.Threading;

namespace ConveyorBlocks
{
    public class StringReverserBlock : ConveyorBlock<String, String>
    {
        public static StringReverserBlock Create(ILog logger) 
        {
            Func<String, String> handler = (source) => 
            { 
                Thread.Sleep(TimeSpan.FromSeconds(5));
                return source;// new String(source.Reverse().ToArray()); 
            };

            return new StringReverserBlock(handler, logger);
        }
        private StringReverserBlock(Func<string, string> dataHandler, ILog log) 
            : base(dataHandler, log)
        {
        }
    }
}
