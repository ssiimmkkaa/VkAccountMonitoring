using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    class HostService
    {
        private readonly ILogger<HostService> _logger;
        
        public HostService(
            IService service,
            IEnumerable<IService> services,
            ILogger<HostService> logger
            )
        {
            _logger = logger;
            
            if (service is IAuthorizeService authorize)
            {
                try
                {
                    authorize.Authorize();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }
        }
    }
}