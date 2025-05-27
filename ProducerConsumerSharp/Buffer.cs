using System;
using System.Collections.Generic;
using System.Threading;

public class Buffer
{
    private readonly Queue<int> queue = new Queue<int>();
    private readonly int capacity;

    private readonly SemaphoreSlim emptySlots;
    private readonly SemaphoreSlim fullSlots;
    private readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1); // для взаємовиключення

    public Buffer(int capacity)
    {
        this.capacity = capacity;
        this.emptySlots = new SemaphoreSlim(capacity, capacity);
        this.fullSlots = new SemaphoreSlim(0, capacity);
    }

    public void Produce(int item, int producerId)
    {
        if (emptySlots.CurrentCount == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"!Сховище заповнене! Виробник #{producerId} чекає...");
            Console.ResetColor();
        }

        emptySlots.Wait();
        mutex.Wait();

        queue.Enqueue(item);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Виробник #{producerId} створив: {item} | Буфер: {queue.Count}/{capacity}");
        Console.ResetColor();

        mutex.Release();
        fullSlots.Release();
    }

    public int Consume(int consumerId)
    {
        if (fullSlots.CurrentCount == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"!Сховище порожнє! Споживач #{consumerId} чекає...");
            Console.ResetColor();
        }

        fullSlots.Wait();
        mutex.Wait();

        int item = queue.Dequeue();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Споживач #{consumerId} спожив: {item} | Буфер: {queue.Count}/{capacity}");
        Console.ResetColor();

        mutex.Release();
        emptySlots.Release();

        return item;
    }
}
