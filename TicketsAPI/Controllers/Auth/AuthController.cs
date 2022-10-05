using Microsoft.AspNetCore.Mvc;
using TicketsAPI.Auth;

namespace TicketsAPI.Controllers.Auth
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ICustomUserManager _customUserManager;
        private readonly ICustomTokenManager _customTokenManager;

        public AuthController(ICustomUserManager customUserManager, ICustomTokenManager customTokenManager)
        {
            _customUserManager = customUserManager;
            _customTokenManager = customTokenManager;
        }

        [HttpPost]
        [Route("/authenticate")]
        public Task<string> Authenticate(string username, string password)
        {
            return Task.FromResult(_customUserManager.Authenticate(username, password));
        }

        [HttpGet]
        [Route("/verifyToken")]
        public Task<bool> Verify(string token)
        {
            return Task.FromResult(_customTokenManager.VerifyToken(token));
        }

        [HttpGet]
        [Route("/getuserinfo")]
        public Task<string> GetUserInfoByToken(string token)
        {
            return Task.FromResult(_customTokenManager.GetUserInfoByToken(token));
        }
    }
}
