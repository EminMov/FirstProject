using FirstProject.Models;

namespace FirstProject.Abstractions.Services
{
    public interface IRoleService
    {
        Task<ResponseModel<object>> GetAllRoles();
        Task<ResponseModel<object>> GetRoleById(string id);
        Task<ResponseModel<bool>> CreateRole(string name);
        Task<ResponseModel<bool>> UpdateRole(string id, string name);
        Task<ResponseModel<bool>> DeleteRole(string id);
    }
}
