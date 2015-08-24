using Serilog;
using Topshelf;

namespace BackendJobA {
    class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                       .ReadFrom.AppSettings()
                       .CreateLogger();
            Log.Information("BackEndJobAService is started.");

            HostFactory.Run(x => {
                x.Service<BackEndJobAService>(s => {
                    s.ConstructUsing(name => new BackEndJobAService());
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
