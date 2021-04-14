using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.Created task will run asynchronously can run on multiple threads automatically depending on the CPU core.
            Task t = Task.Run(() => ExecuteBackgroundTask(5000));
            
            //2. Task cancellation example
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //3. Task Progress Reporting example
            var progressHandler = new Progress<string>(value =>
            {
                Console.WriteLine(value);
            });

            var progress = progressHandler as IProgress<string>;

            Task t2 = Task.Factory.StartNew(() => SaveFileAsync("", new byte[100], token, progress));
            tokenSource.Cancel(); //This will cancel the task t2

            Task.WaitAll(t, t2);
        }

        public static void ExecuteBackgroundTask(int millis)
        {
            Thread.Sleep(millis);
            Console.WriteLine("Background task completed....");

        }

        static Task<int> SaveFileAsync(string path, byte[] fileBytes, CancellationToken cancellationToken, IProgress<string> progress)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                progress.Report("Cancellation is called");
                Console.WriteLine("Cancellation is requested...");
                //cancellationToken.ThrowIfCancellationRequested();
            }
            progress.Report("Saving File");
            //Do some file save operation 
            Thread.Sleep(10000);
            progress.Report("File Saved");
            return Task.FromResult<int>(0);
        }
    }
}
