using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProducerComsumerPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxColl = 10;
            var blockingCollection = new BlockingCollection<int>(maxColl);
            var taskFactory = new TaskFactory(TaskCreationOptions.LongRunning,
            TaskContinuationOptions.None);

            Task producer = taskFactory.StartNew(() =>
            {
                if (blockingCollection.Count <= maxColl)
                {
                    int imageID = ReadImageFromDB();
                    blockingCollection.Add(imageID);
                    blockingCollection.CompleteAdding();
                }
            });


            Task consumer = taskFactory.StartNew(() =>
            {
                while (!blockingCollection.IsCompleted)
                {
                    try
                    {
                        int imageID = blockingCollection.Take();
                        ProcessImage(imageID);
                    }
                    catch (Exception ex)
                    {
                        //Log exception 
                    }
                }
            });

            Console.Read();

        }

        public static int ReadImageFromDB()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Image is read");
            return 1;
        }

        public static void ProcessImage(int imageID)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Image is processed");

        }
    }
}
