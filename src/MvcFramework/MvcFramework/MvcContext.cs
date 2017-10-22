namespace MvcFramework
{
    public class MvcContext
    {
        private static MvcContext _instance;        

        private MvcContext()
        {
        }

        public static MvcContext Instance => _instance == null ? (_instance = new MvcContext()) : _instance;

        public string AssemblyName { get; set; }

        public string ControllersFolder { get; set; } = "Controllers";

        public string ControllerSuffix { get; set; } = "Controller";

        public string ViewsFolder { get; set; } = "Views";

        public string ModelsFolder { get; set; } = "Models";
    }
}
