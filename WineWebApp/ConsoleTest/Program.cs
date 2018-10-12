using System;
using System.Threading;

namespace ConsoleTest
{
    public class Program
    {
        static bool isDone;
        static readonly object myLock = new object();
        static void Main(string[] args)
        {
            //// ex 1
            //Thread myThread = new Thread(Pound);
            //myThread.Start();

            //for (int i = 0; i < 500; i++)
            //{
            //    Console.Write("$");
            //}


            ////ex 2
            //Program process = new Program();

            //Thread myThread2 = new Thread(Done);
            //myThread2.Start();

            //Done();


            // ex 3

            Thread myThread3 = new Thread(Satan);
            myThread3.Start();

            myThread3.Join();

            Console.WriteLine("thread finished");

            Console.ReadKey();
        }

        // multithread
        public static void Pound()
        {
            for (int i = 0; i < 500; i++)
            {
                Console.Write("£");
                
            }
        }

        // lock strategy
        public static void Done()
        {
            lock (myLock)
            {
                if (isDone == false)
                {
                    Console.WriteLine("done");

                    isDone = true;
                }
            }
           
        }

        // joined strategy - a thread waits to be finished for another to start
        public static void Satan()
        {
            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine("666");
            }
        }
    }
}
