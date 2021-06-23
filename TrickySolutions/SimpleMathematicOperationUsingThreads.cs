using System;
using System.Threading;

//Create two threads, one thread should take integer input and second thread should print sqrt output.
namespace ConsoleApp3
{
    class Program
    {
        static int i;
        static readonly string _lockvar = String.Empty;

        static void Main(string[] args)
        {
            Thread t = new Thread(() => ReadInputAndPrint());
            Thread t1 = new Thread(() => writeOutput());
            t.Start();
            t1.Start();
            t.Join();
            t1.Join();
            Console.ReadLine();
        }

        public static void ReadInputAndPrint()
        {
            lock(_lockvar)
            {
                Console.WriteLine("Enter the input:");
                Int32.TryParse(Console.ReadLine(), out i);
            }
        }

        public static void writeOutput()
        {
            lock (_lockvar)
            {
                Console.WriteLine("Sqrt Output:" + Math.Sqrt(i));
            }
        }
    }
}
