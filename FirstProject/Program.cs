using FirstProject.Abstractions.IRepositories;
using FirstProject.Abstractions.IRepositories.ISchoolRepo;
using FirstProject.Abstractions.IRepositories.IStudentRepo;
using FirstProject.Abstractions.IUnitOfWorks;
using FirstProject.Abstractions.Services;
using FirstProject.AutoMapper;
using FirstProject.Contexts;
using FirstProject.Entities.Identity;
using FirstProject.Extentions;
using FirstProject.Implementation.Repositories;
using FirstProject.Implementation.Repositories.EntityRepo;
using FirstProject.Implementation.Services;
using FirstProject.Implementation.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static FirstProject.Entities.Identity.AppUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MapperProfile));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISchoolService,  SchoolService>();
builder.Services.AddScoped(typeof(IRepository<>),typeof (Repository<>));
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExtention();

//app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
