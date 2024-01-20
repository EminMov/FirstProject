using FirstProject.DTOs.SchoolDTOs;
using FirstProject.Entities;
using FirstProject.Models;

namespace FirstProject.Abstractions.Services
{
    public interface ISchoolService
    {
        public Task<ResponseModel<List<SchoolGetDTO>>> SchoolsGet();
        public Task<ResponseModel<SchoolGetDTO>> SchoolGetByID(int Id);
        public Task<ResponseModel<SchoolAddDTO>> SchoolAdd(SchoolAddDTO schoolAdd);
        public Task<ResponseModel<bool>> SchoolUpdate(SchoolUpdDTO schoolUpdate);
        public Task<ResponseModel<bool>> SchoolDelete(int Id);
    }
}
