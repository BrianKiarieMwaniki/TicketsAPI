namespace TicketsAPI.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {
        private List<Token> tokens = new();
        public string CreateToken(string username)
        {
            var token = new Token(username);
            tokens.Add(token);
            return token.TokenString;
        }

        public bool VerifyToken(string token)
        {
            return tokens.Any(t => token != null && token.Contains(t.TokenString) && t.ExpiryDate > DateTime.Now);
        }

        public string GetUserInfoByToken(string tokenString)
        {
            var token = tokens.FirstOrDefault(t => !string.IsNullOrWhiteSpace(tokenString) && tokenString.Contains(t.TokenString));

            if (token is not null) return token.UserName;

            return string.Empty;
        }
    }
}
