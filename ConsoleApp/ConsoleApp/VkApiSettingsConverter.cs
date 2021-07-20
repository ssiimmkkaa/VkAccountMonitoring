using VkNet.Enums.Filters;

namespace ConsoleApp
{
    public static class VkApiSettingsConverter
    {
        public static Settings GetSettingsByString(string settings)
        {
            return settings switch
            {
                "All" => Settings.All,
                "Notify" => Settings.Notify,
                "Friends" => Settings.Friends,
                "Photos" => Settings.Photos,
                "Audio" => Settings.Audio,
                "Video" => Settings.Video,
                "Pages" => Settings.Pages,
                "Notes" => Settings.Notes,
                "Messages" => Settings.Messages,
                "Wall" => Settings.Wall,
                "Ads" => Settings.Ads,
                "Offline" => Settings.Offline,
                "Docs" => Settings.Documents,
                "Groups" => Settings.Groups,
                "Notifications" => Settings.Notifications,
                "Stats" => Settings.Stats,
                "Email" => Settings.Email,
                "Market" => Settings.Market,
                _ => Settings.All
            };
        }
    }
}