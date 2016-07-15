namespace Warden.Api.Core.Events.Wardens
{
    public class WardenCreated : IEvent
    {
        public string Name { get; }

        public WardenCreated(string name)
        {
            Name = name;
        }
    }
}