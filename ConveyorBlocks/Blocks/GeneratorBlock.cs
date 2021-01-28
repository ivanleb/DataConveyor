using DataConveyor;
using System;

namespace ConveyorBlocks
{
    public class GeneratorBlock : ProducerConveyorBlock<String>
    {
        private static Int32 counter;
        public static GeneratorBlock Create() 
        {
            Func<(string, bool)> dataGenerator = () => 
            {
                return (counter++.ToString(), counter > 100000);
            };

            return new GeneratorBlock(dataGenerator);
        }

        private GeneratorBlock(Func<(string, bool)> dataGenerator) : base(dataGenerator)
        {
        }
    }
}
