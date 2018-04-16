using MPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MpiNetHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int sum = 0;
            
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = MPI.Communicator.world;
                int division = numbers.Length / comm.Size;

                if (comm.Rank == 0)
                {

                    for (int processPart = comm.Size - 1; processPart >= 0; processPart--)
                    {

                        List<int> myPart = new List<int>();

                        int inequal = 0;

                        if (processPart == comm.Size - 1)
                            inequal = numbers.Length % comm.Size;

                        for (int j = processPart * division; j < ((processPart + 1) * division) + inequal; j++)
                        {
                            if (processPart == 0)
                            {
                                sum += numbers[j];
                            }
                            else
                            {
                                myPart.Add(numbers[j]);
                            }
                        }

                        if(processPart != 0)
                            comm.Send(myPart, processPart, 0);

                        myPart.Clear();
                    }

                    for (int i = 1; i < comm.Size; i++)
                    {
                        sum += comm.Receive<int>(i, 0);
                    }

                    Console.WriteLine("Sum is: " + sum);
                }
                else
                {
                    List<int> myList = comm.Receive<List<int>>(0, 0);

                    int mySum = myList.Sum();

                    foreach (var i in myList)
                    {
                        Console.WriteLine(comm.Rank + "|" + i);
                    }
                    Console.WriteLine(comm.Rank + "|" + mySum);

                    comm.Send(mySum, 0, 0);
                }

            }
        }
    }
}