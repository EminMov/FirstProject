using FirstProject.Abstractions.Services;
using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ResponseModel<bool>> CreateRole(string name)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();

            IdentityResult identityResult = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), 
                Name = name });

            if(identityResult.Succeeded) 
            {
                responseModel.Data = identityResult.Succeeded;
                responseModel.StatusCode = 200;
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                return responseModel;
            }

        }

        public async Task<ResponseModel<bool>> DeleteRole(string id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            var data = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(data);
            if(result.Succeeded)
            {
                responseModel.Data = result.Succeeded;
                responseModel.StatusCode = 200;
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                return responseModel;
            }
        }

        public async Task<ResponseModel<object>> GetAllRoles()
        {
            ResponseModel<object> responseModel = new();
            var data = await _roleManager.Roles.ToListAsync();
            if (data != null)
            {
                responseModel.Data = data;
                responseModel.StatusCode = 200;
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                return responseModel;
            }
        }

        public async Task<ResponseModel<object>> GetRoleById(string id)
        {
            ResponseModel<object> responseModel = new();
            var data = await _roleManager.FindByIdAsync(id);
            if (data != null)
            {
                responseModel.Data = data;
                responseModel.StatusCode = 200;
                return responseModel;
            }
            else
            {
                responseModel.Data = false;
                responseModel.StatusCode = 400;
                return responseModel;
            }
        }

        public async Task<ResponseModel<bool>> UpdateRole(string id, string name)
        {
            ResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 404,
            };
            var data = await _roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return responseModel;
            }
            data.Name = name;
            var result = await _roleManager.UpdateAsync(data);
            if (result.Succeeded)
            {
                responseModel.Data = true;
                responseModel.StatusCode = 200;
            }
            return responseModel;
        }
    }
}
