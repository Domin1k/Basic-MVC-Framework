namespace MvcFramework.ViewEngine
{
    using System;
    using System.Reflection;
    using WebServer;
    
    public static class MvcEngine
    {
        public static void Run(ServerRunner webServer)
        {
            RegisterAssembly();

            try
            {
                webServer.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RegisterAssembly()
        {
            MvcContext.Instance.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;
        }
    }
}
