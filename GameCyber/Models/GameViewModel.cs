using GameCyber.Entities;

namespace GameCyber.Models
{
    public class GameViewModel
    {
        public List<Game> ListGames { get; set; }
        public List<Game> ListNewGames { get; set; }
        public List<Game> ListTopGames { get; set; }
        public List<Game> ListRelatedGames { get; set; }
        public Game GameEdit { get; set; }

    }
}
