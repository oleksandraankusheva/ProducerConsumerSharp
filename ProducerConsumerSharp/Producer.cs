using System;
using System.Threading;

public class Producer
{
    private readonly Buffer buffer;
    private readonly int itemsToProduce;
    private readonly int id;

    public Producer(Buffer buffer, int itemsToProduce, int id)
    {
        this.buffer = buffer;
        this.itemsToProduce = itemsToProduce;
        this.id = id;
    }

    public void Run()
    {
        for (int i = 1; i <= itemsToProduce; i++)
        {
            buffer.Produce(i, id);
            Thread.Sleep(100); // імітація часу на виробництво
        }
    }
}
