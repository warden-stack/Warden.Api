using Microsoft.AspNetCore.Hosting;
using RawRabbit.vNext.Disposable;
using Warden.Services.Commands;
using Warden.Services.Events;
using Warden.Services.Extensions;

namespace Warden.Services.Host
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run()
        {
            _webHost.Run();
        }

        public static Builder Create(string name)
        {
            var builder = new Builder();

            return builder;
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class Builder : BuilderBase
        {
            private IResolver _resolver;
            private IBusClient _bus;
            private IWebHost _webHost;

            public Builder WithResolver(IResolver resolver)
            {
                _resolver = resolver;

                return this;
            }

            public BusBuilder WithBus()
            {
                _bus = _resolver.Resolve<IBusClient>();

                return new BusBuilder(_webHost, _bus, _resolver);
            }

            public Builder WithWebHost(IWebHost webHost)
            {
                _webHost = webHost;

                return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private readonly IBusClient _bus;
            private readonly IResolver _resolver;

            public BusBuilder(IWebHost webHost, IBusClient bus, IResolver resolver)
            {
                _webHost = webHost;
                _bus = bus;
                _resolver = resolver;
            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var commandHandler = _resolver.Resolve<ICommandHandler<TCommand>>();
                _bus.WithCommandHandlerAsync(commandHandler);

                return this;
            }

            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var eventHandler = _resolver.Resolve<IEventHandler<TEvent>>();
                _bus.WithEventHandlerAsync(eventHandler);

                return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}