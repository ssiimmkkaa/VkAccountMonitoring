using System;
using System.Collections.Generic;
using ConsoleApp.Exceptions.AuthorizeExceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VkNet;
using VkNet.Model;

namespace ConsoleApp.Services
{
    class VkAuthorizeService : IVkAuthorizeService
    {
        private readonly HostBuilderContext _hostBuilderContext;
        private readonly IOptions<VkAuthorizeData> _vkAuthorizeDataOptions;
        private readonly IOptions<ApplicationId> _applicationIdOptions;
        private readonly ILogger<IVkAuthorizeService> _logger;

        public VkAuthorizeService(
            HostBuilderContext hostBuilderContext,
            IOptions<ApplicationId> applicationIdOptions,
            IOptions<VkAuthorizeData> vkAuthorizeDataOptions,
            ILogger<VkAuthorizeService> logger
        )
        {
            _hostBuilderContext = hostBuilderContext;
            _vkAuthorizeDataOptions = vkAuthorizeDataOptions;
            _applicationIdOptions = applicationIdOptions;
            _logger = logger;
        }

        public void Authorize()
        {
            var api = new VkApi();

            try
            {
                api.Authorize(new ApiAuthParams
                {
                    ApplicationId = _applicationIdOptions.Value.Value,
                    Login = _vkAuthorizeDataOptions.Value.Login,
                    Password = _vkAuthorizeDataOptions.Value.Password,
                    Settings = VkApiSettingsConverter.GetSettingsByString(_vkAuthorizeDataOptions.Value.Settings)
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }

            var user = api.Account.GetProfileInfo();
            _logger.LogInformation(
                $"User {api.UserId} \'{user.FirstName} {user.LastName}\' is logged in. Access token: {api.Token}");

            _hostBuilderContext.Properties.Add(new KeyValuePair<object, object>("Api", api));
        }

        public VkApi EnsureUserAuthorized(long userId)
        {
            if (!_hostBuilderContext.Properties.ContainsKey("Api"))
            {
                throw new UserNotAuthorizedException();
            }

            VkApi api = (VkApi) _hostBuilderContext.Properties["Api"];

            if (api.UserId != userId)
            {
                throw new UserNotAuthorizedException(userId.ToString());
            }

            return api;
        }
    }
}