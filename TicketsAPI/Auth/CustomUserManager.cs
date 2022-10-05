namespace TicketsAPI.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private Dictionary<string, string> credentials = new Dictionary<string, string>()
        {
            {"Brian", "password1"}
        };
        private readonly ICustomTokenManager _customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            _customTokenManager = customTokenManager;
        }

        public string Authenticate(string username, string password)
        {
            if (credentials[username] != password) return string.Empty;
            return _customTokenManager.CreateToken(username);
        }
    }
}
