namespace MvcFramework.Routers
{
    using MvcFramework.Attributes;
    using MvcFramework.Contracts;
    using MvcFramework.Controllers;
    using MvcFramework.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ControllerRouter : IHandleable
    {
        private Controller _controllerInstance;

        private IDictionary<string, string> _getParams;
        private IDictionary<string, string> _postParams;

        private string _requestMethod;
        private string _controllerName;
        private string _actionName;
        private object[] _methodParams;

        public ControllerRouter()
        {
            _getParams = new Dictionary<string, string>();
            _postParams = new Dictionary<string, string>();
        }

        public IHttpResponse Handle(IHttpRequest request)
        {
            _requestMethod = request.Method.ToString();
            var urlParts = request.Url.Split(new[] { '/', '?' }, StringSplitOptions.RemoveEmptyEntries);
            if (urlParts.Length < 2)
            {
                return new BadRequestResponse();
            }
            _controllerName = $"{StringHelper.FirstCharToUpper(urlParts[0])}{MvcContext.Instance.ControllerSuffix}";
            _actionName = StringHelper.FirstCharToUpper(urlParts[1]);
            _getParams = request.UrlParameters;
            _postParams = request.FormData;
            InitController();
            var method = GetMethod();
            if(method == null)
            {
                return new NotFoundResponse();
            }

            var parameters = method.GetParameters();

            _methodParams = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                ConvertPrimitives(param, i);
                ConvertComplexTypes(param, i);
            }

            var actionResult = (IInvokable)GetMethod().Invoke(_controllerInstance, _methodParams);
            var content = actionResult.Invoke();
            var response = new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }
                
        private void ConvertComplexTypes(ParameterInfo param, int index)
        {
            if (param.ParameterType.IsPrimitive || param.ParameterType == typeof(string))
            {
                return;
            }

            var bindingModelType = param.ParameterType;
            var bindingModel = Activator.CreateInstance(bindingModelType);
            var properties = bindingModelType.GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(bindingModel, Convert.ChangeType(_postParams[prop.Name], prop.PropertyType));
            }
            _methodParams[index] = Convert.ChangeType(bindingModel, bindingModelType);
        }

        private void ConvertPrimitives(ParameterInfo param, int index)
        {
            if (param.ParameterType.IsPrimitive || param.ParameterType == typeof(string))
            {
                var paramValue = _getParams[param.Name];
                _methodParams[index] = Convert.ChangeType(paramValue, param.ParameterType);
            }
        }

        private MethodInfo GetMethod()
        {
            foreach (var methodInfo in GetSuitableMethodInfos())
            {
                var attributes = methodInfo.GetCustomAttributes().Where(a => a is HttpMethodAttribute);

                if (!attributes.Any() && _requestMethod.ToUpper() == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(_requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return null;
        }

        private IEnumerable<MethodInfo> GetSuitableMethodInfos()
        {
            if (_controllerInstance == null)
            {
                return new MethodInfo[0];
            }

            return _controllerInstance
                .GetType()
                .GetMethods()
                .Where(m => m.Name == _actionName)
                .ToList();
        }

        private void InitController()
        {
            var controllerFullQualifiedName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Instance.AssemblyName,
                MvcContext.Instance.ControllersFolder,
                _controllerName);

            Type type = Type.GetType(controllerFullQualifiedName);

            if (type == null)
            {
                throw new InvalidOperationException("Invalid controller!");
            }

            _controllerInstance = (Controller)Activator.CreateInstance(type);
        }
    }
}
