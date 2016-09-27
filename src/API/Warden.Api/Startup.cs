using System;
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
using NLog.Extensions.Logging;
using RawRabbit.vNext;
using Warden.Api.Core.Settings;

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
            services.Configure<AccountSettings>(Configuration.GetSection("account"));
            services.Configure<EmailSettings>(Configuration.GetSection("email"));
            services.Configure<GeneralSettings>(Configuration.GetSection("general"));
            services.Configure<Auth0Settings>(Configuration.GetSection("auth0"));
            services.Configure<RedisSettings>(Configuration.GetSection("redis"));
            services.Configure<StorageSettings>(Configuration.GetSection("storage"));
            services.AddSingleton(GetConfigurationValue<AccountSettings>());
            services.AddSingleton(GetConfigurationValue<EmailSettings>());
            services.AddSingleton(GetConfigurationValue<GeneralSettings>());
            services.AddSingleton(GetConfigurationValue<Auth0Settings>());
            services.AddSingleton(GetConfigurationValue<RedisSettings>());
            services.AddSingleton(GetConfigurationValue<StorageSettings>());
            services.AddRawRabbit();
            services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });
            ApplicationContainer = Core.IoC.Container.Resolve(services);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        private T GetConfigurationValue<T>(string section = "") where T : new()
        {
            if (string.IsNullOrWhiteSpace(section))
            {
                section = typeof(T).Name.Replace("Settings", string.Empty);
            }

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
            Logger.Info("Warden API has started.");
        }
    }
}
