using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Infra.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// Add services to the container.
var stringDeConexao = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PaywiseDbContextSqlite>(options =>
    options.UseLazyLoadingProxies().UseSqlite(stringDeConexao)
);
//MySQL
/*builder.Services.AddDbContext<PaywiseDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseMySql(stringDeConexao, ServerVersion.AutoDetect(stringDeConexao));
});*/

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IContaService, ContaService>();
builder.Services.AddTransient<IInvestimentoService, InvestimentoService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:issuer"],
            ValidAudience = builder.Configuration["Jwt:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
            ClockSkew = TimeSpan.Zero
        };
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "RefreshToken";
        options.Cookie.HttpOnly = true;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
