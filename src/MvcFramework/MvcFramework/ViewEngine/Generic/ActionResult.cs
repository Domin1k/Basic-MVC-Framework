namespace MvcFramework.ViewEngine
{
    using MvcFramework.Contracts;
    using System;

    public class ActionResult<TModel> : IActionResult<TModel>
    {
        public ActionResult(string viewFullQualifiedName, TModel model)
        {
            Action = (IRenderable<TModel>)Activator.CreateInstance(Type.GetType(viewFullQualifiedName));

            Action.Model = model;
        }

        public IRenderable<TModel> Action { get; set; }

        public string Invoke() => Action.Render();
    }
}
