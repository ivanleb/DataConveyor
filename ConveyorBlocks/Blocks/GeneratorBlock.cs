using DataConveyor;
using System;

namespace ConveyorBlocks
{
    public class GeneratorBlock : ProducerConveyorBlock<String>
    {
        public static GeneratorBlock Create() 
        {
            Func<string> dataGenerator = () => 
            {
                return $"date:{DateTime.Now}";
            };

            return new GeneratorBlock(dataGenerator, TimeSpan.FromSeconds(1));
        }

        private GeneratorBlock(Func<string> dataGenerator, TimeSpan time) : base(dataGenerator, time)
        {
        }
    }
}
