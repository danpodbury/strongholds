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

    public List<Robot> Get(string token)
    {
        try
        {
            var loginID = _context.Logins.Where(l => l.Token == token).ToList().First().LoginID;

            return _context.Robots.Where(r => r.LoginID == loginID).ToList();
        }
        catch
        {
            throw new Exception("tried to get login ID of non existant account");
        }
 
    }


    public Robot Get(int id, string token)
    {
        try
        {
            var loginID = _context.Logins.Where(l => l.Token == token).ToList().First().LoginID;
            return _context.Robots.Where(r => r.LoginID == loginID && r.RobotID == id).ToList().First();

        } catch
        {
            throw new Exception($"Couldn't find robot id: {id} and token: {token}");
        }
        
    }

    
}
