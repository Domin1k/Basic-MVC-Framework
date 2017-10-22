namespace MvcApp.Views.Home
{
    using MvcFramework.Contracts;

    public class Index : IRenderable
    {
        public string Render()
        {
            return "<h3>Hello</h3>";
        }
    }
}
