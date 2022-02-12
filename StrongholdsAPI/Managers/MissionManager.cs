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
        return _context.Missions.ToList();
    }

    public List<Mission> GetMissionsByRobotID(int robotID)
    {
        return _context.Missions.ToList();
    }

    public List<Mission> GetMission(int missionID)
    {
        return _context.Missions.ToList();
    }

    public int AddMission(Mission mission)
    {
        //Does this work?
        _context.Missions.Add(mission);
        _context.SaveChanges();
        return mission.MissionID;
    }

    public int AddObjective(Objective objective)
    {
        var cleanObj = new Objective
        {
            MissionID = objective.MissionID,
            Order = objective.Order,
            Latitude = objective.Latitude,
            Longitude = objective.Longitude,
            Action = objective.Action,
        };
        _context.Objectives.Add(objective);
        _context.SaveChanges();
        return objective.ObjectiveID;
    }

    //public void NewMission(Mission mission, string token)
    //{
    //    //_context.Missions.Add();
    //}



}
