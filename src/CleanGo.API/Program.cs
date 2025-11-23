using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Application.Mapping;
using CleanGo.Application.Services.Bookings;
using CleanGo.Application.Services.Cleaners;
using CleanGo.Application.Services.Services;
using CleanGo.Application.Services.Users;
using CleanGo.Infrastructure;
using CleanGo.Infrastructure.Repositories;
using CleanGo.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CleanGoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CleanGoDb")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICleanerRepository, CleanerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICleanerService, CleanerService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
