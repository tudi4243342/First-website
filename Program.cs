using First_website.Backend;
using First_website.Data;
using First_website.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure SQLite Database using Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=fruitshop.db"));

// 2. Allow your Frontend files to talk to this backend (CORS Policy)
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors();

// AUTOMATICALLY CREATE DATABASE ON STARTUP
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    // Seed database with initial fruit objects if empty
    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Name = "Tom" },
            new User { Name = "Anton" }
        );
        db.SaveChanges();
    }
}

// 3. API ENDPOINTS

// GET: Fetch all fruits (For customers)
app.MapGet("/api/fruits", async (AppDbContext db) =>
    await db.Users.ToListAsync());

// PUT: Update stock values (For the moderator)
app.MapPut("/api/fruits/{id}", async (int id, string newName, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user == null) return Results.NotFound();

    user.Name = newName; // Modifying the C# object
    await db.SaveChangesAsync();    // EF Core saves it back to SQLite

    return Results.Ok(user);
});

app.Run();