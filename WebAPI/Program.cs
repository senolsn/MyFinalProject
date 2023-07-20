using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //appsetings.js'deki token optionslar� okumakla g�revlidir.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader();
                      });
});


//Di�er DI(Dependency Injection) nesnelerimizi buraya ta��m�� olduk. K�saca Core katman�ndaki IoC yap�s� burada.
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Autofac Container Yap�land�rma :
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule()); //AutofacBusinessModule, uygulaman�z�n ba��ml�l�klar�n� ve bunlar�n nas�l ��z�mlenece�ini tan�mlayabilece�iniz bir s�n�ft�r.
});



//App'ler Middleware'leri temsil eder. Gelen istekleri i�leme ve yan�t �retme bile�enlerdir.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();