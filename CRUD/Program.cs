using BlazorCRUD.Data.Dapper.Repositories;
using CRUD.Components;
using CRUD.Data;
using CRUD.Interfaces;
using CRUD.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentAssertions.Common;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<SqlConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("sqlConnection"))
);
builder.Services.AddSingleton(new SqlConfiguration(
    builder.Configuration.GetConnectionString("sqlConnection")
));
builder.Services.AddSweetAlert2();
var app = builder.Build();
Console.WriteLine("App construida - antes de iniciar");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
