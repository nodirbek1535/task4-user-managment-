using task4_user_managment_.Brokers.Loggings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
// Add services to the container.

builder.Services.AddControllers();
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
