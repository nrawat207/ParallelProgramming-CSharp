using System;
using System.Linq;

namespace PLINQExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = Enumerable.Range(1, 10000);

            // Opt in to PLINQ with AsParallel.
            var evenNums = from num in source.AsParallel()
                           where num % 2 == 0
                           select num;
            Console.WriteLine("{0} even numbers out of {1} total",
                              evenNums.Count(), source.Count());
            // The example displays the following output:
            //       5000 even numbers out of 10000 total


            //Other options with PLINQ
            //1. Degree of parallelism
            /* var query = from item in source.AsParallel().WithDegreeOfParallelism(2)
                         where Compute(item) > 42
                             select item;*/
            //2.AsOrdered()
            //var evenNums2 =
            //         from num in source.AsParallel().AsOrdered()
            //         where num % 2 == 0
            //         select num;

            //3. ForAll()
            // var nums = Enumerable.Range(10, 10000);
            // var query =
            //   from num in nums.AsParallel()
            // where num % 10 == 0
            //select num;

            // Process the results as each thread completes
            // and add them to a System.Collections.Concurrent.ConcurrentBag(Of Int)
            // which can safely accept concurrent add operations
            //query.ForAll(e => concurrentBag.Add(Compute(e)));
        }
    }
}
