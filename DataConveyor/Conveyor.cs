using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataConveyor
{
    public class Conveyor
    {
        private List<IBlock> _blocks;

        public Conveyor(params IBlock[] blocks)
        {
            _blocks = blocks.ToList();
        }

        public Conveyor Add(IBlock block) 
        {
            _blocks.Add(block);
            return this;
        }

        public void Run()
        {
            foreach (var block in _blocks)
            {
                ThreadPool.QueueUserWorkItem(block.Run);
            }
        }
    }
}
