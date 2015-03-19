using System;

namespace JlgCommon.Domain
{
    public class NameValuePair<T>
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual T Value { get; set; }

        public NameValuePair()
        {
            Id = Guid.NewGuid();
        }

        public NameValuePair(string name, T value)
        {
            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }
    }
}
