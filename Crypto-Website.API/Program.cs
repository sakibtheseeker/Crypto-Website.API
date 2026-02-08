using Crypto_Website.API.Exceptions;
using Crypto_Website.Application.Interface;
using Crypto_Website.Application.Mapping;
using Crypto_Website.Application.Services;
using Crypto_Website.Infrastructure.Data;
using Crypto_Website.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File("Logs/error-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Crypto Website API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<PortfolioService>();

builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<ICryptoService, CryptoService>();

builder.Services.AddHttpClient<ICryptoMarketService, CryptoMarketService>(client =>
{
    client.BaseAddress = new Uri("https://api.coingecko.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "CryptoWebsite");
});

builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
builder.Services.AddScoped<FavouriteService>();

builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<WalletService>();

builder.Services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
builder.Services.AddScoped<ITransactionsHistoryRepository, TransactionsHistoryRepository>();
builder.Services.AddScoped<TradingService>();

builder.Services.AddScoped<IPortfolioAssetRepository, PortfolioAssetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<RegisterService>();

builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
builder.Services.AddScoped <UserService>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddRateLimiter(r =>
{
    r.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 2;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine);
});

builder.Services.AddResponseCaching();

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();


app.UseExceptionHandler(_ => { });

app.UseResponseCaching();
app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularApp");
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();
app.Run();
