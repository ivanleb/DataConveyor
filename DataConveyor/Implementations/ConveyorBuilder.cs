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

        public ConveyorBuilder(IConnectionMaker connectionMaker)
        {
            _connectionMaker = connectionMaker;
        }

        public IConveyorBuilder Start<TOutput>(IOutputConveyorBlock<TOutput> block)
            where TOutput : class
        {
            if (_hasStartBlock)
                throw new Exception("Conveyor has start block yet");

            _blocks.AddFirst(block);
            _hasStartBlock = true;

            return this;
        }

        public Boolean TryAdd<TInput, TOutput>(IConveyorBlock<TInput, TOutput> block)
            where TInput : class
            where TOutput : class
        {
            if (!_hasStartBlock)
                throw new Exception("Conveyor has not start block yet");

            return TryAddBlockAtTheEnd(block);
        }

        public Boolean TryInsert<TInput, TOutput>(IOutputConveyorBlock<TInput> blockBefore, IConveyorBlock<TInput, TOutput> insertingBlock, IInputConveyorBlock<TOutput> blockAfter)
            where TInput : class
            where TOutput : class
        {
            bool blockBeforeExists = false;
            bool blockAfterExists = false;
            var block = _blocks.First;
            while (block != null)
            {
                if (block.Value == blockBefore) blockBeforeExists = true;
                if (block.Value == blockAfter) blockAfterExists = true;
                if (blockAfterExists && blockBeforeExists) break;
                block = block.Next;
            }
            if (!(blockAfterExists && blockBeforeExists)) return false;
            _connectionMaker.Connect(blockBefore, insertingBlock)
                            .Connect(insertingBlock, blockAfter);
            _blocks.AddLast(insertingBlock);
            return true;
        }

        private Boolean TryAddBlockAtTheEnd<TInput>(IInputConveyorBlock<TInput> block)
            where TInput : class
        {
            if (_blocks.Last() is IOutputConveyorBlock<TInput> previousBlock)
            {
                _connectionMaker.Connect(previousBlock, block);
                _blocks.AddLast(block);
                return true;
            }

            return false;
        }

        public Boolean End<TInput>(IInputConveyorBlock<TInput> block)
            where TInput : class
        {
            if (_hasEndBlock)
                throw new Exception("Conveyor has end block yet");
            if (_blocks.Count < 2)
                throw new Exception("Conveyor cannot be so short");

            _hasEndBlock = TryAddBlockAtTheEnd(block);
            return _hasEndBlock;

        }

        public Conveyor Build() 
        {
            if (_hasEndBlock)
                return new Conveyor(_blocks.ToList());
            else
                throw new Exception("Conveyor has not end block");
        }
    }
}
