using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Commons.Business.Interfaces
{
    public interface IValidationRule<T>
    {
        bool IsValid(T entity); 

    }
}
