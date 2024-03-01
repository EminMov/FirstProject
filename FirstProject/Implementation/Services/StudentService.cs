using AutoMapper;
using FirstProject.Abstractions.IRepositories;
using FirstProject.Abstractions.IUnitOfWorks;
using FirstProject.Abstractions.Services;
using FirstProject.Contexts;
using FirstProject.DTOs.SchoolDTOs;
using FirstProject.DTOs.StudentDTOs;
using FirstProject.Entities;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private ApplicationContext _dbContext;
        private IUnitOfWork _unitOfWork;
        private IRepository<School> _schoolRepo;
        private IRepository<Student> _studentsRepo;
        public StudentService(IMapper mapper, ApplicationContext context, IUnitOfWork unitofwork) 
        { 
            _mapper = mapper;
            _dbContext = context;
            _unitOfWork = unitofwork;
            _schoolRepo = _unitOfWork.GetRepository<School>();
            _studentsRepo = _unitOfWork.GetRepository<Student>();
        }
        public async Task<ResponseModel<bool>> ChangeSchool(int StudentId, int newSchoolId)
        {
            //Student student = await _unitOfWork.GetRepository<Student>().GetByIdAsync(changeStudentId);
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            try
            {
                var studentdata = await _studentsRepo.GetByIdAsync(StudentId);
                if (studentdata == null)
                {
                    responseModel.Data = false;
                    responseModel.StatusCode = 400;
                    return responseModel;
                }
                var schooldata = await _schoolRepo.GetByIdAsync(newSchoolId);
                if ( schooldata == null)
                {
                    responseModel.Data = false;
                    responseModel.StatusCode = 400;
                    return responseModel;
                }
                studentdata.SchoolId = newSchoolId;
                _studentsRepo.Update(studentdata);
                int rowaffected = await _unitOfWork.SaveChangesAsync();
                if (rowaffected > 0)
                {
                    responseModel.Data = true;
                    responseModel.StatusCode = 200;
                }
                else
                {
                    responseModel.Data = false;
                    responseModel.StatusCode = 400;
                }
            }
            catch
            {
                responseModel.Data = false;
                responseModel.StatusCode = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> ChangeSchool(ChangeSchoolDTO changeSchool)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            try
            {
                var student = await _unitOfWork.GetRepository<Student>().GetByIdAsync(changeSchool.StudentId);
                if (student == null)
                {
                    responseModel.Data = false;
                    responseModel.StatusCode = 400;
                    return responseModel;
                }
                var school = await _unitOfWork.GetRepository<School>().GetByIdAsync(changeSchool.NewSchoolId);
                if (school == null)
                {
                    responseModel.Data = false;
                    responseModel.StatusCode = 400;
                    return responseModel;
                }
                student.SchoolId = school.Id;
                _unitOfWork.GetRepository<Student>().Update(student);
                var rowAffected = await _unitOfWork.SaveChangesAsync();
                if (rowAffected > 0)
                {
                    responseModel.Data = true;
                    responseModel.StatusCode = 200;
                }
                else
                {
                    responseModel.Data = false;
                    responseModel.StatusCode = 400;
                }
            }
            catch
            {
                responseModel.Data = false;
                responseModel.StatusCode = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<StudentGetDTO>>> GetAllStudentBySchoolId(int schoolId)
        {
            ResponseModel<List<StudentGetDTO>> responseModel = new ResponseModel<List<StudentGetDTO>>();
            var data = await _unitOfWork.GetRepository<School>().GetByIdAsync(schoolId);
            try
            {
                if(data != null)
                {
                    var get = _mapper.Map<List<StudentGetDTO>>(data);
                    responseModel.Data = get;
                    responseModel.StatusCode = 200;
                }
                else
                {
                    responseModel.StatusCode=500;
                    responseModel.Data = null;
                }
            }
            catch (Exception ex)
            {
                responseModel.StatusCode = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<StudentAddDTO>> StudentAdd(StudentAddDTO newStudent)
        {
            ResponseModel<StudentAddDTO> responseModel = new ResponseModel<StudentAddDTO>();
            Student student = new Student(); 
            try
            {
                student.Surname = newStudent.Surname;
                student.FirstName = newStudent.FirstName;
                student.Age = newStudent.Age;
                var data = _unitOfWork.GetRepository<Student>().AddAsync(student);
                var savedata = await _unitOfWork.SaveChangesAsync();
                
                if (savedata > 0)
                {
                    responseModel.Data = newStudent;
                    responseModel.StatusCode = 200;
                    return responseModel;
                }
                else
                {
                    responseModel.Data = newStudent;
                    responseModel.StatusCode = 400;
                    return responseModel;
                }
            }
            catch
            {
                responseModel.Data = newStudent;
                responseModel.StatusCode = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<StudentGetDTO>>> StudentGetAll()
        {
            ResponseModel<List<StudentGetDTO>> responseModel = new ResponseModel<List<StudentGetDTO>>();
            var data = _unitOfWork.GetRepository<Student>().GetAll();
            try
            {
                if (data != null)
                {
                    var get = _mapper.Map<List<StudentGetDTO>>(data);
                    responseModel.Data = get;
                    responseModel.StatusCode = 200;
                }
                else
                {
                    responseModel.StatusCode = 500;
                    responseModel.Data = null;
                }
            }
            catch
            {
                responseModel.StatusCode = 500;
                responseModel.Data = null;
            }
            return responseModel;
        }

        public async Task<ResponseModel<StudentGetDTO>> StudentGetById(int id)
        {
            ResponseModel<StudentGetDTO> responseModel = new ResponseModel<StudentGetDTO>();
            var data = await _unitOfWork.GetRepository<Student>().GetByIdAsync(id);
            try
            {
                if(data != null)
                {
                    var get = _mapper.Map<StudentGetDTO>(data);
                    responseModel.Data = get;
                    responseModel.StatusCode = 200;
                }
                else
                {
                    responseModel.StatusCode = 500;
                    responseModel.Data = null;
                }
            }
            catch
            {
                responseModel.StatusCode = 500;
                responseModel.Data = null;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> StudentRemove(int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Student>().GetByIdAsync(id);
            var result = _unitOfWork.GetRepository<Student>().Remove(data);
            var rawAffected = await _unitOfWork.SaveChangesAsync();
            try
            {
                if (rawAffected > 0)
                {
                    responseModel.StatusCode = 200;
                    responseModel.Data = true;
                }
                else
                {
                    responseModel.StatusCode = 400;
                    responseModel.Data = false;
                }
            }
            catch 
            { 
                responseModel.StatusCode = 500;
                responseModel.Data = false;
            }
            return responseModel;
            
        }

        public async Task<ResponseModel<bool>> StudentUpdate(StudentUpdDTO studentUpdate, int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Student>().GetByIdAsync(id);
            try
            {
                if (data != null)
                {
                    data.Age = studentUpdate.Age;
                    data.Surname = studentUpdate.Surname;
                    data.FirstName = studentUpdate.FirstName;
                    await _unitOfWork.GetRepository<Student>().AddAsync(data);
                    var rowaffected = await _unitOfWork.SaveChangesAsync();
                    if(rowaffected > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Data = true;
                    }
                    else
                    {
                        responseModel.Data = false;
                        responseModel.StatusCode = 400;
                    }
                }
            }
            catch
            {
                responseModel.Data = false;
                responseModel.StatusCode = 500;
            }
            return responseModel;
        }
    }
}
