using Bp.Transaction;
using Bp.Transaction.Service.Entities;
using Bp.Common;
using Bp.Common.MssqlDB;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.Json.Serialization;
using Bp.Transaction.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Set up custom env file
var env = builder.Environment;

// Add additional configuration sources to builder's Configuration
builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("transaction.appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"transaction.appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<TransactionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendBpDb"));
});

builder.Services.AddScoped<IRepository<Account>, SqlDataRepository<Account>>();
builder.Services.AddScoped<IRepository<TransactionBalance>, SqlDataRepository<TransactionBalance>>();
builder.Services.AddScoped<DbContext, TransactionDbContext>();

var app = builder.Build();

//Auto-run migration on database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TransactionDbContext>();
    db.Database.Migrate();
}

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
