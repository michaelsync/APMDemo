using Serilog;
using Topshelf;

namespace JobScheduler {
    class Program {

        static ILogger logger = new LoggerConfiguration()
                        .ReadFrom.AppSettings()
                        .CreateLogger();

        static void Main(string[] args) {
            logger.Information("The scheduler is started.");

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


            logger.Information("The scheduler is ended.");
        }
    }
}
