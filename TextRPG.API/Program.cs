using Microsoft.EntityFrameworkCore;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO:AddScoped
builder.Services.AddScoped<IBaseCRUDRepo<Armour>, ArmourRepo>();

//Database
builder.Services.AddDbContext<TextRPG.Repository.Server.Dbcontext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

var app = builder.Build();


// CORS Policy - so 2 processes can talk to each other
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

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
