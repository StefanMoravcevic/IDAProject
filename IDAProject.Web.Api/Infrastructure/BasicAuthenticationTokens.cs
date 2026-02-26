namespace IDAProject.Web.Api.Infrastructure
{
    public class BasicAuthenticationTokens
    {
        private readonly string[] _tokens;

        public string Username => _tokens[0];
        public string Password => _tokens[1];

        public BasicAuthenticationTokens(string[] tokens)
        {
            _tokens = tokens;
        }

        public bool IsInvalid()
        {
            return ContainsTwoTokens() && ValidTokenValue(Username) && ValidTokenValue(Password);
        }

        public bool CredentialsMatch(string user, string pass)
        {
            return Username.Equals(user) && Password.Equals(pass);
        }

        private bool ValidTokenValue(string token)
        {
            return string.IsNullOrWhiteSpace(token);
        }

        private bool ContainsTwoTokens()
        {
            return _tokens.Length == 2;
        }
    }
}
