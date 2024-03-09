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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using static FirstProject.Entities.Identity.AppUser;
using Serilog.Core;

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],

        ValidIssuer = builder.Configuration["Token:Issuer"],

        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? DateTime.UtcNow : false,

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "School Student API",
        Description = "ASP.Net Core 6 Web API"
    });
    swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer Scheme."
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
        
    });
});

Logger? log = new LoggerConfiguration()
    .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
    .WriteTo.File("Logs/myJsonLogs.json")
    .WriteTo.File("Logs/mylogs.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sinkOptions:
    new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
    {
        TableName = "MySerilog",
        AutoCreateSqlTable = true
    },

    null, null, LogEventLevel.Warning, null,
    columnOptions: new ColumnOptions
    {
        AdditionalColumns = new Collection<SqlColumn>
        {
            new SqlColumn(columnName:"User_Id",SqlDbType.NVarChar)
        }
    },
    null, null
    )
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

Log.Logger = log;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExtention();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
