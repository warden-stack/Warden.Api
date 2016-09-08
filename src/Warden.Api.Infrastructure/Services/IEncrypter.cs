namespace Warden.Api.Infrastructure.Services
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
        string GetSalt(string data);
        string GetHash(string data, string salt);
        string Encrypt(string text, string salt);
        string Decrypt(string text, string salt);
    }
}