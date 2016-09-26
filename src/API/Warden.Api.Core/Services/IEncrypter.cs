namespace Warden.Api.Core.Services
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
    }
}