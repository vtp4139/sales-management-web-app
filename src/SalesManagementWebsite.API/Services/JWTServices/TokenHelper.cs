using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesManagementWebsite.Core.Services.JWTServices
{
    public static class TokenHelper
    {
        public static string GenerateToken(string jwtSecret, string issuer, string audience
            , List<string> userRoles, string id, string userName, string fullName)
        {
            List<Claim> authClaims = new();
            List<Claim> claimRoles = userRoles.Select(s => new Claim(AppJwtClaimTypes.Roles, s)).ToList();

            authClaims.AddRange(new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToLower()),
                new(AppJwtClaimTypes.Subject, id.ToLower()),
                new(AppJwtClaimTypes.UserName, userName),
                new(AppJwtClaimTypes.FullName, fullName),
                //new(AppJwtClaimTypes.Roles, userRoles)
            });

            authClaims.AddRange(claimRoles);

            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(jwtSecret));

            JwtSecurityToken token = new(
                issuer,
                audience,
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static DateTime GetValidTo(string jwt)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jwtSecurityToken = handler.ReadJwtToken(jwt);
            return jwtSecurityToken.ValidTo;
        }
    }
}
