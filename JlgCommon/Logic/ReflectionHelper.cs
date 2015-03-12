using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.Logic
{
    public class ReflectionHelper
    {

        public Func<object, object> BuildPropertyGetter(Type runtimeType, string propertyName)
        {
            //It is much more efficient to compile a getter function using expression trees and reuse it (instead of directly using reflection each time you need this).
            
            var propertyInfo = runtimeType.GetProperty(propertyName);

            // create a parameter (object obj)
            var obj = Expression.Parameter(typeof(object), "obj");

            // cast obj to runtimeType
            var objT = Expression.TypeAs(obj, runtimeType);

            // property accessor
            var property = Expression.Property(objT, propertyInfo);

            var convert = Expression.TypeAs(property, typeof(object));
            return (Func<object, object>)Expression.Lambda(convert, obj).Compile();
        }
    }
}
