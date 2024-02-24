using FirstProject.Abstractions.Services;
using FirstProject.AutoMapper;
using FirstProject.DTOs.SchoolDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SchoolController : ControllerBase
{
    private readonly ISchoolService _schoolService;

    public SchoolController( ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    [HttpGet]
    public async Task<IActionResult> SchoolsGet()
    {
        var data = await _schoolService.SchoolsGet();
        return StatusCode(data.StatusCode, data);
    }
    [HttpGet]
    public async Task<IActionResult> SchoolGetById([FromQuery] int id)
    {
        var data = await _schoolService.SchoolGetByID(id);
        return StatusCode(data.StatusCode, data);
    }
    [HttpPost]
    public async Task<IActionResult> SchoolAdd([FromBody]SchoolAddDTO model)
    {
        var data = await _schoolService.SchoolAdd(model);
        return StatusCode(data.StatusCode, data);
    }
    [HttpPut]
    public async Task<IActionResult> SchoolUpdate([FromBody]SchoolUpdDTO model)
    {
        var data = await _schoolService.SchoolUpdate(model);
        return StatusCode(data.StatusCode, data);
    }
    [HttpDelete]
    public async Task<IActionResult> SchoolDelete([FromQuery]int id)
    {
        var data = await _schoolService.SchoolDelete(id);
        return StatusCode(data.StatusCode, data);
    }
}


