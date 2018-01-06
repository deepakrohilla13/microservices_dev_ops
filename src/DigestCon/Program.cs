using System;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.Configuration;
using System.Threading;

namespace DigestCon
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<Runner>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));
            var serviceProvider = services.BuildServiceProvider();
            var logFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            logFactory.AddNLog(new NLogProviderOptions{CaptureMessageTemplates = true, CaptureMessageProperties = true});
            logFactory.ConfigureNLog("nlog.config");


            
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
            var Configuration = builder.Build();
            string myKey1 = Configuration["JobSettings:Job1"];
            Console.WriteLine(myKey1);

        
            System.Console.WriteLine("Starting Main");
            InitScheduler();
            System.Console.WriteLine("End of Main");
            System.Console.WriteLine("Sleeping Main thread");
            Thread.Sleep(Timeout.Infinite);
            Console.ReadLine();
        }

        static async void InitScheduler()
        {
            System.Console.WriteLine("Init");
            var props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            System.Console.WriteLine("Job");

            IJobDetail job1 = JobBuilder.Create<Job1>()
                        .WithIdentity("myJob1", "group1")
                        .Build();

            IJobDetail job2 = JobBuilder.Create<Job2>()
                        .WithIdentity("myJob2", "group1")
                        .Build();

            System.Console.WriteLine("Trigger");

            ITrigger trigger1 = TriggerBuilder.Create()
                        .WithIdentity("myTrigger1", "group1")
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(1)
                            .RepeatForever())
                        .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
                        .WithIdentity("myTrigger2", "group1")
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(1)
                            .RepeatForever())
                        .Build();

            System.Console.WriteLine("Scheding first >>>>>>");

            await sched.ScheduleJob(job1, trigger1);
            System.Console.WriteLine("Scheduling Second >>>>>>>>>>>>>>>>>>>>>>>");
            await sched.ScheduleJob(job2, trigger2);
            System.Console.WriteLine("Scheduled");

        }
    }
}
