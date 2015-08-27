using Serilog;
using Topshelf;

namespace BackEndJobInformationCenter {
    class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                       .Enrich.WithThreadId()
                       .Enrich.WithMachineName()
                       .Enrich.FromLogContext()
                       .ReadFrom.AppSettings()
                       .CreateLogger();
            Log.Information("Information Center Service is started.");

            HostFactory.Run(x => {
                x.Service<BackEndJobInformationService>(s => {
                    s.ConstructUsing(name => new BackEndJobInformationService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Information Center Service.");
                x.SetDisplayName("InformationCenterService");
                x.SetServiceName("InformationCenterService");
            });


            Log.Information("Information Center Service is ended.");
        }
    }
}
