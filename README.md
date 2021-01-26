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
