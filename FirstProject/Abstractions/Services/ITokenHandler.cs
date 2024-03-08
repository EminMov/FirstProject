using FirstProject.DTOs.TokenDTO;
using FirstProject.Entities.Identity;

namespace FirstProject.Abstractions.Services
{
    public interface ITokenHandler
    {
        public Task<TokenDTO> CreateAccessToken(AppUser user);
        public string CreateRefreshToken();
    }
}
