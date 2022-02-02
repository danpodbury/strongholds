using StrongholdsAPI.Data;
using StrongholdsAPI.Models;


namespace StrongholdsAPI.Managers;

public class RobotManager
{
    private readonly StrongholdsContext _context;

    public RobotManager(StrongholdsContext context)
    {
        _context = context;
    }

    public List<Robot> Get()
    {
        return _context.Robots.ToList();
    }


    public Robot Get(int id)
    {
        return _context.Robots.Find(id);
    }

    
}
