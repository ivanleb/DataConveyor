using DataConveyor;
using System;

namespace ConveyorBlocks
{
    public class GeneratorBlock : ProducerConveyorBlock<String>
    {
        private static Int32 counter;
        public static GeneratorBlock Create() 
        {
            Func<string> dataGenerator = () => 
            {
                return counter++.ToString();
            };

            return new GeneratorBlock(dataGenerator);
        }

        private GeneratorBlock(Func<string> dataGenerator) : base(dataGenerator)
        {
        }
    }
}
