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

using Microsoft.Extensions.DependencyInjection;  
using Microsoft.Extensions.Logging;
using DigestCon.Logging;
using StructureMap;
using NLog.Extensions.Logging;

namespace DigestCon
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                                .AddLogging();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));


            services.AddLogging((lb) => lb.SetMinimumLevel(LogLevel.Trace));
            
            var container = new Container();
            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                            {
                                _.AssemblyContainingType(typeof(Program));
                                _.WithDefaultConventions();
                            });
                // Populate the container using the service collection
                config.Populate(services);
            });

           
            var loggerFactory = container.GetInstance<ILoggerFactory>();
            //var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties =true });
            loggerFactory.ConfigureNLog("nlog.config"); 
            /////////////////////////////////////////////           

            var docFun = container.GetInstance<IDocBuilder>();
            docFun.Try();






            /////////////////////////////////////////////           
            
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
