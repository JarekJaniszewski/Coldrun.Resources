using Microsoft.OpenApi.Models;
using Resources.Api;
using Resources.Api.Swagger;
using Resources.Application;
using Resources.Persistence;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomHealthChecks();
builder.Services.AddCustomMediator();
builder.Services.AddFluentValidation();
builder.Services.AddColdrunEntityContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Resources API"
    });
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateTruckCommandExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<UpdateTruckCommandExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<TrucksSearchQueryCommandExamples>();
builder.Services.AddControllers().AddJsonOptions(options =>
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


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

public partial class Program { }