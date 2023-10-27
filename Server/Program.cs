using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using UserSpying.Client.Models;
using UserSpying.Shared.Models;
using UserSpying.Server.Database;
using UserSpying.Shared.Models;
using UserSpying.Client.HttpRepository.Users;

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
        try
        {
            List<User> users = await db.Users
                .Include(u => u.Gender)
                .Include(u => u.CustomFields)
                .ToListAsync();

            return Results.Ok(new Response<List<User>>()
            {
                Data = users,
                Success = true,
                Message = "",
                StatusCode = StatusCodes.Status200OK
            });
        }
        catch (Exception e)
        {
            return Results.BadRequest(new Response<int?>()
            {
                Data = null,
                Success = false,
                Message = e.Message,
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        
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
        try
        {
            await db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Results.BadRequest(new Response<int?>()
            {
                Data = null,
                Success = false,
                Message = e.Message,
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        return Results.Ok(new Response<int?>()
        {
            Data = newUser.Id,
            Success = true,
            Message = "",
            StatusCode = StatusCodes.Status200OK
        });
    }
});

app.MapPut("/users/{id}", async ([FromRoute] int id, [FromBody] UpsertUser upsertUser) =>
{
    using (var db = new DatabaseContext())
    {
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return Results.NotFound(new Response<int?>()
            {
                Data = null,
                Success = false,
                Message = "Nie odnaleziono u¿ytkownika",
                StatusCode = StatusCodes.Status404NotFound
            });
        }

        user.FirstName = upsertUser.FirstName;
        user.LastName = upsertUser.LastName;
        user.DateOfBirth = upsertUser.DateOfBirth;
        user.GenderId = upsertUser.GenderId;

        await db.SaveChangesAsync();

        return Results.Ok(new Response<int>()
        {
            Data = user.Id,
            Success = true,
            Message = "",
            StatusCode = StatusCodes.Status200OK
        });
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

        await db.CustomFields.AddAsync(newCustomField);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when ((ex.InnerException as SqliteException)?.SqliteExtendedErrorCode == 2067)
        {
            return Results.Conflict(new Response<int?>()
            {
                Data = null,
                Success = false,
                Message = $"Istnieje ju¿ pole {upsertCustomField.Name} dla u¿ytkownika o id {id}",
                StatusCode = StatusCodes.Status409Conflict
            });
        }

        return Results.Ok(new Response<int>()
        {
            Data = 0,
            Success = true,
            Message = "",
            StatusCode = StatusCodes.Status200OK
        });
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
        IEnumerable<Gender> genders = await db.Genders.ToListAsync();

        return Results.Ok(new Response<IEnumerable<Gender>?>()
        {
            Data = genders,
            Success = true,
            Message = "",
            StatusCode = StatusCodes.Status200OK
        });
    }
});

app.Run();
