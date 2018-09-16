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
    }
}
