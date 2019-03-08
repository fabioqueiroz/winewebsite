using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.Business.Interfaces;

namespace Wine.Commons.Validation
{
    public class FiguresRule<T> : IValidationRule<T>
    {
        private Func<T, decimal> _figure;
 
        public FiguresRule(Func<T, decimal> figure)
        {
            _figure = figure;
        }

        public bool IsValid(T entity)
        {
            return _figure(entity) > 0;
        }
    }
}
