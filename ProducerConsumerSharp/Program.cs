using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int bufferCapacity = 10;
        int totalItems = 60;
        int numProducers = 8;
        int numConsumers = 2;

        Buffer buffer = new Buffer(bufferCapacity);

        Thread[] producers = new Thread[numProducers];
        Thread[] consumers = new Thread[numConsumers];

        int itemsPerProducer = totalItems / numProducers;
        int itemsPerConsumer = totalItems / numConsumers;

        for (int i = 0; i < numProducers; i++)
        {
            int id = i + 1;
            Producer producer = new Producer(buffer, itemsPerProducer, id);
            producers[i] = new Thread(new ThreadStart(producer.Run));
            producers[i].Start();
        }

        for (int i = 0; i < numConsumers; i++)
        {
            int id = i + 1;
            Consumer consumer = new Consumer(buffer, itemsPerConsumer, id);
            consumers[i] = new Thread(new ThreadStart(consumer.Run));
            consumers[i].Start();
        }

        foreach (Thread t in producers) t.Join();
        foreach (Thread t in consumers) t.Join();

        Console.WriteLine("✅ Усі потоки завершено. Програма завершила роботу.");
    }
}
