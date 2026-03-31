using Microsoft.OpenApi.Models;
using YatriiWolrd.Infrastructure;
using YatriiWorld.Application;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Application.MappingProfiles;
using YatriiWorld.Persistance;
using YatriiWorld.Persistance.Implementations.Repositories;
using YatriiWorld.Persistance.Implementations.Services;
using YatriiWorld.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



// Dependency Injection
builder.Services
    .AddApplicationServices()
    .AddPersistenceServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC",
        policy => policy.WithOrigins("https://localhost:7085") 
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader());
});






var app = builder.Build();








if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



using (var scope = app.Services.CreateScope())
{
    await app.UseAppDbContextInitializer(scope);
}


// HTTPS Redirection
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowMVC");

// Authentication 
app.UseAuthentication();

// Authorization
app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();