using StrongholdsUtil.Models;

namespace Strongholds.ViewModels
{
    public class GameView
    {
        public List<Robot> MyRobots { get; set; } = new List<Robot>();
        public Login MyLogin { get; set; } = new Login();
        public List<Station> MyStations { get; set; } = new List<Station>();
        public List<Mission> MyMissions { get; set; } = new List<Mission>();

    }
}
