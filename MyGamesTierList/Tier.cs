using System.Collections;

namespace MyGamesTierList
{

    public class Tier
    {
        public Tier(string name, string color, int position)
        {
            Name = name;
            BackgroundColor = color;
            Position = position;
            Games = new();
        }

        public Tier()
        {
            Games = new();
        }

        public string Name { get; set; }

        public Action TierRefreshDelegate { get; set; }

        public string BackgroundColor { get; set; }

        public int Position { get; set; }

        public List<Game> Games { get; set; }

        public void RefreshTear()
        {
            if (TierRefreshDelegate != null)
            {
                TierRefreshDelegate.Invoke();
            }
        }
    }


    public class Game
    {
        public int appid { get; set; }
        public string name { get; set; }
        public int playtime_forever { get; set; }
        public string img_icon_url { get; set; }
        public bool has_community_visible_stats { get; set; }
        public int playtime_windows_forever { get; set; }
        public int playtime_mac_forever { get; set; }
        public int playtime_linux_forever { get; set; }
        public int playtime_deck_forever { get; set; }
        public int rtime_last_played { get; set; }
        public int playtime_disconnected { get; set; }

        public string GetImageUrl()
        {

            return $"https://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{img_icon_url}.jpg";
        }
    }


    public class Rootobject
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public int total_count { get; set; }
        public Game[] games { get; set; }
    }

}
