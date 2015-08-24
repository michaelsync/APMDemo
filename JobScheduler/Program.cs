using Serilog;
using Topshelf;

namespace JobScheduler {
    class Program {

        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                        .Enrich.WithThreadId()
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
