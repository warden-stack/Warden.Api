using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Api.Framework.Handlers;
using Warden.Common.DTO.Users;
using Warden.Common.Extensions;

namespace Warden.Api.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly string NotificationsKey = "Notifications";
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IMapper Mapper;
        protected readonly IUserProvider UserProvider;
        private string _currentUserId = string.Empty;

        protected ControllerBase(ICommandDispatcher commandDispatcher, IMapper mapper, IUserProvider userProvider)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
            UserProvider = userProvider;
        }

        protected RequestHandler<T> For<T>(T request) => new RequestHandler<T>(this, request);

        protected string CurrentUserId
        {
            get
            {
                if (_currentUserId.Empty())
                    SetCurrentUserId(User?.Identity?.Name?.Replace("auth0|", string.Empty));

                return _currentUserId;
            }
        }

        protected void SetCurrentUserId(string id)
        {
            _currentUserId = id;
        }

        protected TModel MapTo<TModel>(object source)
            => (TModel) Mapper.Map(source, source.GetType(), typeof(TModel));

        public async Task<UserDto> GetCurrentUserAsync()
        {
            if (User?.Identity?.Name.Empty() == true)
                throw new AuthenticationException("User is not authenticated.");

            var user = await UserProvider.GetAsync(CurrentUserId);

            return user;
        }

        //TODO: Log exception etc.
        public void HandleException(Exception exception)
        {
            throw exception;
        }
    }
}