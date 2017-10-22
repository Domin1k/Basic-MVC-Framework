namespace MvcFramework.Contracts
{
    public interface IRenderable<TModel> : IRenderable
    {
        TModel Model { get; set; }
    }
}
