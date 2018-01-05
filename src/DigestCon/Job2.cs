using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace DigestCon
{
    public class Job2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await JobDetails();
        }

        Task JobDetails()
        {
            Thread.Sleep(6*1000);
            Console.WriteLine("||||||||||||||||||||||||||||||||");
            return Task.CompletedTask;
        }
    }
}