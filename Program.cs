using task4_user_managment_.Brokers.Loggings;
using task4_user_managment_.Brokers.Security;
using task4_user_managment_.Services.Foundations.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();

builder.Services.AddScoped<IRandomBroker, RandomBroker>();
builder.Services.AddScoped<IHashBroker, HashBroker>();

builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();