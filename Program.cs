using System;

namespace Single_Server_Queue_Simulation
{
    class Program
    {
        static int RandomNumber(int Range)
        {
            return (new Random()).Next(Range);
        }
        static int MaxValue(int Num1, int Num2)
        {
            if (Num1 > Num2)
                return Num1;
            else return Num2;
        }
        static string ModifyLength(double Num)
        {
            if (Num.ToString().Length > 5)
            {
                return Num.ToString().Substring(0, 5);
            }
            return Num.ToString();
        }

        static void Main(string[] args)
        {
            int TotalCustmer = 100;
            int[] IAT = new int[100];
            int[] ArrivalTime = new int[100];
            int[] RNofST = new int[100];
            int[] ST = new int[100];
            int[] STbegin = new int[100];
            int[] queue = new int[100];
            int[] STend = new int[100];
            int[] TimeinSystem = new int[100];
            int[] IdleTime = new int[100];
            IAT[0] = 0;
            STbegin[0] = 0;
            IdleTime[0] = 0;
            // IAT
            for (int i = 1; i < 100; i++)
            {
                IAT[i] = RandomNumber(8);
            }

            // RNofST
            for (int i = 0; i < 100; i++)
            {
                RNofST[i] = RandomNumber(100);
            }

            // ArrivalTime
            for (int i = 1; i < 100; i++)
            {
                ArrivalTime[i] = IAT[i] + ArrivalTime[i - 1];
            }

            // ST
            for (int i = 0; i < 100; i++)
            {
                if (RNofST[i] <= 10)
                {
                    ST[i] = 1;
                }
                else
                    if (RNofST[i] <= 30)
                {
                    ST[i] = 2;
                }
                else
                        if (RNofST[i] <= 60)
                {
                    ST[i] = 3;
                }
                else
                            if (RNofST[i] <= 85)
                {
                    ST[i] = 4;
                }
                else
                                if (RNofST[i] <= 95)
                {
                    ST[i] = 5;
                }
                else
                {
                    ST[i] = 6;
                }
            }

            // STbegin - STend
            for (int i = 1; i < 100; i++)
            {
                STend[i - 1] = ST[i - 1] + STbegin[i - 1];
                STbegin[i] = MaxValue(STend[i - 1], ArrivalTime[i]);
            }

            // queue
            for (int i = 0; i < 100; i++)
            {
                queue[i] = STbegin[i] - ArrivalTime[i];
            }

            // TimeinSystem
            for (int i = 0; i < 100; i++)
            {
                TimeinSystem[i] = ST[i] + queue[i];
            }

            // IdleTime
            for (int i = 1; i < 100; i++)
            {
                IdleTime[i] = STbegin[i] - STend[i - 1];
            }




            Console.WriteLine("CustmerNo\tIAT\tArrivalTime\tRNofST\tST\tSTbegin\tqueue\tSTend\tTimeinSystem\tIdleTime");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{i + 1}\t\t{IAT[i]}\t\t{ArrivalTime[i]}\t{RNofST[i]}\t{ST[i]}\t{STbegin[i]}\t{queue[i]}\t{STend[i]}\t\t{TimeinSystem[i]}\t\t{IdleTime[i]}");
                Console.WriteLine();
            }


            // Performance.
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Performance : ");
            int IdleTimeCounter = 0;
            int TotalQueue = 0;
            int TotalIdleTime = 0;
            int TotalST = 0;
            int TotalTimeinSystem = 0;
            foreach (var item in IdleTime)
            {
                if (item != 0)
                {
                    IdleTimeCounter++;
                }
            }
            foreach (var item in queue)
            {
                TotalQueue += item;
            }
            foreach (var item in IdleTime)
            {
                TotalIdleTime += item;
            }
            foreach (var item in ST)
            {
                TotalST += item;
            }
            foreach (var item in TimeinSystem)
            {
                TotalTimeinSystem += item;
            }
            Console.WriteLine("Average Time between arrivals = " + ((double)ArrivalTime[ArrivalTime.Length - 1] / (TotalCustmer - 1)).ToString().Substring(0, 4) + " min");
            Console.WriteLine("Averge waiting time of those who wait = " + ModifyLength((double)TotalQueue / IdleTimeCounter) + " min");
            Console.WriteLine("Averge waiting time = " + (double)TotalQueue / TotalCustmer + " min");
            Console.WriteLine("Probability of idle servers = " + ModifyLength((double)TotalIdleTime / ArrivalTime[ArrivalTime.Length - 1]) + " min");
            Console.WriteLine("Average service time = " + (double)TotalST / TotalCustmer + " min");
            Console.WriteLine("Average time a customer spends in the system = " + (double)TotalTimeinSystem / TotalCustmer + " min");



        }
    }
}
