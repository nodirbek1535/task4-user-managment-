using Microsoft.EntityFrameworkCore;
using task4_user_managment_.Brokers.Emails;
using task4_user_managment_.Brokers.Loggings;
using task4_user_managment_.Brokers.Security;
using task4_user_managment_.Brokers.Storages;
using task4_user_managment_.Middlewares;
using task4_user_managment_.Services.Foundations.Emails;
using task4_user_managment_.Services.Foundations.Security;
using task4_user_managment_.Services.Foundations.Users;
using task4_user_managment_.Services.Orchestrations.Auth;

var builder = WebApplication.CreateBuilder(args);

// Brokers
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<IEmailBroker, EmailBroker>();

builder.Services.AddScoped<IRandomBroker, RandomBroker>();
builder.Services.AddScoped<IHashBroker, HashBroker>();

// Services
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Database
builder.Services.AddDbContext<StorageBroker>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStorageBroker, StorageBroker>();

// Controllers
builder.Services.AddControllers();

// Swagger ✅
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware ✅
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<UserStatusCheckMiddleware>();

app.MapControllers();

app.Run();