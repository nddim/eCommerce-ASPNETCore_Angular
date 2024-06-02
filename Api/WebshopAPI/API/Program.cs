using Microsoft.EntityFrameworkCore;
using WebshopApi.Data;
using WebAPI.Services;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.Auth.EmailSlanje;
using WebAPI.Helpers.Auth.Loggeri;
using WebAPI.Helpers.Auth.PrijavaLogger;
using WebAPI.Helpers.Auth.Loggeri.Interfacei;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Services.Google;
using WebAPI.Services.JwtHeader;
using WebAPI.Services.RefreshToken;
using WebAPI.Services.Quartz;
using WebAPI.Helpers.Report;
using WebAPI.Helpers.SignalR;
using Vonage.Extensions;
using WebAPI.Services.Sms;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));

// Add services to the container.

builder.Services.AddIdentity<Korisnik, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase=false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromSeconds(0));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
//    .AddCookie(x =>
//{
//    x.Cookie.Name = "token";
//})
    .AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        //ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,

        ValidAudiences = new List<string>
        {
            builder.Configuration.GetSection("Jwt:Audience").Value,
            builder.Configuration.GetSection("Jwt:Audience2").Value,
        },
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!)),
        //TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!)),
        ClockSkew = TimeSpan.Zero
    };

})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration.GetSection("Google:ClientId").Value;
    googleOptions.ClientSecret = builder.Configuration.GetSection("Google:ClientSecret").Value;
})
;

//za angular 1. dio
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            //.AllowAnyOrigin()
            .WithOrigins(new string[] { "http://localhost:4200", "https://localhost:7110" })
            //.WithMethods("GET", "POST", "DELETE", "PUT", "TRACE")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
            //.WithExposedHeaders("refresh");

    });
});

builder.Services.AddScoped<IEmailService, EmailSenderService>();
builder.Services.AddScoped<ILoggerPrijava, PrijavaLogger>();
builder.Services.AddScoped<ILoggerOdjava, OdjavaLogger>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IGoogleService, GoogleService>();
builder.Services.AddScoped<IJwtHeaderService, JwtHeaderService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IRacunGenerator, RacunGenerator>();
builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(x => x.OperationFilter<AutorizacijaSwaggerHeader>());
builder.Services.AddSwaggerGen(x => x.OperationFilter<RefreshTokenHeader>());
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Unesite validan token",
        Name = "Autorizacija",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="BeaRer",
                In=ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddInfrastructure();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<MyAuthService>();
builder.Services.AddTransient<EmailSenderService>();
builder.Services.AddVonageClientTransient(builder.Configuration);


builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   
//}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors();

//app.UseCookiePolicy();
//za angular 2. dio - POSLIJE REDIRECTION


app.UseAuthentication(); //dodana linija
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHub<SignalRHub>("/hub-putanja");
app.Run();
