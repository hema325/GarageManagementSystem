using GMS.API.Data;
using GMS.API.Extensions.DependencyInjection;
using GMS.API.Handlers;
using GMS.API.Services.JWT;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers();

// services
builder.Services
    .AddEndpointsApiExplorer() 
    .AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Local")))
    .AddAutoMapper(Assembly.GetExecutingAssembly())
    .ConfigureApiBehaviorOptions()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddSwaggerService()
    .AddJWTService(builder.Configuration)
    .AddCorsService(builder.Configuration)
    .AddHttpContextAccessor();

// dependency injection
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IJWTManager, JWTManager>();

// settings
builder.Services
    .Configure<JWTSettings>(builder.Configuration.GetSection(JWTSettings.SectionName))
    .Configure<CorsSettings>(builder.Configuration.GetSection(CorsSettings.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseStatusCodePagesWithReExecute("/api/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
