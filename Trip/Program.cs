using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Trip.Services;
using Trip.Services.Extra;
using Trip.Services.Interfaces;
using Trip.Services.Interfaces.Extra;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi il contesto del database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100));

// Abilita CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add JWT Authentication
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
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

#region map services

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ICountriesService, CountryService>();
builder.Services.AddScoped<IExpensesService, ExpenseService>();
builder.Services.AddScoped<ISharedFilesService, SharedFileService>();
builder.Services.AddScoped<ITravelPartecipantService, TravelPartecipantService>();
builder.Services.AddScoped<ITravelService, TravelService>();
builder.Services.AddScoped<IUsefulLinkService, UsefulLinkService>();
builder.Services.AddScoped<IUserService, UserService>();

#endregion

# region app
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS prima di Authorization
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion