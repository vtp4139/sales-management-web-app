using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SalesManagementWebsite.Core.Services.BrandServices;
using SalesManagementWebsite.Core.Services.CategoryServices;
using SalesManagementWebsite.Core.Services.CustomerServices;
using SalesManagementWebsite.Core.Services.ItemServices;
using SalesManagementWebsite.Core.Services.KafkaServices;
using SalesManagementWebsite.Core.Services.OrderServices;
using SalesManagementWebsite.Core.Services.RoleServices;
using SalesManagementWebsite.Core.Services.SupplierServices;
using SalesManagementWebsite.Core.Services.UserServices;
using SalesManagementWebsite.Domain.UnitOfWork;
using SalesManagementWebsite.Infrastructure;
using SalesManagementWebsite.Infrastructure.UnitOfWork;
using System.Text;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SalesManagementDBContext>(x => x.UseNpgsql(connectionString));
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Config JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

//Dependency Injection
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddTransient(typeof(IItemServices), typeof(ItemServices));
builder.Services.AddTransient(typeof(ICategoryServices), typeof(CategoryServices));
builder.Services.AddTransient(typeof(IBrandServices), typeof(BrandServices));
builder.Services.AddTransient(typeof(ICustomerSevices), typeof(CustomerSevices));
builder.Services.AddTransient(typeof(IOrderServices), typeof(OrderServices));
builder.Services.AddTransient(typeof(IKafkaServices), typeof(KafkaServices));
builder.Services.AddTransient(typeof(ISupplierServices), typeof(SupplierServices));
builder.Services.AddTransient(typeof(IRoleServices), typeof(RoleServices));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Sale Management System - Author: Vo Tuan Phuong");

app.Logger.LogInformation("Starting the app...");

app.Run();
