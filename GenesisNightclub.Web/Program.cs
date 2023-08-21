using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Mappers;
using GenesisNightclub.Infrastucture.UnitOfWork;
using GenesisNightclub.Repository.Contexts;
using GenesisNightclub.Repository.Repositories;
using GenesisNightclub.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(GenesisNightclub.Application.Interfaces.ICommand).Assembly);
//builder.Services.AddSingleton<IMemberRepository, InMemoryMemberRepository>();
//builder.Services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();
builder.Services.AddScoped<IMemberRepository, SqlMemberRepository>();
builder.Services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
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
