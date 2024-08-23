using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SULTEC_API.Data;
using SULTEC_API.Models;
using SULTEC_API.Repositories;
using SULTEC_API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var WebApiCorsSpecificAllows = "WebApiCorsSpecificAllows";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: WebApiCorsSpecificAllows,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new
    TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("VDN;0]DjErm<!e9k*}141)2Ez4Ah))senhasenha")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.
string connectionString = "server=localhost;database=sultec;user=root;password=root";

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<SultecContext> (opts =>
        opts.UseLazyLoadingProxies()
        .UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 39))));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SultecContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(WebApiCorsSpecificAllows);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
