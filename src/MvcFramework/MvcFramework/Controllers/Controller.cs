namespace MvcFramework.Controllers
{
    using MvcFramework.Contracts;
    using MvcFramework.Models;
    using MvcFramework.ViewEngine;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        protected IActionResult View([CallerMemberName] string caller = null)
        {
            var controllerName = GetType().Name.Replace(MvcContext.Instance.ControllerSuffix, string.Empty);

            var fullQualifiedControllerName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Instance.AssemblyName,
                MvcContext.Instance.ViewsFolder,
                controllerName,
                caller);

            return new ActionResult(fullQualifiedControllerName);
        }

        protected IActionResult View(string controllerName, string actionName)
        {
            var fullQualifiedControllerName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Instance.AssemblyName,
                MvcContext.Instance.ViewsFolder,
                controllerName,
                actionName);

            return new ActionResult(fullQualifiedControllerName);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName] string caller = null)
        {
            var controllerName = GetType().Name.Replace(MvcContext.Instance.ControllerSuffix, string.Empty);

            var fullQualifiedControllerName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Instance.AssemblyName,
                MvcContext.Instance.ViewsFolder,
                controllerName,
                caller);

            return new ActionResult<TModel>(fullQualifiedControllerName, model);
        }

        protected IActionResult<TModel> View<TModel>(string controllerName, string actionName, TModel model)
        {
            var fullQualifiedControllerName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Instance.AssemblyName,
                MvcContext.Instance.ViewsFolder,
                controllerName,
                actionName);

            return new ActionResult<TModel>(fullQualifiedControllerName, model);
        }
    }
}
