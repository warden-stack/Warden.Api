using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Warden.Api.Framework.Filters;
using Warden.Api.Infrastructure.Services;
using Warden.Api.Infrastructure.Settings;
using NLog.Extensions.Logging;
using Rebus.Activation;
using LogLevel = Rebus.Logging.LogLevel;
using Rebus.Transport.Msmq;

namespace Warden.Api
{
    public class Startup
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var activator = new BuiltinHandlerActivator();
            Rebus.Config.Configure.With(activator)
                .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                .Transport(t => t.UseMsmq("Warden.Api"))
                .Start();

            services.Configure<AccountSettings>(Configuration.GetSection("account"));
            services.Configure<DatabaseSettings>(Configuration.GetSection("database"));
            services.Configure<EmailSettings>(Configuration.GetSection("email"));
            services.Configure<FeatureSettings>(Configuration.GetSection("feature"));
            services.Configure<GeneralSettings>(Configuration.GetSection("general"));
            services.Configure<PaymentPlanSettings>(Configuration.GetSection("paymentPlan"));
            services.Configure<Auth0Settings>(Configuration.GetSection("auth0"));
            var databaseSettings = GetConfigurationValue<DatabaseSettings>("database");
            services.AddSingleton(GetConfigurationValue<FeatureSettings>("account"));
            services.AddSingleton(databaseSettings);
            services.AddSingleton(GetConfigurationValue<EmailSettings>("email"));
            services.AddSingleton(GetConfigurationValue<FeatureSettings>("feature"));
            services.AddSingleton(GetConfigurationValue<GeneralSettings>("general"));
            services.AddSingleton(GetConfigurationValue<PaymentPlanSettings>("paymentPlan"));
            services.AddSingleton(GetConfigurationValue<Auth0Settings>("auth0"));
            services.AddSingleton(activator.Bus);
            services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });
            ApplicationContainer = Infrastructure.IoC.Container.Resolve(services, databaseSettings.Type);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        private T GetConfigurationValue<T>(string section) where T : new()
        {
            var configurationValue = new T();
            Configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");
            var options = new JwtBearerOptions
            {
                Audience = Configuration["auth0:clientId"],
                Authority = $"https://{Configuration["auth0:domain"]}/",
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
                }
            };
            app.UseJwtBearerAuthentication(options);
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            }
            app.UseMvc();
            app.UseDeveloperExceptionPage();
            Task.WaitAll(InitializeDatabaseAsync(app));
            Logger.Info("Warden API has started.");
        }

        private async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            var databaseInitializer = app.ApplicationServices.GetService<IDatabaseInitializer>();
            await databaseInitializer.InitializeAsync();
        }
    }
}
