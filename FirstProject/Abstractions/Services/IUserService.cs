using FirstProject.DTOs.UserDTOs;
using FirstProject.Entities.Identity;
using FirstProject.Models;

namespace FirstProject.Abstractions.Services
{
    public interface IUserService
    {
        Task UpdateRefreshToken(string  refreshToken, AppUser user, DateTime accessTokenDate);
        public Task ForgetPasswordAsync(string userId, string refreshToken, string newPassword);
        public Task<ResponseModel<bool>> AssignRoleToUserAsync(string userId, string newPassword);

        public Task<ResponseModel<List<UserGetDTO>>> GetAllUserAsync();
        public Task<ResponseModel<CreateUserResponseDTO>> CreateUserAsync(CreateUserDTO newUser);
        public Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO updateUser);
        public Task<ResponseModel<bool>> DeleteUserAsync(string UserIdOrName);
        public Task<ResponseModel<string[]>> GetRolesToUserAsync(string UserIdOrName);
    }
}
