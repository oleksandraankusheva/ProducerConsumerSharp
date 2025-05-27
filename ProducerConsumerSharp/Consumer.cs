using System;
using System.Threading;

public class Consumer
{
    private readonly Buffer buffer;
    private readonly int itemsToConsume;
    private readonly int id;

    public Consumer(Buffer buffer, int itemsToConsume, int id)
    {
        this.buffer = buffer;
        this.itemsToConsume = itemsToConsume;
        this.id = id;
    }

    public void Run()
    {
        for (int i = 0; i < itemsToConsume; i++)
        {
            buffer.Consume(id);
            Thread.Sleep(150); // імітація часу на споживання
        }
    }
}
