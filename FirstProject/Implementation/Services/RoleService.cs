using FirstProject.Abstractions.Services;
using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using static FirstProject.Entities.Identity.AppUser;

namespace FirstProject.Implementation.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public Task<ResponseModel<bool>> CreateRole(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteRole(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<object>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<object>> GetRoleById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateRole(string id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
