using System.Collections.Generic;
using VkNet.Model;
using VkNet.Utils;

namespace ConsoleApp.Services
{
    public interface IVkFriendsService : IService
    {
        public VkCollection<User> GetUserFriends(long userId);

        public IEnumerable<User> GetFriendFriends(long userId, long friendId);

        public Dictionary<User, IEnumerable<User>> GetFriendFriends(long userId, ICollection<User> userFriends);
    }
}