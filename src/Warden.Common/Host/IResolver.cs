namespace Warden.Common.Host
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}