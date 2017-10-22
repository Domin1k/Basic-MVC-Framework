namespace MvcApp
{
    using MvcFramework.Routers;
    using MvcFramework.ViewEngine;
    using WebServer;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            var server = new ServerRunner(1337, new ControllerRouter());

            MvcEngine.Run(server);
        }
    }
}
