using System.Collections.Generic;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace ConsoleApp.Services
{
    class VkFriendsService : IVkFriendsService
    {
        private IVkAuthorizeService _vkAuthorizeService;

        public VkFriendsService(IVkAuthorizeService vkAuthorizeService)
        {
            _vkAuthorizeService = vkAuthorizeService;
        }

        public VkCollection<User> GetUserFriends(long userId)
        {
            var userApi = _vkAuthorizeService.EnsureUserAuthorized(userId);

            var friends = userApi.Friends.Get(new FriendsGetParams() {Fields = ProfileFields.All});

            return friends;
        }

        public IEnumerable<User> GetFriendFriends(long userId, long friendId)
        {
            var userApi = _vkAuthorizeService.EnsureUserAuthorized(userId);
            return userApi.Friends.Get(new FriendsGetParams() {Fields = ProfileFields.All, UserId = userId});
        }

        public Dictionary<User, IEnumerable<User>> GetFriendFriends(long userId, ICollection<User> userFriends)
        {
            var userApi = _vkAuthorizeService.EnsureUserAuthorized(userId);
            Dictionary<User, IEnumerable<User>> friends = new Dictionary<User, IEnumerable<User>>();

            foreach (var friend in userFriends)
            {
                var friendFriends = userApi.Friends.Get(new FriendsGetParams()
                    {Fields = ProfileFields.All, UserId = friend.Id});
                friends.Add(friend, friendFriends);
            }

            return friends;
        }
    }
}