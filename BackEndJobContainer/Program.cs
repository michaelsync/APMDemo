using Serilog;
using Topshelf;

namespace BackEndJobContainer {
    class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                       .Enrich.WithThreadId()
                       .ReadFrom.AppSettings()
                       .CreateLogger();
            Log.Information("BackEndJobAService is started.");

            HostFactory.Run(x => {
                x.Service<BackEndJobService>(s => {
                    s.ConstructUsing(name => new BackEndJobService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("BackEnd Job A Service.");
                x.SetDisplayName("BackEndJobAService");
                x.SetServiceName("BackEndJobAService");
            });


            Log.Information("BackEndJobAService is ended.");
        }
    }
}
