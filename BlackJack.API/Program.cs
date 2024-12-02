using BlackJack.API.Data;
using BlackJack.API.Hubs;
using BlackJack.API.Services.GameService;
using BlackJack.API.Services.GameSessionService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7113")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
        });
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            options.JsonSerializerOptions.MaxDepth = 64; // optional, depending on your needs
            options.JsonSerializerOptions.ReferenceHandler = null; // Уберите обработку ссылок
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGameSessionService, GameSessionService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<GameHub>("/game");

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.Run();
