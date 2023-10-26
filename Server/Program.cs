using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using UsersSpying.Server.Database;
using UsersSpying.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

using var db = new DatabaseContext();
db.Database.EnsureCreated();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCORS", builder =>
    {
        builder.WithOrigins("https://localhost:7199")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("MyCORS");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", async () =>
{
    using (var db = new DatabaseContext())
    {
        return Results.Ok(await db.Users
            .Include(u => u.Gender)
            .Include(u => u.CustomFields)
            .ToListAsync());
    }
});

app.MapGet("/users/{id}", async ([FromRoute]int id) =>
{
    using (var db = new DatabaseContext())
    {
        User? foundUser = await db.Users
            .Include(u => u.Gender)
            .Include(u => u.CustomFields)
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        return foundUser != null ? Results.Ok(foundUser) : Results.NotFound();
    }
});

app.MapPost("/users", async ([FromBody] UpsertUser upsertUser) =>
{
    using (var db = new DatabaseContext())
    {
        User newUser = new User()
        {
            GenderId = upsertUser.GenderId,
            FirstName = upsertUser.FirstName,
            LastName = upsertUser.LastName,
            DateOfBirth = upsertUser.DateOfBirth
        };

        db.Users.Add(newUser);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.MapPost("/users/{id}/custom-fields", async ([FromRoute] int id, [FromBody] UpsertCustomField upsertCustomField) =>
{
    using (var db = new DatabaseContext())
    {
        //TODO: Check if user with ID from route exists.
        CustomField newCustomField = new CustomField()
        {
            UserId = id,
            Name = upsertCustomField.Name,
            Value = upsertCustomField.Value
        };

        db.CustomFields.Add(newCustomField);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when ((ex.InnerException as SqliteException)?.SqliteExtendedErrorCode == 2067)
        {
                return Results.Conflict($"Istnieje ju¿ pole {upsertCustomField.Name} dla u¿ytkownika o id {id}");
        }

        return Results.NoContent();
    }
});

app.MapPut("/custom-fields/{id}", async ([FromRoute] int id, [FromBody] UpsertCustomField upsertUstomField) =>
{
    using (var db = new DatabaseContext())
    {
        CustomField? foundCustomField = await db.CustomFields
            .Where(cf => cf.Id == id)
            .FirstOrDefaultAsync();

        if (foundCustomField == null)
        {
            return Results.NotFound();
        }

        foundCustomField.Name = upsertUstomField.Name;
        foundCustomField.Value = upsertUstomField.Value;

        db.CustomFields.Update(foundCustomField);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.MapGet("/genders", async () =>
{
    using (var db = new DatabaseContext())
    {
        return Results.Ok(await db.Genders.ToListAsync());
    }
});

app.Run();
