using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IDAProject.Web.Helpers
{
    public static class JwtHelpers
    {
        public static SecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            //var symmetricKey = DataHelpers.GetBytes(secretKey);
            var symmetricKey = Encoding.UTF8.GetBytes(secretKey);

            var result = new SymmetricSecurityKey(symmetricKey);
            return result;
        }

        public static TokenValidationParameters GetTokenValidationParameters(string secretKey)
        {
            var symmetricSecurityKey = GetSymmetricSecurityKey(secretKey);
            var validationParams = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = symmetricSecurityKey
            };
            return validationParams;
        }
    }
}