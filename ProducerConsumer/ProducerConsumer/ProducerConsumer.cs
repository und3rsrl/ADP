using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    public class ProducerConsumer
    {
        Semaphore semaphoreFree = new Semaphore(5, 5);
        Semaphore semaphoreFull = new Semaphore(0, 5);
        private Queue<int> _items;
        private object _lockObject;

        public ProducerConsumer()
        {
            _items = new Queue<int>();
            _lockObject = new object();
        }

        public void Produce(string name)
        {
            int count = 1;
            int queueSize;
            bool produced = false;

            while (true)
            {
                
                if (produced == false)
                {
                    Console.WriteLine(name + "/Produced: " + count);
                    Thread.Sleep(50);
                    produced = true;
                    count++;
                }
                semaphoreFree.WaitOne();

                lock (_lockObject)
                {
                    if (_items.Count < 5)
                    {
                        _items.Enqueue(count);
                        produced = false;
                        Console.WriteLine(name + "/Pus in lista: " + count);
                    }    
                }

                semaphoreFull.Release();
            }
        }

        public void Consume(string name)
        {
            int count = 0;
            while (true)
            {
                semaphoreFull.WaitOne();

                lock (_lockObject)
                {
                    if (_items.Count != 0)
                    {
                        count = _items.Dequeue();
                        Console.WriteLine(name + "/Scos din lista: " + count);
                    }
                }
                semaphoreFree.Release(1);
                Thread.Sleep(1000);   
            }
        }
    }
}
