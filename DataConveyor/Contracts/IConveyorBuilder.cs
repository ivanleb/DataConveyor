using System;

namespace DataConveyor
{
    public interface IConveyorBuilder
    {
        IConveyorBuilder Start<TOutput>(IOutputConveyorBlock<TOutput> block) where TOutput : class;
        Boolean TryAdd<TInput, TOutput>(IConveyorBlock<TInput, TOutput> block) where TInput : class where TOutput : class;
        Boolean TryInsert<TInput, TOutput>(IOutputConveyorBlock<TInput> blockBefore, IConveyorBlock<TInput, TOutput> insertingBlock, IInputConveyorBlock<TOutput> blockAfter) where TInput : class where TOutput : class;
        Boolean End<TInput>(IInputConveyorBlock<TInput> block) where TInput : class;
        Conveyor Build();
    }
}
