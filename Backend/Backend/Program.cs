using System.Text;
using Backend.Dal;
using Backend.Dal.Entities;
using Backend.Extensions;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using NLog;
using NLog.Web;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddDefaultIdentity<DbUserInfo>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
    builder.Services.AddIdentityServer().AddApiAuthorization<DbUserInfo, AppDbContext>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

    builder.Services.Configure<IdentityOptions>(
        options =>
        {
            options.ClaimsIdentity.RoleClaimType = nameof(DbUserInfo.Role);
            options.ClaimsIdentity.UserNameClaimType = nameof(DbUserInfo.UserName);
            options.ClaimsIdentity.UserIdClaimType = nameof(DbUserInfo.Id);
        });

    builder.Services.AddHostedService<UserInit>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ICaffService, CaffService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<ICommentService, CommentService>();

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.Converters.Add(new DateTimeConverter());
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** your JWT Bearer token on text box below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {jwtSecurityScheme, Array.Empty<string>()}
        });
    });
    builder.Services.AddSwaggerGenNewtonsoftSupport();
    //builder.Logging.ClearProviders();
    builder.Host.UseNLog(new NLogAspNetCoreOptions { RemoveLoggerFactoryFilter =false});
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseHttpsRedirection();
    }




    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();


}
// ReSharper disable once EmptyGeneralCatchClause
catch (Exception){}
finally
{
    
    LogManager.Shutdown();
}

public partial class Program { }