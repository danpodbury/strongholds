using StrongholdsAPI.Models;

namespace StrongholdsAPI.Services
{
    public class GameState
    {
        public List<Robot> Robots { get; set; } = new List<Robot>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
