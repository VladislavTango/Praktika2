using CommonShared.Middlewares;
using Microsoft.OpenApi.Models;
using PraktikaApplication;
using PraktikaDataPersistance;
using PraktikaDomain.Interfaces;
using PraktikaInfrastructure.Email;
using PraktikaInfrastructure.JWT;
using PraktikaInfrastructure.OpenRouteService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.JwtAuthentication();

builder.Services.AddMediatRServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "¬ведите JWT токен в формате: Bearer {токен}"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>() 
        }
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IJwtTokentService, JwtTokenService>();

builder.Services.AddDatabaseContext(builder.Configuration);

builder.Services.AddHttpClient<IRouteService , RouteService>();
builder.Services.AddScoped<IMailRepository, MailRepository>();

builder.Services.AddTransient<IEmailService, EmailSenderService>();

builder.Services.AddMapperServices();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseFilter>();
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();


app.Run();
