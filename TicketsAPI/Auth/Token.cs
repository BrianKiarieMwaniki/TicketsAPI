namespace TicketsAPI.Auth
{
    public class Token
    {
        public Token(string username)
        {
            this.UserName = username;
            this.TokenString = Guid.NewGuid().ToString();
            this.ExpiryDate = DateTime.Now.AddMinutes(1);
        }

        public string TokenString { get; set; }

        public string UserName { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}