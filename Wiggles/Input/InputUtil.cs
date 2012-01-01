using System;
using System.Collections.Generic;
using System.Reflection;

namespace InnovationEngine.Input
{
    public static class InputUtil
    {
        public static List<T> GetEnumValues<T>()
        {
            Type currentEnum = typeof(T);
            List<T> resultSet = new List<T>();

            if (currentEnum.IsEnum)
            {
                FieldInfo[] fields = currentEnum.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo field in fields)
                    resultSet.Add((T)field.GetValue(null));
            }
            else
            {
               throw new ArgumentException("The argument must of type Enum or of a type derived from Enum", "T");
            }
            return resultSet;
        }
    }
}
