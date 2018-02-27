using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    public class ProducerConsumer
    {
        private Queue<int> _items;
        private object _lockObject;

        public ProducerConsumer()
        {
            _items = new Queue<int>(5);
            _lockObject = new object();
        }

        public void Produce()
        {
            int count = 1;
            int queueSize;

            while (true)
            {
                lock (_lockObject)
                {
                    queueSize = _items.Count;
                }     

                if (queueSize < 5)
                {
                    Console.WriteLine("Produced: " + count);
                    Thread.Sleep(100);

                    lock (_lockObject)
                    {
                        _items.Enqueue(count);
                    }

                    count++;
                }

            }
        }

        public void Consume()
        {
            while (true)
            {
                Thread.Sleep(1500);
                lock (_lockObject)
                {
                    if (_items.Count != 0)
                    {

                        Console.WriteLine("Consumed: " + _items.Dequeue());
                    }
                }

            }

        }
    }
}
