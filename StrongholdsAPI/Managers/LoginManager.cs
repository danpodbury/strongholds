using StrongholdsAPI.Data;
using StrongholdsUtil.Models;


namespace StrongholdsAPI.Managers;

public class LoginManager
{
    private readonly StrongholdsContext _context;

    public LoginManager(StrongholdsContext context)
    {
        _context = context;
    }

    // Get all [DEVELOPMENT ONLY]
    public List<Login> Get()
    {
        return _context.Logins.ToList();
    }

    // Get by id
    public Login Get(int id)
    {
        return _context.Logins.Find(id);
    }

    // Get by name
    public Login? GetByName(string name)
    {
        var login = _context.Logins.Where(l => l.Username == name);

        if (login.Count() != 1) return null;

        return login.First();
    }
    // Add
    public Login Add(Login login)
    {
        _context.Logins.Add(login);
        _context.SaveChanges();

        return login;
    }


}
