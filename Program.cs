using GlobalAuditService.Data;
using GlobalAuditService.Repositories;
using GlobalAuditService.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection
builder.Services.AddDbContext<AuditDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Add services to the container.

builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuditDbContext>();
    // Run migrations instead of EnsureCreated for Postgres
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();