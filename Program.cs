using System.Security.Claims;
using ApiIdentityEndpoint.Data;
using ApiIdentityEndpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddDbContext<AppDbContext>(
        options => options.UseSqlServer(
            "SUA_CONNECTION_STRING_AQUI")
    );

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();
app.MapSwagger();

app
    .MapGet("/", (ClaimsPrincipal user) => user.Identity!.Name)
    .RequireAuthorization();

app.MapIdentityApi<User>();

app.MapPost("/logout",
    async (SignInManager<User> signInManager, [FromBody] object empty) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    });

app.Run();