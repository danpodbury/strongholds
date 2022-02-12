using StrongholdsAPI.Data;
using StrongholdsUtil.Models;


namespace StrongholdsAPI.Managers;

public class MissionManager
{
    private readonly StrongholdsContext _context;

    public MissionManager(StrongholdsContext context)
    {
        _context = context;
    }

    public List<Mission> GetMissionsByLoginID(int loginID)
    {
        var robots = _context.Robots.Where(r => r.LoginID == loginID).ToList();
        List<int> robotIDs = new List<int>();
        foreach (var robot in robots)
        {
            robotIDs.Add(robot.RobotID);
        }
        return _context.Missions.Where(m => robotIDs.Contains(m.RobotID)).ToList();
       
    }

    public List<Mission> GetMissionsByRobotID(int robotID)
    {
        return _context.Missions.Where(m => m.RobotID == robotID).ToList();
    }

    public List<Mission> GetMission(int missionID)
    {
        return _context.Missions.ToList();
    }

    public int AddMission(Mission mission)
    {
        _context.Missions.Add(mission);
        _context.SaveChanges();
        return mission.MissionID;
    }

}
