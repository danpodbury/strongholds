using StrongholdsAPI.Data;
using StrongholdsUtil.Models;


namespace StrongholdsAPI.Managers;

public class StationManager
{
    private readonly StrongholdsContext _context;

    public StationManager(StrongholdsContext context)
    {
        _context = context;
    }

    public List<Station> GetAll()
    {
        return _context.Stations.ToList();
    }


    public Station Get(int id, string token)
    {
        try
        {
            var loginID = _context.Logins.Where(l => l.Token == token).ToList().First().LoginID;
            return _context.Stations.Where(r => r.LoginID == loginID && r.StationID == id).ToList().First();

        } catch
        {
            throw new Exception($"Couldn't find station id: {id} and token: {token}");
        }
        
    }

    public Station Get(string token)
    {
        try
        {
            var loginID = _context.Logins.Where(l => l.Token == token).ToList().First().LoginID;
            return _context.Stations.Where(r => r.LoginID == loginID).ToList().First();

        }
        catch
        {
            throw new Exception($"Couldn't find a station for token: {token}");
        }

    }

    public void Add(string token)
    {
        var loginID = _context.Logins.Where(l => l.Token == token).ToList().First().LoginID;

        // Add default station for new user
        _context.Stations.Add(
            new Station
            {
                LoginID = loginID
            }
        );
        _context.SaveChanges();

        // Add default robot for new user
        _context.Robots.Add(
            new Robot
            { 
                Name = "bot01", 
                LoginID = loginID,
                 //StationID = _context.Stations.Where(s => s.LoginID == loginID).ToList().First().StationID,
            }
        );
        _context.SaveChanges();
    }


}
