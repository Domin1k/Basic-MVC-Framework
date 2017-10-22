namespace MvcFramework.Contracts
{
    public interface IActionResult<TModel> : IInvokable
    {
        IRenderable<TModel> Action { get; set; }
    }
}
