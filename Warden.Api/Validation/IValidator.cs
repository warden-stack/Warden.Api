using System.Collections.Generic;

namespace Warden.Api.Validation
{
    public interface IValidator<in T>
    {
        IEnumerable<string> SetPropertiesAndValidate(T value);
    }
}