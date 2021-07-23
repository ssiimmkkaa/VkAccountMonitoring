using System;
using ConsoleApp.Services;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    class HostService : IService
    {
        private readonly ILogger<HostService> _logger;
        private readonly IVkAuthorizeService _vkAuthorizeService;
        private readonly IVkFriendsService _vkFriendsService;

        public HostService(ILogger<HostService> logger, IVkAuthorizeService vkAuthorizeService,
            IVkFriendsService vkFriendsService)
        {
            _logger = logger;
            _vkAuthorizeService = vkAuthorizeService;
            _vkFriendsService = vkFriendsService;

            Run();
        }

        private void Run()
        {
            try
            {
                _vkAuthorizeService.Authorize();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}