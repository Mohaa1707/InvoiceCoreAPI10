using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.Data;
using InvoiceCoreAPI.Mapper;
using InvoiceCoreAPI.Middleware;
using InvoiceCoreAPI.Repositories;
using InvoiceCoreAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using System.Data;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/log.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var connectionString =
        configuration.GetConnectionString("DefaultConnection");

    return new SqlConnection(connectionString);
});

builder.Services.AddScoped<IItemmasterRepository, ItemmasterRepositoriesEFSp>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositories>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepositories>();
builder.Services.AddScoped<IVendorRepository, VendorRepositories>();
builder.Services.AddScoped<IUsersRepository, UserRepositorySpDap>();
builder.Services.AddScoped<IMockAIProvider, MockAIProvider>();
builder.Services.AddScoped<IItemMasterService, ItemMasterServiceEFSp>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IUsersService, UserServiceSpDap>();
builder.Services.AddScoped<IAIService, AIService>();
builder.Services.AddAutoMapper(cfg => { }, typeof(ItemMasterProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(CategoryProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(CustomerProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(VendorProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(UsersProfile));

builder.Services.AddApiVersioning(options =>

{

    options.DefaultApiVersion = new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified = true;

    options.ReportApiVersions = true;

});

builder.Services.AddVersionedApiExplorer(options =>

{

    options.GroupNameFormat = "'v'VVV";

    options.SubstituteApiVersionInUrl = true;

});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var jwtKey = builder.Configuration["Jwt:Key"] ?? "v2UJQxTrwUCqqJkehkxvSUZKQCX6gNmRWq7q1bWa3Jw=";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "yourapiissuer";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; ;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token like: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoiceCoreAPI");
c.RoutePrefix = "swagger"; // opens at /swagger});
});


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
