using System;

namespace Awesome.Domain
{
    public class AppSetting: BaseEntity
    {
        public AppSetting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
