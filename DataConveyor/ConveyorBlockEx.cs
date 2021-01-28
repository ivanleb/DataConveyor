using System;

namespace DataConveyor
{
    public static class ConveyorBlockEx
    {
        internal static Boolean Connect<TInput>(this IInputConveyorBlock<TInput> inputBlock, IOutputConveyorBlock<TInput> outputBlock, ConnectionSpec spec)
        {
            if (inputBlock.Connector == null && outputBlock.Connector == null)
            {
                inputBlock.Connector = new Connector<TInput>(spec.MaxBufferSize);

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

        internal static Boolean Connect<TOutput>(this IOutputConveyorBlock<TOutput> outputBlock, IInputConveyorBlock<TOutput> inputBlock, ConnectionSpec spec)
        {
            if (outputBlock.Connector == null && inputBlock.Connector == null)
            {
                outputBlock.Connector = new Connector<TOutput>(spec.MaxBufferSize);

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

        public static (IConveyorBlock<TInput, TOutput> Block, Boolean Success, Conveyor Conveyor) Connect<TInput, TOutput>(this (IOutputConveyorBlock<TInput> Block, Boolean PreviousSuccess, Conveyor Conveyor) firstBlock, IConveyorBlock<TInput, TOutput> secondBlock, ConnectionSpec spec)
        {
            firstBlock.Conveyor.Add(firstBlock.Block);
            Boolean success = firstBlock.Block.Connect(secondBlock, spec);

            if (success)
                firstBlock.Conveyor.Add(secondBlock);

            return (secondBlock, success, firstBlock.Conveyor);
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

        public static (IInputConveyorBlock<TOutput> Block, Boolean Success, Conveyor Conveyor) Connect<TInput, TOutput>(this (IConveyorBlock<TInput, TOutput> Block, Boolean PreviousSuccess, Conveyor Conveyor) firstBlock, IInputConveyorBlock<TOutput> secondBlock, ConnectionSpec spec)
        {
            Boolean success = firstBlock.PreviousSuccess
                ? firstBlock.Block.Connect(secondBlock, spec)
                : firstBlock.PreviousSuccess;

            if (success)
                firstBlock.Conveyor.Add(secondBlock);

            return (secondBlock, success, firstBlock.Conveyor);
        }

        public static (IOutputConveyorBlock<TInput> Block, Boolean PreviousSuccess, Conveyor Conveyor) CreateConveyor<TInput>(this IOutputConveyorBlock<TInput> startBlock, ConnectionSpec spec)
        {
            return (startBlock, true, new Conveyor());
        }

        public static Conveyor Run<TOutput>(this (IInputConveyorBlock<TOutput> Block, Boolean Success, Conveyor Conveyor) endBlock)
        {
            endBlock.Conveyor.Run();
            return endBlock.Conveyor;
        }
    }
}
