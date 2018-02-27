using System;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer pc = new ProducerConsumer();
            Task p = Task.Factory.StartNew(() => pc.Produce());
            Task c = Task.Factory.StartNew(() => pc.Consume());
            Task.WaitAll(p, c);
            Console.ReadKey();
        }
    }
}
