namespace Warden.Api.Core.Domain
{
    public interface IValidatable
    {
        bool IsValid { get; }
    }
}