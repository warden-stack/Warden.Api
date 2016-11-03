namespace Warden.Common.Encryption
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
    }
}