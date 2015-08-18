using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.Extensions
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
            {
                var res= Activator.CreateInstance(t);
                return res;
            }
            
             return null;
        }
    }
}
