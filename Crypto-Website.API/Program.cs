using Crypto_Website.Application.Interface;
using Crypto_Website.Infrastructure.Repository;
using Crypto_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Crypto_Website.Application.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Serilog;
using Crypto_Website.API.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGeckoCoinservice, GeckoCoinserviceRepo>();
builder.Services.AddScoped<ICrypto, CryptoRepo>();
builder.Services.AddScoped<ICoinService, CoinServiceRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IFavourite,FavouriteRepository>();
builder.Services.AddScoped<IAuth, AuthRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddScoped<IWalletRepo, WalletRepo>();
builder.Services.AddScoped<IWalletTransaction, WalletTransactionRepo>();
builder.Services.AddScoped<IPortfolio, PortfolioRepo>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddHttpClient<IGeckoCoinservice, GeckoCoinserviceRepo>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["GeckoAPI:BaseURL"]);
    client.DefaultRequestHeaders.Add("x-cg-demo-api-key", config["GeckoAPI:Apikey"]);
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])),
        ClockSkew=TimeSpan.Zero
        

    };
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddRateLimiter(r => r.AddFixedWindowLimiter(policyName: "fixed", options =>
{
    options.PermitLimit = 2;
    options.Window = TimeSpan.FromSeconds(10);
    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    options.QueueLimit = 0;

}));
builder.Services.AddResponseCaching();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy => 
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
Log.Logger=new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("Logs/error-.logs",rollingInterval:RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddExceptionHandler<ExceptionHandler>();
var app = builder.Build();
app.UseRateLimiter();
app.UseResponseCaching();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });
app.UseCors("policy");
app.UseAuthentication();
app.UseAuthorization();
app.MapFallbackToFile("index.html");
app.MapControllers();

app.Run();
