using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RRHH_Backend.Common.Core.Helpers;
using RRHH_Backend.Setup;
using RRHH_Backend.Domain.RRHH;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "Files"
});
builder.Services.AddControllers();

// Crear y configurar el logger para ExceptionHandler
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
    builder.SetMinimumLevel(LogLevel.Information);
});

var logger = loggerFactory.CreateLogger<Program>();
ExceptionHandler.Configure(logger);

// Configuraci n de AppSettings
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("JWTSettings"));

// Configuraci n de controladores
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Configuraci n de FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Configuraci n de HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configuraci n de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recursos Humanos ISTPET", Version = "v1" });

    // Configuraci n de autenticacion JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Configuraci n de CORS
builder.Services.AddCors(c => c.AddPolicy("Angular", builder =>
    builder.WithOrigins("http://localhost:4200")
           .AllowCredentials()
           .AllowAnyMethod()
           .AllowAnyHeader()));

// Configuraci n de l mites de solicitud
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue; // Permitir tama os grandes
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});




// Configuraci n de autenticaci n JWT (solo una vez)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWTSettings:issuer"],
        ValidAudience = builder.Configuration["JWTSettings:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Secret"]))
    };
});

// Configuraci n de la base de datos MySQL
var connectionString = builder.Configuration.GetConnectionString("SigafiContext");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexion 'SigafiContext' no esta configurada.");
}

builder.Services.AddDbContext<sigafi_esContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 21))));

// Inicializar inyeccion de dependencias
builder.Services.AddContextInfraestructure(builder.Configuration);
builder.Services.AddRepositoryInfraestructure(builder.Configuration);


var app = builder.Build();

// Configuracion de CORS
app.UseCors("Angular");

// Configurar el logger global para la aplicacion
var globalLogger = app.Services.GetRequiredService<ILogger<Program>>();
ExceptionHandler.Configure(globalLogger);

// Configuraci n del entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuracion de archivos est ticos
string uploadDirectory;

if (app.Environment.IsDevelopment())
{
    uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Files");
}
else
{
    uploadDirectory = builder.Configuration["StaticFilesPath"]
        ?? Path.Combine(Directory.GetCurrentDirectory(), "Files");
}

if (!Directory.Exists(uploadDirectory))
{
    Directory.CreateDirectory(uploadDirectory);
}

app.UseStaticFiles(); // Para servir archivos est ticos por defecto

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = "/Files"
});

// Middleware de manejo de excepciones globales
app.UseMiddleware<GlobalExceptionMiddleware>();

// Redirecci n HTTPS
app.UseHttpsRedirection();

// Autenticaci n y autorizacion
app.UseAuthentication();
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecutar la aplicacion
app.Run();