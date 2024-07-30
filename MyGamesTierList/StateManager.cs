using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;

namespace MyGamesTierList
{
    public interface IStateManager
    {
        public List<Tier> Tiers { get; set; }

        public List<Game> GameList { get; set; }

        public void RemoveTier(Tier tier);

        public void AddTier();

        public void ChangeTier(Tier tier, string name, string color, int number);

        public void AddGameToTier(Game game, Tier tier, int position);
        public void AddGameToTier(Game game, Tier tier);

        public void RemoveGameFromTier(Game game, Tier tier);

        public void ReplaceGame(Game game, Tier tier, int position);

        public void SaveFilterState(string search, int playTime, bool showOnlyPlayed, bool sortByLastPlayed, bool sortByPlayTime, bool sortByName);

        public List<Game> GetFilteredResult();

        public FilterState Filter { get; set; }

        public void SetGames(string jsonString);

        public void RemoveGameFromList(Game game);

        public void AddGameToList(Game game);

        public Action RefreshGamesList { get; set; }
    }

    public class StateManager : IStateManager
    {

        public StateManager() 
        {
            Tiers = new List<Tier>()
            {
                new Tier("S", "#ff7f7f", 0),
                new Tier("A", "#ffbf7f", 1),
                new Tier("B", "#ffdf7f", 2),
                new Tier("C", "#ffff7f", 3),
                new Tier("D", "#bfff7f", 4),
                new Tier("E", "#7fff7f", 5),
                new Tier("F", "#7fffff", 6)
            };

            Filter = new();
        }

        public Action RefreshGamesList { get; set; }

        public void SetGames(string jsonString)
        {
            var rootobject = JsonConvert.DeserializeObject<Rootobject>(jsonString);
            AllGames = rootobject.response.games.ToList();
            GameList = AllGames.ToList();
        }

        public List<Tier> Tiers { get; set;}
        private List<Game> AllGames { get; set; }
        public List<Game> GameList { get; set; }
        public FilterState Filter { get; set; }

        public void SaveFilterState(string search, int playTime, bool showOnlyPlayed, bool sortByLastPlayed, bool sortByPlayTime, bool sortByName)
        {
            Filter.Search = search;
            Filter.PlayTime = playTime;
            Filter.ShowOnlyPlayed = showOnlyPlayed;
            Filter.SortByLastPlayed = sortByLastPlayed;
            Filter.SortByPlayTime = sortByPlayTime;
            Filter.SortByName = sortByName;
        }

        public void AddGameToTier(Game game, Tier tier, int position)
        {
            if (!tier.Games.Contains(game))
            {
                tier.Games.Insert(position, game);
            }
        }

        public void AddGameToTier(Game game, Tier tier)
        {
            if (!tier.Games.Contains(game))
            {
                tier.Games.Add(game);
            }
        }

        public void AddTier()
        {
            var lastPos = Tiers.Count;
            var newTier = new Tier("NewTier", "#ffffff", lastPos);
            Tiers.Add(newTier);
        }

        public void ChangeTier(Tier tier, string name, string color, int position)
        {
            tier.Name = name;
            tier.BackgroundColor = color;
            tier.Position = position;

            Tiers.Remove(tier);
            Tiers.Insert(position, tier);
        }

        public void RemoveGameFromTier(Game game, Tier tier)
        {
            if (Tiers.Contains(tier))
            {
                tier.Games.Remove(game);
            }

        }

        public void RemoveTier(Tier tier)
        {
            Tiers.Remove(tier);
        }

        public void ReplaceGame(Game game, Tier tier, int position)
        {
            tier.Games.Remove(game);
            tier.Games.Insert(position, game);
        }

        public void RemoveGameFromList(Game game)
        {
            GameList.Remove(game);
        }

        public void AddGameToList(Game game)
        {
            if (!GameList.Contains(game))
            {
                GameList.Add(game);
            }

        }

        public List<Game> GetFilteredResult()
        {
            List<Game> result;
            if (Filter.Search != null)
            {
                if (Filter.PlayTime > 0 || Filter.ShowOnlyPlayed)
                {
                    result = GameList.Where(g => g.playtime_forever > Filter.PlayTime).Where(g => g.name.Contains(Filter.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    result = GameList.Where(g => g.name.Contains(Filter.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }
            else
            {
                if (Filter.PlayTime > 0 || Filter.ShowOnlyPlayed)
                {
                    result = GameList.Where(g => g.playtime_forever > Filter.PlayTime).ToList();
                }
                else
                {
                    result = GameList.ToList();
                }
            }

            if (Filter.SortByLastPlayed)
            {
                result.Sort((emp2, emp1) => emp1.rtime_last_played.CompareTo(emp2.rtime_last_played));
            }
            else if (Filter.SortByPlayTime)
            {
                result.Sort((emp2, emp1) => emp1.playtime_forever.CompareTo(emp2.playtime_forever));
            }
            else if (Filter.SortByName)
            {
                result.Sort((emp1, emp2) => emp1.name.CompareTo(emp2.name));
            }

            return result;
        }
    }

    public class FilterState()
    {
        public string Search = "";
        public int PlayTime = 0;
        public bool ShowOnlyPlayed = false;
        public bool SortByLastPlayed = false;
        public bool SortByPlayTime = false;
        public bool SortByName = true;
    }
}
