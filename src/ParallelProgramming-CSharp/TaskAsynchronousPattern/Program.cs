using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskAsynchronousPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = ExecuteLongRunningOperationAsync(5000);
            Console.WriteLine("Called ExecuteLongRunningOperationAsync method,now waiting for it to complete"); 
          
            t.Wait();
            Console.Read();
        }

        /// <summary>
        /// This is method will be run asynchronously as complier identify it using 'async; keyword
        /// </summary>
        /// <param name="millis"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteLongRunningOperationAsync(int millis)
        {
            Task t = Task.Factory.StartNew(() => RunLoopAsync(millis));
            await t;
            Console.WriteLine("Executed RunLoopAsync method");
            return 0;
        }

        public static void RunLoopAsync(int millis)
        {
            Console.WriteLine("Inside RunLoopAsync method");
            for (int i = 0; i < millis; i++)
            {
                Debug.WriteLine($"Counter = {i}");
            }
            Console.WriteLine("Exiting RunLoopAsync method");
        }
    }
}
