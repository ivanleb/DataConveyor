using DataConveyor;
using System;

namespace ConveyorBlocks
{
    public class GeneratorBlock : ProducerConveyorBlock<String>
    {
        private static Int32 counter;
        public static GeneratorBlock Create(ILog logger) 
        {
            Func<string> dataGenerator = () => 
            {
                return counter++.ToString();
            };

            return new GeneratorBlock(dataGenerator, logger);
        }

        private GeneratorBlock(Func<string> dataGenerator, ILog logger) : base(dataGenerator, logger)
        {
        }
    }
}
