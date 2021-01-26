using System;

namespace DataConveyor
{
    public static class ConveyorBlockEx
    {
        public static Boolean Connect<TInput>(this IInputConveyorBlock<TInput> inputBlock, IOutputConveyorBlock<TInput> outputBlock, ConnectionSpec spec)
            where TInput : class
        {
            if (inputBlock.Connector == null && outputBlock.Connector == null)
            {
                inputBlock.Connector = new Connector<TInput>(spec.MaxBufferSize, spec.Log);

                return outputBlock.Connect(inputBlock, spec);
            }

            if (inputBlock.Connector == null && outputBlock.Connector != null)
            {
                inputBlock.Connector = outputBlock.Connector;
                return true;
            }

            if (inputBlock.Connector != null && outputBlock.Connector == null)
            {
                outputBlock.Connector = inputBlock.Connector;
                return true; ;
            }
            return (inputBlock.Connector == outputBlock.Connector);
        }

        public static Boolean Connect<TOutput>(this IOutputConveyorBlock<TOutput> outputBlock, IInputConveyorBlock<TOutput> inputBlock, ConnectionSpec spec)
            where TOutput : class
        {
            if (outputBlock.Connector == null && inputBlock.Connector == null)
            {
                outputBlock.Connector = new Connector<TOutput>(spec.MaxBufferSize, spec.Log);

                return inputBlock.Connect(outputBlock, spec);
            }

            if (outputBlock.Connector == null && inputBlock.Connector != null)
            {
                outputBlock.Connector = inputBlock.Connector;
                return true;
            }

            if (outputBlock.Connector != null && inputBlock.Connector == null)
            {
                inputBlock.Connector = outputBlock.Connector;
                return true;
            }
            return (outputBlock.Connector == inputBlock.Connector);
        }

        public static (IConveyorBlock<TMiddle, TOutput> Block, Boolean Success, Conveyor Conveyor) Connect<TInput, TMiddle, TOutput>(this (IConveyorBlock<TInput, TMiddle> Block, Boolean PreviousSuccess, Conveyor Conveyor) firstBlock, IConveyorBlock<TMiddle, TOutput> secondBlock, ConnectionSpec spec)
                    where TInput : class
                    where TMiddle : class
                    where TOutput : class
        {
            Boolean success = firstBlock.PreviousSuccess 
                ? firstBlock.Block.Connect(secondBlock, spec) 
                : firstBlock.PreviousSuccess;

            if (success)
                firstBlock.Conveyor.Add(secondBlock);

            return (secondBlock, success, firstBlock.Conveyor);

        }

        public static (IConveyorBlock<TInput, TOutput> Block, Boolean Success, Conveyor Conveyor) Connect<TInput, TOutput>(this IOutputConveyorBlock<TInput> firstBlock, IConveyorBlock<TInput, TOutput> secondBlock, ConnectionSpec spec, Conveyor conveyor)
            where TInput : class
            where TOutput : class
        {
            conveyor.Add(firstBlock);
            Boolean success = firstBlock.Connect(secondBlock, spec);

            if (success)
                conveyor.Add(secondBlock);

            return (secondBlock, success, conveyor);
        }

        public static (IInputConveyorBlock<TOutput> Block, Boolean Success, Conveyor Conveyor) Connect<TInput, TOutput>(this (IConveyorBlock<TInput, TOutput> Block, Boolean PreviousSuccess, Conveyor Conveyor) firstBlock, IInputConveyorBlock<TOutput> secondBlock, ConnectionSpec spec)
            where TInput : class
            where TOutput : class
        {
            Boolean success = firstBlock.PreviousSuccess
                ? firstBlock.Block.Connect(secondBlock, spec)
                : firstBlock.PreviousSuccess;

            if (success)
                firstBlock.Conveyor.Add(secondBlock);

            return (secondBlock, success, firstBlock.Conveyor);
        }

        public static Conveyor Run<TOutput>(this (IInputConveyorBlock<TOutput> Block, Boolean Success, Conveyor Conveyor) endBlock)
            where TOutput : class
        {
            endBlock.Conveyor.Run();
            return endBlock.Conveyor;
        }
    }
}
