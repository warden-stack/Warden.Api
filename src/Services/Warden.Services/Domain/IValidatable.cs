namespace Warden.Services.Domain
{
    public interface IValidatable
    {
        bool IsValid { get; }
    }
}