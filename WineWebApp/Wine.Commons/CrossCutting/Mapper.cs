using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wine.Commons.CrossCutting
{
    public class Mapper
    {
       public static object UpdateMapper(object source, object destination)
       {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            var sourceTypeProperties = sourceType.GetProperties();
            var sourceTypeDestination = destinationType.GetProperties();

            foreach (var sourceProperty in sourceTypeProperties)
            {
                sourceTypeDestination
                    .FirstOrDefault(x => x.PropertyType == sourceProperty.PropertyType && x.Name == sourceProperty.Name)
                    ?.SetValue(destination, sourceProperty.GetValue(source));
            }

            return destination;

       }

        public static TD UpdateGenerics<TS, TD>(TS source, TD destination) 
            where TS : class
            where TD : class
        {
            Type sourceType = source.GetType();
            Type destinationType = sourceType.GetType();

            //var sourceTypeProperties = sourceType.GetProperties();
            //var sourceTypeDestination = destinationType.GetProperties();

            var sourceTypeProperties = sourceType.GetGenericArguments();
            var sourceTypeDestination = destinationType.GetGenericArguments();

            foreach (var sourceProperty in sourceTypeProperties)
            {
                sourceTypeDestination
                    .FirstOrDefault(x => x.GenericParameterAttributes == sourceProperty.GenericParameterAttributes && x.Name == sourceProperty.Name)
                    ?.MakeGenericType(sourceType.ReflectedType);
            }

            return (TD)destination;
        }

        public static object UpdateParamsMapper(object source, object destination, params string[] avoidProperties)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            var sourceTypeProperties = sourceType.GetProperties();
            var sourceTypeDestination = destinationType.GetProperties();

            foreach (var sourceProperty in sourceTypeProperties)
            {
                if (avoidProperties.FirstOrDefault(x => x == sourceProperty.Name) != null)
                {
                    sourceTypeDestination
                    .FirstOrDefault(x => x.PropertyType == sourceProperty.PropertyType && x.Name == sourceProperty.Name)
                    ?.SetValue(destination, sourceProperty.GetValue(source));
                }
            }

            return destination;

        }
    }
}
