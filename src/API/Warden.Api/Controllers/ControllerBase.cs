using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Extensions;
using Warden.Api.Framework.Handlers;
using Warden.Api.Infrastructure.Commands;
using Warden.Common.DTO.Users;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly string NotificationsKey = "Notifications";
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IMapper Mapper;
        protected readonly IUserService UserService;

        protected ControllerBase(ICommandDispatcher commandDispatcher, IMapper mapper, IUserService userService)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
            UserService = userService;
        }

        protected RequestHandler<T> For<T>(T request) => new RequestHandler<T>(this, request);

        //TODO: Temporary property for API key usage.
        protected string CurrentUserId { get; set; }

        protected TModel MapTo<TModel>(object source)
            => (TModel) Mapper.Map(source, source.GetType(), typeof(TModel));

        public async Task<UserDto> GetCurrentUser()
        {
            var externalUserId = User?.Identity?.Name;
            if (externalUserId.Empty())
                throw new AuthenticationException("User is not authenticated.");

            var user = await UserService.GetAsync(externalUserId);

            return user;
        }

        //TODO: Log exception etc.
        public void HandleException(Exception exception)
        {
            throw exception;
        }
    }
}