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
using Tortillas.Infrastructure.Configuration;
using Tortillas.Infrastructure.DataContexts;
using Tortillas.Infrastructure.Persistence;
using Tortillas.Infrastructure.Repositories;
using Tortillas.Infrastructure.Services.Auth;
using Tortillas.Infrastructure.Services.Address;
using Tortillas.Application.UseCases.Order;
using Tortillas.Application.UseCases.Company;
using Tortillas.Application.UseCases.Address;
using Tortillas.Application.Dtos.Order;


var builder = WebApplication.CreateBuilder(args);

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    // Registrar todos los handlers de estos assemblies
    cfg.RegisterServicesFromAssemblies(
        typeof(CreateEmpresaHandler).Assembly,
        typeof(CreateSucursalHandler).Assembly,
        typeof(CreatePedidoHandler).Assembly,
        typeof(RegisterUser).Assembly,
        typeof(RoleUser).Assembly
    );
});

// DbContext
builder.Services.AddDbContext<TortillasDbContext>(options =>
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
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IDireccionRepository, DireccionRepository>();
builder.Services.AddScoped<IDetallePedidoSucursalRepository, DetallePedidoSucursalRepository>();


// UseCases
builder.Services.AddScoped<CreatePedidoHandler>();
builder.Services.AddScoped<GetPedidoByIdHandler>();
builder.Services.AddScoped<UpdatePedidoHandler>();
builder.Services.AddScoped<CreateSucursalHandler>();
builder.Services.AddScoped<UpdateSucursalHandler>();
builder.Services.AddScoped<DeleteSucursalHandler>();
builder.Services.AddScoped<GetSucursalesHandler>();
builder.Services.AddScoped<GetSucursalesHandler>();
builder.Services.AddScoped<GetSucursalByIdHandler>();
builder.Services.AddScoped<GetSucursalesByEmpresaHandler>();
builder.Services.AddScoped<CreateEmpresaHandler>();
builder.Services.AddScoped<UpdateEmpresaHandler>();
builder.Services.AddScoped<DeleteEmpresaHandler>();
builder.Services.AddScoped<GetEmpresasHandler>();
builder.Services.AddScoped<GetEmpresaByIdHandler>();
builder.Services.AddScoped<CreateDireccionHandler>();
builder.Services.AddScoped<UpdateDireccionHandler>();
builder.Services.AddScoped<DeleteDireccionHandler>();
builder.Services.AddScoped<GetDireccionesHandler>();
builder.Services.AddScoped<GetDireccionByIdHandler>();
builder.Services.AddScoped<RegisterUser>();
builder.Services.AddScoped<LoginUser>();
builder.Services.AddScoped<RoleUser>();
builder.Services.AddScoped<UserEmailNotificationService>();
builder.Services.AddScoped<SendRegistrationLink>();

builder.Services.AddScoped<GetPedidosByEmpresaHandler>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddHttpClient<INotificationService, NotificationService>(client =>
{
    var baseUrl = builder.Configuration["Notification:BaseUrl"];
    client.BaseAddress = string.IsNullOrEmpty(baseUrl)
        ? new Uri("http://localhost:5149/api/Notification/")
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
