using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddScoped<IBaseCRUDRepo<Armour>, ArmourRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Career>, CareerRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<EntityBaseSystem>, EntityBaseSystemRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Hero>, HeroRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Inventory>, InventoryRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Monster>, MonsterRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Potion>, PotionRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<PotionType>, PotionTypeRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Race>, RaceRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<SkillRollType>, SkillRollTypeRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<Weapon>, WeaponRepo>();
builder.Services.AddScoped<IBaseCRUDRepo<WeaponType>, WeaponTypeRepo>();

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
