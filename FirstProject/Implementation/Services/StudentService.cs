using AutoMapper;
using FirstProject.Abstractions.Services;
using FirstProject.Contexts;
using FirstProject.DTOs.StudentDTOs;
using FirstProject.Models;

namespace FirstProject.Implementation.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private ApllicationContext _dbContext;
        public StudentService(IMapper mapper, ApllicationContext context) 
        { 
            _mapper = mapper;
            _dbContext = context;
        }
        public Task<ResponseModel<bool>> ChangeSchool(int changeStudentId, int newSchoolId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> ChangeSchool(ChangeSchoolDTO changeSchool)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<StudentGetDTO>>> GetAllStudentBySchoolId(int schoolId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<StudentAddDTO>> StudentAdd(StudentAddDTO student)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<StudentGetDTO>>> StudentGetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<StudentGetDTO>> StudentGetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> StudentRemove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> StudentUpdate(StudentUpdDTO studentUpdate, int id)
        {
            throw new NotImplementedException();
        }
    }
}
