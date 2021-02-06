# DataConveyor
Conveyor for data processing.

Library for simplification data processing.

Separate blocks for producing, transformation and consuming data can be connected in chain.

First produce block in chain generates data items and sends them to next block. 
Next block can be transform or consume block. 
Transform block receives generated data items from previous block, handles that data and sends them to next block.
Consume block receives data items and doesnt push them further. 

For example produce block can read certain type data from file or from network, transform block can change that type data to data of another type, consume block can write data in file or send to network.

Chain can have several blocks each type, that can be connected between each other according their input and output data types. Blocks can be connected in parallel, in loop. 
One block can be connected with many other blocks.
Transform block has to have at least one block before and at least one block after.
Produce block has to have at least one block after.
Consume block has to have at least one block before.

Connector between blocks can have buffer, by default connector is create without buffer.

## Sample of reading text file line by line, reversing each line and writing reversed lines in text file


```c#
            //create specification for connectors, '100' - is buffer size
            ConnectionSpec spec = new ConnectionSpec(100);
      
            //create source of data - block that read file with 'sourcePath' line by line
            var source = LineReaderBlock.Create(sourcePath)
                        .CreateConveyor(spec);
            
            //create target of data - block that write lines into file with 'targetPath' line by line
            var sink = LineWriterBlock.Create(targetPath);
            
            //create 4 handle-blocks, that reverse lines and connect all that blocks to reader block and to writer block
            var branch1 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            var branch2 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            var branch3 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            var branch4 = source.Connect(StringReverserBlock.Create(), spec).Connect(sink, spec);
            
            //start conveyor
            Conveyor conveyor = branch4.Run();
```
