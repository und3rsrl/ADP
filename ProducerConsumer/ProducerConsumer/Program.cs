using System;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer pc = new ProducerConsumer();
            //Task p = Task.Factory.StartNew(() => pc.Produce("Producer 1"));
            //Task p1 = Task.Factory.StartNew(() => pc.Produce("Producer 2"));
            //Task c = Task.Factory.StartNew(() => pc.Consume("Consumer 1"));
            //Task c1 = Task.Factory.StartNew(() => pc.Consume("Consumer 2"));
            //Task.WaitAll(p, c);

            Thread p1 = new Thread(() => pc.Produce("Producer 1"));
            Thread p2 = new Thread(() => pc.Produce("Producer 2"));
            Thread c1 = new Thread(() => pc.Consume("Consumer 1"));
            Thread c2 = new Thread(() => pc.Consume("Consumer 2"));
            p1.Start();
            p2.Start();
            c1.Start();
            c2.Start();
            p1.Join();
            p2.Join();
            c1.Join();
            c2.Join();
            Console.ReadKey();
        }
    }
}
