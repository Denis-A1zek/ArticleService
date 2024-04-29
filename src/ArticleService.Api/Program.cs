using System.Reflection;
using ArticleService.Api;
using ArticleService.Api.Definitios;
using ArticleService.Api.Endpoints;
using ArticleService.Application;
using ArticleService.Application.Interfaces.Services;
using ArticleService.Application.Services;
using ArticleService.Database;
using ArticleService.Database.Repository;
using ArticleService.Domain.Interfaces;
using ArticleService.Domain.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddInfrastruture();
builder.Services.AddCore();

builder.Services.AddEnpoints(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddSwaggerDefinition();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();
