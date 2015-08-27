using Serilog;
using Topshelf;

namespace JobManager {
    class Program {

        static void Main(string[] args) {

            //.WriteTo.ColoredConsole(
            //outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
            //.Enrich.WithProperty("Version", "1.0.0")

            Log.Logger = new LoggerConfiguration()
                        .Enrich.WithThreadId()    
                        .Enrich.WithMachineName()
                        .Enrich.FromLogContext()
                        .ReadFrom.AppSettings()
                        .CreateLogger();

            Log.Information("The JobManager is started.");

            HostFactory.Run(x =>
            {
                x.Service<JobManagementService>(s =>
                {
                    s.ConstructUsing(name => new JobManagementService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("JobManager");
                x.SetDisplayName("JobManager");
                x.SetServiceName("JobManager");
            });


            Log.Information("The JobManager is ended.");
        }
    }
}
