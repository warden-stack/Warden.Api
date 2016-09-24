namespace Warden.Services.Host
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}