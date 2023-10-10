using Microsoft.EntityFrameworkCore;
using Bp.Common;

var builder = WebApplication.CreateBuilder(args);

// Set up custom env file
var env = builder.Environment;

// Add additional configuration sources to builder's Configuration
builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("common.appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"common.appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<CommonDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendBpDb"));
});

var app = builder.Build();

//Auto-run migration on database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CommonDbContext>();
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
