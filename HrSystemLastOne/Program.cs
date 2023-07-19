//using HrSystemLastOne.Filter;
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using HrSystemLastOne.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//using System.ComponentModel;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var txt = "_myAllowSpecificOrigins";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(txt,
//    builder =>
//    {
//        builder.AllowAnyOrigin(); builder.AllowAnyHeader(); builder.AllowAnyMethod();
//    });
//});
//builder.Services.Configure<SecurityStampValidatorOptions>(options =>
//{
//    options.ValidationInterval = TimeSpan.Zero;
//});

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddControllers().AddJsonOptions(options =>
     options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter()));
builder.Services.AddMvc().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ITIContext>(optios => optios.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ILeaveAttendRepository, LeaveAttendRepository>();
builder.Services.AddScoped<ISalaryReportRepository, SalaryReportRepository>();
builder.Services.AddScoped<IGeneralSettingsRepository, GeneralSettingsRepository>();
builder.Services.AddScoped<IOffDaysRepository, OffDaysRepository>();



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ITIContext>()
                .AddDefaultTokenProviders();

//Authorize 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
  
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        //ValidIssuer = Configuration["JWT:ValidIssuer"],
        //ValidateAudience = true,
        //ValidAudience = Configuration["JWT:ValidAudiance"],
        //IssuerSigningKey =
        //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
    };
});

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
});
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 5 Web API",
        Description = " ITI Projrcy"
    });

    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                });
});




var app = builder.Build();
// Add Scope
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var looggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = looggerFactory.CreateLogger("app");
try
{
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await HrSystemLastOne.Seeds.DefaultRoles.SeedAsync(roleManager);
    await HrSystemLastOne.Seeds.DefaultUsers.SeedAdminUserAsync(userManager,roleManager);
    await HrSystemLastOne.Seeds.DefaultUsers.SeedSupervisorUserAsync(userManager, roleManager);

    logger.LogInformation("Data seeded");
    logger.LogInformation("Application Started");
}
catch (System.Exception ex)
{
    logger.LogWarning(ex, "An error occurred while seeding data");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(txt);

app.UseAuthorization();

app.MapControllers();

app.Run();
