using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tortillas.Application.UseCases.Role;
using Tortillas.Application.UseCases.Sucursal;
using Tortillas.Application.Services;
using Tortillas.Application.UseCases.Auth;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;
using Tortilleria.Infrastructure;
using Tortilleria.Infrastructure.Configuration;
using Tortilleria.Infrastructure.DataContexts;
using Tortilleria.Infrastructure.Persistence;
using Tortilleria.Infrastructure.Repositories;
using Tortilleria.Infrastructure.Services.Auth;
using Tortilleria.Infrastructure.Services.Address;
using Tortillas.Application.UseCases.Order;


var builder = WebApplication.CreateBuilder(args);

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<TortilleriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();


// Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRegistrationLinkService, RegistrationLinkService>();
// Repositorios
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();

// Handlers / UseCases
builder.Services.AddScoped<CreatePedidoHandler>();
builder.Services.AddScoped<GetPedidoHandler>();
builder.Services.AddScoped<ListPedidosHandler>();
builder.Services.AddScoped<UpdatePedidoHandler>();
builder.Services.AddScoped<DeletePedidoHandler>();
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();







// UseCases
builder.Services.AddScoped<CreateSucursalHandler>();
builder.Services.AddScoped<UpdateSucursalHandler>();
builder.Services.AddScoped<DeleteSucursalHandler>();
builder.Services.AddScoped<GetSucursalesHandler>();
builder.Services.AddScoped<RegisterUser>();
builder.Services.AddScoped<LoginUser>();
builder.Services.AddScoped<RoleUser>();
builder.Services.AddScoped<UserEmailNotificationService>();
builder.Services.AddScoped<SendRegistrationLink>();

builder.Services.AddHttpClient<INotificationService, NotificationService>(client =>
{
    var baseUrl = builder.Configuration["Notification:BaseUrl"];
    client.BaseAddress = string.IsNullOrEmpty(baseUrl)
        ? new Uri("http://localhost:5149/api/v1/Notification/")
        : new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});


// Registrar PasswordRecovery
builder.Services.AddScoped<PasswordRecovery>();

builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");
// Middlewares
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
