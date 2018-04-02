using MPI;
using System;

namespace MS_MPI_2
{
    class Program
    {
        static void Main(string[] args)
        {   
            Random random = new Random();

            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = MPI.Communicator.world;

                int master = -1;

                int myNumber = random.Next(10);

                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < comm.Size; i++)
                    {
                        if (i == comm.Rank)
                            continue;

                        comm.Send(myNumber, i, 0);
                        int receivedNumber = comm.Receive<int>(i, 0);

                        if (receivedNumber > myNumber)
                        {
                            if (receivedNumber == myNumber)
                            {
                                if (comm.Rank > i)
                                    master = comm.Rank;
                                else
                                {
                                    master = i;
                                }
                            }
                            else
                            {
                                master = i;
                            }
                        }
                        else
                        {
                            master = comm.Rank;
                        }
                    }
                }

                Console.WriteLine("Proces " + comm.Rank + ": " + master + "/ My number : " + myNumber);
            }
        }
    }
}
