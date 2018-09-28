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
            Type destinationType = destination.GetType();

            var sourceTypeProperties = sourceType.GetProperties();
            var destinationTypeProperties = destinationType.GetProperties();

            foreach (var sourceProperty in sourceTypeProperties)
            {
                destinationTypeProperties
                    .FirstOrDefault(x => x.PropertyType == sourceProperty.PropertyType && x.Name == sourceProperty.Name)
                    ?.SetValue(destination, sourceProperty.GetValue(source));
            }

            return destination;
        }

        public static object UpdateParamsMapper(object source, object destination, params string[] targetProperties)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            var sourceTypeProperties = sourceType.GetProperties();
            var sourceTypeDestination = destinationType.GetProperties();

            foreach (var sourceProperty in sourceTypeProperties)
            {
                if (targetProperties.FirstOrDefault(x => x == sourceProperty.Name) != null)
                {
                    sourceTypeDestination
                    .FirstOrDefault(x => x.PropertyType == sourceProperty.PropertyType && x.Name == sourceProperty.Name)
                    ?.SetValue(destination, sourceProperty.GetValue(source));
                }
            }

            return destination;

        }

        public static TD UpdateParamsGenerics<TS, TD>(TS source, TD destination, params string[] targetProperties)
            where TS : class
            where TD : class
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            var sourceTypeProperties = sourceType.GetProperties();
            var destinationTypeProperties = destinationType.GetProperties();

            foreach (var sourceProperty in sourceTypeProperties)
            {
                if (sourceProperty.PropertyType == typeof(ICollection<>))
                {

                }

                else
                {
                    destinationTypeProperties
                                .FirstOrDefault(x => x.PropertyType == sourceProperty.PropertyType && x.Name == sourceProperty.Name)
                                ?.SetValue(destination, sourceProperty.GetValue(source)); 
                }
            }

            return destination;
        }
    }
}
