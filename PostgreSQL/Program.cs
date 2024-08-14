using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSQL;
using PostgreSQL.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresDbContext>(x =>
{
    x.UseNpgsql("Host=localhost;Database=TestDB;Username=sa;Password=asdASD123");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("api/add-users/{usersNumber:int}", async (PostgresDbContext context, [FromRoute] int usersNumber) =>
{
    var users = DataGenerator.GenerateUsers(usersNumber);

    await context.Users.AddRangeAsync(users);

    await context.SaveChangesAsync();

    return Results.Ok();

});

app.MapGet("api/filter", async (PostgresDbContext context, [FromQuery] string? address, [FromQuery] int take) =>
{
    var query = context.Users.AsQueryable();

    if (!string.IsNullOrWhiteSpace(address))
    {
        query = query.Where(x => x.Address.Address1.Contains(address));
    }

    query = query.Take(take);

    var users = await query.ToListAsync();

    return Results.Ok(users);
});

app.MapPut("api/update", async (PostgresDbContext context, [FromQuery] string address1, [FromQuery] string newAddress1) =>
{
    var user = await context.Users.FirstOrDefaultAsync(x => x.Address.Address1.Contains(address1));

    if (user is null)
    {
        return Results.NotFound();
    }

    user.Address.Address1 = newAddress1;

    context.Update(user);

    await context.SaveChangesAsync();

    return Results.Ok(user);

});

app.MapGet("test1", (PostgresDbContext context) =>
{
    var filteredUsers = context.Users
    .Where(u => u.FirstName.Contains("a") &&
                u.LastName.Contains("u") &&
                u.Email.Contains("@") &&
                u.PhoneNumber.Contains("7") &&
                u.Username.Contains("a") &&
                u.Occupation.Contains("s") &&
                u.Nationality.Contains("a") &&
                u.IsActive &&
                EF.Functions.Like(u.Address.Address2, "%1%"))
    .ToList();

    return Results.Ok(filteredUsers);
});

app.UseHttpsRedirection();

app.Run();