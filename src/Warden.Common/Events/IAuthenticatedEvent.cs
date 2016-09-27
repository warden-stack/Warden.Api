namespace Warden.Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
        string AuthenticatedUserId { get; set; }
    }
}