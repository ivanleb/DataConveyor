using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConveyor
{
    public class ConveyorBuilder : IConveyorBuilder
    {
        private Boolean _hasStartBlock;
        private Boolean _hasEndBlock;
        
        private LinkedList<IBlock> _blocks = new LinkedList<IBlock>();
        private IConnectionMaker _connectionMaker;

        public ConveyorBuilder()
        {
            _connectionMaker = new ConnectionMaker();
        }

        public IConveyorBuilder Start<TOutput>(IOutputConveyorBlock<TOutput> block)
        {
            if (_hasStartBlock)
                throw new Exception("Conveyor has start block yet");

            _blocks.AddFirst(block);
            _hasStartBlock = true;

            return this;
        }

        public Boolean TryAdd<TInput, TOutput>(IConveyorBlock<TInput, TOutput> block)
        {
            if (!_hasStartBlock)
                throw new Exception("Conveyor has not start block yet");

            return AddBlockAtTheEnd(block);
        }

        private Boolean AddBlockAtTheEnd<TInput>(IInputConveyorBlock<TInput> block)
        {
            if (_blocks.Last() is IOutputConveyorBlock<TInput> previousBlock)
            {
                _connectionMaker.Connect(block, previousBlock);
                _blocks.AddLast(block);
                return true;
            }

            return false;
        }

        public Conveyor End<TInput>(IInputConveyorBlock<TInput> block)
        {
            if (_hasEndBlock)
                throw new Exception("Conveyor has end block yet");
            if (_blocks.Count < 2)
                throw new Exception("Conveyor cannot be so short");

            _hasEndBlock = AddBlockAtTheEnd(block);
            return new Conveyor(_blocks.ToList());
        }
    }
}
