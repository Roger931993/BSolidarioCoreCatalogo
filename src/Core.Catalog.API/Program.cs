using Core.Catalog.API.Extensions;
using Core.Catalog.API.Middlewares;
using Core.Catalog.Application;
using Core.Catalog.Infrastructure;
using Core.Catalog.Persistence;
using Core.Catalog.Persistence.Contexts;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800; // 50 MB en bytes
});

LogManager.Setup().LoadConfigurationFromFile("Nlog.config");

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthorizeServiceJwt(builder.Configuration);

#region Si necesitas mantener las referencias pero evitar ciclos, puedes usar
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Habilitar el manejo de ciclos con ReferenceLoopHandling.Ignore
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
#endregion


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

var app = builder.Build();


// Aquí es donde debes aplicar `Migrate()`
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // ✅ Solo esto
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<CatalogCacheMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
