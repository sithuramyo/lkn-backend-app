using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Application.Builders;
using Application.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Persistence.Builders;
using Shared.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtSecret = builder.Configuration["Jwt:Secret"];
var allowedOrigins = builder.Configuration.GetSection("Origins:AllowedOrigins").Get<string[]>() ?? [];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var jti = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Jti);
                var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();

                var blacklisted = await cache.GetStringAsync(jti!);

                if (blacklisted != null)
                {
                    context.Fail("Token has been revoked.");
                }
            },
            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                var apiResponse = new ResponseModel<string>
                {
                    IsSuccess = false,
                    Message = "Unauthorized: Token invalid or revoked",
                    StatusCode = 401
                };

                var result = JsonSerializer.Serialize(apiResponse);

                await context.Response.WriteAsync(result);
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = key
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.InjectServices();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddDatabaseInjection(builder.Configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        var apiResponse = new ResponseModel<List<string>>
        {
            IsSuccess = false,
            Message = "Validation failed",
            StatusCode = StatusCodes.Status422UnprocessableEntity,
            Errors = errors
        };

        return new UnprocessableEntityObjectResult(apiResponse);
    };
});


#region Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "122.248.249.116:6379";
    options.InstanceName = "BlacklistJwtToken:";
});
#endregion

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler(_ => { });
app.MapGet("/", () => "Lawkanat Backend API");
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
