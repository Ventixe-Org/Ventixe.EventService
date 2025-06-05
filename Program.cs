using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Ventixe.EventService.Data;
using Ventixe.EventService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseInMemoryDatabase("VentixeDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Events.Any())
    {
        db.Events.AddRange(
            new Event
            {
                Title = "React Meetup",
                Date = DateTime.Parse("2025-06-01"),
                Description = "Lär dig mer om React.",
                Location = "Stockholm"
            },
            new Event
            {
                Title = "Azure Workshop",
                Date = DateTime.Parse("2025-06-15"),
                Description = "Bygg molnapplikationer.",
                Location = "Göteborg"
            }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseAuthorization();
app.MapControllers();
app.Run();
