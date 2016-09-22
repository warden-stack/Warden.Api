namespace Warden.Api.Infrastructure.Services
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
    }
}