namespace Warden.Services.Encryption
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
    }
}