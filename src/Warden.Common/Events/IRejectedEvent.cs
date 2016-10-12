namespace Warden.Common.Events
{
    public interface IRejectedEvent : IAuthenticatedEvent
    {
        string Reason { get; }
    }
}