using System.Reflection;
using RawRabbit;
using RawRabbit.Common;
using Warden.Common.Commands;
using Warden.Common.Events;

namespace Warden.Common.Extensions
{
    public static class RawRabbitExtensions
    {
        public static ISubscription WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler, string name = null) where TCommand : ICommand
        => bus.SubscribeAsync<TCommand>(async (msg, context) => await handler.HandleAsync(msg),
            cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TCommand>(name))));

        public static ISubscription WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler, string name = null) where TEvent : IEvent
        => bus.SubscribeAsync<TEvent>(async (msg, context) => await handler.HandleAsync(msg),
            cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TEvent>(name))));

        private static string GetExchangeName<T>(string name = null)
            => string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}"
                : $"{name}/{typeof(T).Name}";
    }
}