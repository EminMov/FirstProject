using AutoMapper;
using FirstProject.Abstractions.Services;
using FirstProject.Contexts;
using FirstProject.DTOs.SchoolDTOs;
using FirstProject.Entities;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IMapper _mapper;
        private ApllicationContext _dbContext; 
        public SchoolService(IMapper mapper, ApllicationContext context) 
        { 
            _mapper = mapper;
            _dbContext = context;
        }
        public async Task<ResponseModel<SchoolAddDTO>> SchoolAdd(SchoolAddDTO schoolAdd)
        {
            ResponseModel<SchoolAddDTO> response = new ResponseModel<SchoolAddDTO>();
            School school = new School();
            try 
            {
                school.SchoolName = schoolAdd.SchoolName;
                school.SchoolNumber = schoolAdd.SchoolNumber;
                var data = _dbContext.Schools.Add(school);
                var rawAffected = await _dbContext.SaveChangesAsync();
                if (rawAffected > 0)
                {
                    response.StatusCode = 201;
                    response.Data = schoolAdd;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Data = schoolAdd;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Data = null;
            }
            return response;
        }
        public async Task<ResponseModel<List<SchoolGetDTO>>> SchoolsGet()
        {
            ResponseModel<List<SchoolGetDTO>> response = new ResponseModel<List<SchoolGetDTO>>();
            var data = await _dbContext.Schools.ToListAsync();
            try
            {
                if (data != null)
                {
                    var get = _mapper.Map<List<SchoolGetDTO>>(data);
                    response.Data = get;
                    response.StatusCode = 200;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Data = null;
                }
            }
            catch
            {
                response.StatusCode = 500;
                response.Data = null;
            }
            return response;
        }

        public async Task<ResponseModel<bool>> SchoolDelete(int Id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                var data = await _dbContext.Schools.FirstOrDefaultAsync(d => d.Id == Id);
                var result = _dbContext.Remove(data);
                var rawAffected = await _dbContext.SaveChangesAsync();
                if (result != null)
                {
                    response.StatusCode = 200;
                    response.Data = true;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Data = false;
                }
            }
            catch
            {
                response.StatusCode = 500;
                response.Data = false;
            }
            return response;
        }

        public async Task<ResponseModel<SchoolGetDTO>> SchoolGetByID(int Id)
        {
            ResponseModel<SchoolGetDTO> response = new ResponseModel<SchoolGetDTO>();
            try
            {
                var data = await _dbContext.Schools.FirstOrDefaultAsync(d => d.Id == Id);
                if(data != null)
                {
                    var get = _mapper.Map<SchoolGetDTO>(data);
                    response.StatusCode = 200;
                    response.Data= get;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Data = null;
                }
            }
            catch
            {
                response.StatusCode = 500;
                response.Data = null;
            }
            return response;
        }

        public async Task<ResponseModel<bool>> SchoolUpdate(SchoolUpdDTO schoolUpdate)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _dbContext.Schools.FirstOrDefaultAsync(d => d.Id == schoolUpdate.Id);
            try
            {
                if (data != null)
                {
                    data.SchoolNumber = schoolUpdate.SchoolNumber;
                    data.SchoolName = schoolUpdate.SchoolName;

                    _dbContext.Schools.Update(data);
                    var save = await _dbContext.SaveChangesAsync();

                    if (save > 0)
                    {
                        response.StatusCode = 200;
                        response.Data = true;
                    }
                    else
                    {
                        response.StatusCode = 400;
                        response.Data = false;
                    }

                    response.StatusCode = 200;

                }
                else
                {
                    response.StatusCode = 400;
                    response.Data = false;
                }
            }
            catch
            {
                response.StatusCode = 500;
                response.Data = false;
            }

            return response;
        }
    }
}
