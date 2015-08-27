using Serilog;
using Topshelf;

namespace BackEndJobContainer {
    class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                       .Enrich.WithThreadId()
                       .Enrich.WithMachineName()
                       .Enrich.FromLogContext()
                       .ReadFrom.AppSettings()
                       .CreateLogger();
            Log.Information("BackEndJobContainer is started.");

            HostFactory.Run(x => {
                x.Service<BackEndJobService>(s => {
                    s.ConstructUsing(name => new BackEndJobService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("BackEndJobContainerService");
                x.SetDisplayName("BackEndJobContainerService");
                x.SetServiceName("BackEndJobContainerService");
            });


            Log.Information("BackEndJobContainerService is ended.");
        }
    }
}
