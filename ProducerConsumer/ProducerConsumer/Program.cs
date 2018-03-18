using System;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer pc = new ProducerConsumer();
            Task p = Task.Factory.StartNew(() => pc.Produce("Producer 1"));
            Task c = Task.Factory.StartNew(() => pc.Consume("Consumer 1"));
            Task.WaitAll(p, c);
            Console.ReadKey();
        }
    }
}
