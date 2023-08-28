using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WebShop.Context;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Mapping;
using WebShop.Repository;
using WebShop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebShopDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WebShopDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, WebShopDBContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddScoped<ISellerService, SellerService>();
builder.Services.AddScoped<ExceptionMiddleware>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Shop API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthorization();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var _logger = new LoggerConfiguration().WriteTo.File("C:\\Users\\Zdravko\\Desktop\\Bachelors\\WebShop\\Logs\\logs.log", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Logging.AddSerilog(_logger);

builder.Services.AddCors(o => o.AddPolicy("CORSpolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSpolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

Task orderStatus = Task.Run(async () =>
{
    while (true)
    {
        using(var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<WebShopDBContext>();
            dbContext!.Orders!.ToList().ForEach(o =>
            {
                if(o.OrderState == WebShop.Enums.OrderState.Preparing && o.isAccepted && o.DeliveryTime < DateTime.Now)
                {
                    o.OrderState = WebShop.Enums.OrderState.Delievered;
                }
            });

            dbContext!.SaveChanges();            
        }
        await Task.Delay(TimeSpan.FromMinutes(1));
    }
});

Task userAccess = Task.Run(async () =>
{
    while (true)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<WebShopDBContext>();
            dbContext!.Users!.ToList().ForEach(u =>
            {
                if (u.RegistrationTime.AddDays(7) < DateTime.Now && u.UserType == WebShop.Enums.UserType.Buyer)
                {
                    u.HasFullAccess = true;
                }
            });

            dbContext!.SaveChanges();
        }
        await Task.Delay(TimeSpan.FromHours(1));
    }
});

app.Run();