using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Mappers;
using GenesisNightclub.Repository.Contexts;
using GenesisNightclub.Repository.Interfaces;
using GenesisNightclub.Repository.Repositories;
using GenesisNightclub.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IMemberRepository, InMemoryMemberRepository>();
builder.Services.AddScoped<IMemberRepository, SqlMemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddAutoMapper(typeof(MemberProfile));
builder.Services.AddDbContext<NightclubContext>(options =>
{
    //options.EnableSensitiveDataLogging(true);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServerConnection"));
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
