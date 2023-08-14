using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesManagementWebsite.Core.Services.JWTServices
{
    public static class TokenHelper
    {
        public static string GenerateToken(JWTInput input)
        {
            List<Claim> authClaims = new();
            List<Claim> claimRoles = input.userRoles.Select(s => new Claim(AppJwtClaimTypes.Roles, s)).ToList();

            authClaims.AddRange(new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToLower()),
                new(AppJwtClaimTypes.UserId, input.id.ToString().ToLower()),
                new(AppJwtClaimTypes.UserName, input.userName),
                new(AppJwtClaimTypes.FullName, input.fullName),
                //new(AppJwtClaimTypes.Roles, input.userRoles)
            });

            authClaims.AddRange(claimRoles);

            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(input.jwtSecret));

            JwtSecurityToken token = new(
                input.issuer,
                input.audience,
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
