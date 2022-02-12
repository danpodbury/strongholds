using StrongholdsUtil.Models;

namespace Strongholds.ViewModels
{
    public class GameView
    {
        public List<Robot> MyRobots { get; set; }
        public Login MyLogin { get; set; } 
        public List<Station> MyStations { get; set; }
        public List<Mission> MyMissions { get; set; }

    }
}
