using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConveyor
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
