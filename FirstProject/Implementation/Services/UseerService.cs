using AutoMapper;
using FirstProject.Abstractions.Services;
using FirstProject.DTOs.UserDTOs;
using FirstProject.Entities.Identity;
using FirstProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FirstProject.Implementation.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Task<ResponseModel<bool>> AssignRoleToUserAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<CreateUserResponseDTO>> CreateUserAsync(CreateUserDTO newUser)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteUserAsync(string UserIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task ForgetPasswordAsync(string userId, string refreshToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<UserGetDTO>>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<string[]>> GetRolesToUserAsync(string UserIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
