using Bp.Client;
using Bp.Client.Service.Clients;
using Bp.Client.Service.Entities;
using Bp.Common;
using Bp.Common.MssqlDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Set up custom env file
var env = builder.Environment;

// Add additional configuration sources to builder's Configuration
builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("client.appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"client.appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContextPool<ClientDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendBpDb"));
});

builder.Services.AddScoped<IRepository<Customer>, SqlDataRepository<Customer>>();
builder.Services.AddScoped<DbContext, ClientDbContext>();

var apiServicesConfig = builder.Configuration.GetSection("ApiServices");
var bpTransactionApi = apiServicesConfig["BpTransactionApi"];
// Register HttpClient with the retrieved base address
builder.Services.AddHttpClient<AccountClient>(client =>
{
    client.BaseAddress = new Uri(bpTransactionApi);
});

var app = builder.Build();

//Auto-run migration on database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientDbContext>();
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
