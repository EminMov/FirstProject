using FirstProject.DTOs.StudentDTOs;
using FirstProject.Models;

namespace FirstProject.Abstractions.Services
{
    public interface IStudentService
    {
        public Task<ResponseModel<List<StudentGetDTO>>> StudentGetAll();
        public Task<ResponseModel<StudentGetDTO>> StudentGetById(int id);
        public Task<ResponseModel<List<StudentGetDTO>>> GetAllStudentBySchoolId(int schoolId);
        public Task<ResponseModel<StudentAddDTO>> StudentAdd(StudentAddDTO student);
        public Task<ResponseModel<bool>> StudentUpdate(StudentUpdDTO studentUpdate, int id);
        public Task<ResponseModel<bool>> ChangeSchool(int changeStudentId,  int newSchoolId);
        public Task<ResponseModel<bool>> ChangeSchool(ChangeSchoolDTO changeSchool);
        public Task<ResponseModel<bool>> StudentRemove(int id);
    }
}
