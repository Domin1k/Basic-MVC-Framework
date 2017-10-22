namespace MvcFramework.Models
{
    using System.Collections.Generic;

    public class ViewModel
    {
        public IDictionary<string, string> Data { get; } = new Dictionary<string, string>();

        public string this[string key]
        {
            get => Data[key];
            set => Data[key] = value;
        }
    }
}
