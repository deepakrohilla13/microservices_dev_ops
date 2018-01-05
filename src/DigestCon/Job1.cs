using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace DigestCon
{
    public class Job1 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await JobDetails();
        }

        Task JobDetails()
        {
            Thread.Sleep(3*1000);
            Console.WriteLine("||||||||||||||||");
            return Task.CompletedTask;
        }
    }
}