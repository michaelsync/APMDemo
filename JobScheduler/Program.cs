﻿using Serilog;
using Topshelf;

namespace JobScheduler {
    class Program {

        static void Main(string[] args) {

            //.WriteTo.ColoredConsole(
            //outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
            //.Enrich.WithProperty("Version", "1.0.0")

            

            Log.Logger = new LoggerConfiguration()
                        .Enrich.WithThreadId()    
                        .Enrich.WithMachineName()                    
                        .ReadFrom.AppSettings()
                        .CreateLogger();
            Log.Information("The scheduler is started.");

            HostFactory.Run(x =>
            {
                x.Service<JobSchedulerService>(s =>
                {
                    s.ConstructUsing(name => new JobSchedulerService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("This backend job service manager.");
                x.SetDisplayName("JobScheduler");
                x.SetServiceName("JobScheduler");
            });


            Log.Information("The scheduler is ended.");
        }
    }
}
