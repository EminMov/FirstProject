using FirstProject.Abstractions.Services;
using FirstProject.DTOs.TokenDTO;
using FirstProject.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstProject.Implementation.Services
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<TokenDTO> CreateAccessToken(AppUser user)
        {
            TokenDTO tokenDTO = new();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Token:SecurityKey"]));

            SigningCredentials signingCredential = new(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            tokenDTO.Expiration = DateTime.UtcNow.AddMinutes(5);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: tokenDTO.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredential,
                claims: claims);
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenDTO.AccessToken = tokenHandler.WriteToken(securityToken);

            tokenDTO.RefreshToken = CreateRefreshToken();

            return tokenDTO;
        }

        public string CreateRefreshToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                _configuration["Token:RefreshTokenSecret"]);

            var tokenDescription = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var refreshToken = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(refreshToken);
        }
    }
}
