using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SULTEC_API.Data;
using SULTEC_API.Models;
using SULTEC_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = "server=localhost;database=sultec;user=root;password=root";

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<SultecContext> (opts =>
        opts.UseLazyLoadingProxies()
        .UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 39))));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SultecContext>();

builder.Services.AddScoped<UserService>();
// builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<SultecContext>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
