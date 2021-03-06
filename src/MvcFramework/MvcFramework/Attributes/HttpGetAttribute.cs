﻿namespace MvcFramework.Attributes
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod) => requestMethod.ToUpper() == "GET";
    }
}
