using VkNet;

namespace ConsoleApp.Services
{
    interface IVkAuthorizeService : IService
    {
        void Authorize();
        VkApi EnsureUserAuthorized(long userId);
    }
}