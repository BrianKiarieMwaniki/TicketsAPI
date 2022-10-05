namespace TicketsAPI.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {
        private List<Token> tokens = new();
        public string CreateToken(string username)
        {
            var token = new Token(username);
            tokens.Add(token);
            return "";
        }

        public bool VerifyToken(string token)
        {
            return tokens.Any(t => !string.IsNullOrWhiteSpace(token) && token.Contains(t.TokenString));
        }

        public string GetUserInfoByToken(string tokenString)
        {
            var token = tokens.FirstOrDefault(t => !string.IsNullOrWhiteSpace(tokenString) && tokenString.Contains(t.TokenString));

            if (token is not null) return token.UserName;

            return string.Empty;
        }
    }
}
