using Microsoft.AspNetCore.Mvc;
using StrongholdsAPI.Models;
using StrongholdsAPI.Managers;

using SimpleHashing;

namespace StrongholdsAPI.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginManager _repo;
        private readonly StationManager _stationRepo;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, LoginManager repo, StationManager repo2)
        {
            _logger = logger;
            _repo = repo;
            _stationRepo = repo2;
        }
        
        // 
        [HttpPost, Route("/SignUp/{username}")]
        public Object SignUp(string username)
        {
            // Check if username alread in use
            if (UserExists(username))
            {
                return new Error
                {
                    ErrorText = "Username has already been taken."
                };
            }

            // Create new token for user
            var token = Guid.NewGuid().ToString();

            // Create login object with hashed token
            var login = new Login
            {
                Username = username,
                HashedToken = PBKDF2.Hash(token),
                Token = token //Development only
            };

            _repo.Add(login);

            // Create station for user
            _stationRepo.Add(token);

            login.Token = token;
            login.HashedToken = null;

            return login;
        }

        // [DEVELOPMENT ONLY]
        [HttpGet, Route("/Users/")]
        public List<Login> GetUsers()
        {
            return _repo.Get();
        }


        private bool UserExists(string username)
        {
            if (_repo.GetByName(username) == null) return false;
            return true;
        }
 
    }


}