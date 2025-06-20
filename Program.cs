using Microsoft.EntityFrameworkCore;
using apiPelis.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using apiPelis.Hubs;
using apiPelis.Services;


var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddSignalR();  // Configurar SignalR
builder.Services.AddSingleton<ChatService>();  // Registrar el ChatService

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

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
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
)
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // *** AQUÍ ESTÁ EL CAMBIO CLAVE ***
                          // Debes especificar el ORIGEN EXACTO de tu frontend Angular.
                          // No uses un wildcard "*" si también estás usando AllowCredentials().
                          policy.WithOrigins("http://localhost:4200") // <-- ¡IMPORTANTE!
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); // <-- Necesario para SignalR
                      });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); // Aplicar la política definida
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub"); 
app.MapControllers();


app.Run();