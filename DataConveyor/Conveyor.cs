using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataConveyor
{
    public class Conveyor
    {
        private List<IBlock> _blocks;

        public Conveyor(List<IBlock> blocks)
        {
            _blocks = blocks;
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
