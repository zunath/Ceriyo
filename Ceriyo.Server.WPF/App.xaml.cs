using System.Windows;

namespace Ceriyo.Server.WPF
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);

            var bootstrap = new Bootstrapper();
            bootstrap.Run();
        }
    }
}
