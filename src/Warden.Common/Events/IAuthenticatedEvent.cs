namespace Warden.Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
        string UserId { get; }
    }
}