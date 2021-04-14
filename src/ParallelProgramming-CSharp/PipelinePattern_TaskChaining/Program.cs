using System;
using System.Threading;
using System.Threading.Tasks;

namespace PipelinePattern_TaskChaining
{
    class Program
    {
        static void Main(string[] args)
        {

            Task<int> t1 = Task.Factory.StartNew(() =>
            { return CreateUser(); });

            var t2 = t1.ContinueWith((antecedent) =>
            { return InitiateWorkflow(antecedent.Result); });
            var t3 = t2.ContinueWith((antecedant) =>
            { return SendEmail(antecedant.Result); });

            Console.Read();


        }

        public static int CreateUser()
        {
            //Create user, passing hardcoded user ID as 1 
            Thread.Sleep(1000);
            Console.WriteLine("User created");
            return 1;
        }

        public static int InitiateWorkflow(int userId)
        {
            //Initiate Workflow 
            Thread.Sleep(1000);
            Console.WriteLine("Workflow initiates");

            return userId;
        }

        public static int SendEmail(int userId)
        {
            //Send email 
            Thread.Sleep(1000);
            Console.WriteLine("Email sent");

            return userId;
        }
    }
}
