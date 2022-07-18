using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TimeTrakAPI.Context;
using TimeTrakAPI.Helpers;
using TimeTrakAPI.Repository.Contract;
using TimeTrakAPI.Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TimeTrakContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("TimeTrakConnection")));

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimeSheetService, TimeSheetService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

UserContext.Configure(((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IHttpContextAccessor>());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
