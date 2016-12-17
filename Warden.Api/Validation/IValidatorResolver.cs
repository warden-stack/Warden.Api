namespace Warden.Api.Validation
{
    public interface IValidatorResolver
    {
        IValidator<T> Resolve<T>();
    }
}