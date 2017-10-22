namespace MvcFramework.Contracts
{
    public interface IActionResult : IInvokable
    {
        IRenderable Action { get; set; }
    }
}
