using StrongholdsAPI.Data;
using StrongholdsUtil.Models;


namespace StrongholdsAPI.Managers;

public class LocationManager
{
    private readonly StrongholdsContext _context;

    public LocationManager(StrongholdsContext context)
    {
        _context = context;
    }

    public List<Location> Get()
    {
        return _context.Locations.ToList();
    }


    public Location Get(int id)
    {
        return _context.Locations.Find(id);
    }

    
}
