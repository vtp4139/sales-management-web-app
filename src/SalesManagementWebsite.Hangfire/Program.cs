using Hangfire;
using Hangfire.SqlServer;
using SalesManagementWebsite.Hangfire.Services;

var builder = WebApplication.CreateBuilder(args);
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config Hangfire
GlobalConfiguration.Configuration.UseSqlServerStorage(defaultConnection, new SqlServerStorageOptions
{            
    PrepareSchemaIfNecessary = true //Fix bug: Invalid object name 'HangFire.AggregatedCounter'.
});

builder.Services.AddHttpClient();

builder.Services.AddTransient(typeof(IHangfireServices), typeof(HangfireServices));

builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(defaultConnection);
});

builder.Services.AddHangfireServer();

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

app.UseHangfireDashboard();

app.Run();
