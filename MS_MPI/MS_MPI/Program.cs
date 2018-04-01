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

                if (comm.Rank == 0)
                {

                    for (int processPart = 0; processPart < comm.Size; processPart++)
                    {
                        for (int j = processPart * (numbers.Length / comm.Size);
                            j < (processPart + 1) * (numbers.Length / comm.Size);
                            j++)
                        {
                            if (processPart == 0)
                            {
                                sum += numbers[j];
                            }
                            else
                            {
                                List<int> myPart = new List<int>();
                                myPart.Add(numbers[j]);

                                comm.Send(myPart, processPart, 0);
                            }
                        }
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

                    comm.Send(mySum, 0, 0);
                }

            }
        }
    }
}