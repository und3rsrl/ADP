using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    public class ProducerConsumer
    {
        private Queue<int> _items;
        private object _lockObject;
        private object _condProd;
        private object _condCons;
        ManualResetEvent queueEvent = new ManualResetEvent(); 

        public ProducerConsumer()
        {
            _items = new Queue<int>();
            _lockObject = new object();
            _condProd = new object();
            _condCons = new object();
        }

        public void Produce(string name)
        {
            int count = 1;

            while (true)
            {

                Console.WriteLine(name + "/Produced: " + count);
                Thread.Sleep(50);

                count++;

                lock (_lockObject)
                {
                    while (_items.Count == 5)
                    {   
                        Monitor.Exit(_lockObject);
                        lock (_condProd)
                        {
                            Monitor.Wait(_condProd);
                        }
                    }

                    _items.Enqueue(count);
                }

               

                lock (_condCons)
                {
                    //Monitor.Exit(_condCons);
                    Monitor.Pulse(_condCons);
                }
            }
        }

        public void Consume(string name)
        {
            int count = 0;
            while (true)
            {

                lock (_lockObject)
                {
                    while (_items.Count == 0)
                    {
                        Monitor.Exit(_lockObject);
                        lock (_condCons)
                        {
                            //Monitor.Exit(_condCons);
                            Monitor.Wait(_condCons);
                        }
                    }

                    count = _items.Dequeue();
                }


                
                Console.WriteLine(name + "/Scos din lista: " + count);

                lock (_condProd)
                {
                    Monitor.Pulse(_condProd);
                }

                Thread.Sleep(100);
            }
        }
    }
}
