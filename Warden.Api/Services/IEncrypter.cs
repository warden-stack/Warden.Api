namespace Warden.Api.Services
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
    }
}