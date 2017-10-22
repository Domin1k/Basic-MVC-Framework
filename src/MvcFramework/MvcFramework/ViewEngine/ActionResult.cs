namespace MvcFramework.ViewEngine
{
    using MvcFramework.Contracts;
    using System;

    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            Action = (IRenderable)Activator.CreateInstance(Type.GetType(viewFullQualifiedName));
        }
        public IRenderable Action { get; set; }

        public string Invoke() => Action.Render();
    }
}
