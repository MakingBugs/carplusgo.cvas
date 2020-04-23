using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CarPlusGo.CVAS.Common
{
    public class CommonFunc
    {
        public static T ObjectClone<T>(T o)
        {    
            Type t = typeof(T);
            var retval = t.Assembly.CreateInstance(t.FullName);

            foreach (PropertyInfo p in t.GetProperties())
            {
                retval.GetType().GetProperty(p.Name)
                    .SetValue(retval, o.GetType().GetProperty(p.Name).GetValue(o));
            }


            return (T)retval;
        }
    }
}
