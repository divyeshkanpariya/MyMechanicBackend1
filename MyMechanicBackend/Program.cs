using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyMechanic.Entities.Models;
using MyMechanic.Repositories.Interface;
using MyMechanic.Repositories.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyMechanicDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<ICommonRepo<User>, CommonRepo<User>>();
builder.Services.AddScoped<ICommonRepo<Garage>,CommonRepo<Garage>>();
builder.Services.AddScoped<ICommonRepo<City>,CommonRepo<City>>();
builder.Services.AddScoped<ICommonRepo<State>,CommonRepo<State>>();
builder.Services.AddScoped<ICommonRepo<GarageAvailService>,CommonRepo<GarageAvailService>>();
builder.Services.AddScoped<ICommonRepo<ServiceType>,CommonRepo<ServiceType>>();
builder.Services.AddScoped<ICommonRepo<GarageMedia>,CommonRepo<GarageMedia>>();

builder.Services.AddScoped<IAuthRepo,AuthRepo>();
builder.Services.AddScoped<IGarageRepo,GarageRepo>();
builder.Services.AddScoped<ILocationRepo,LocationRepo>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();
