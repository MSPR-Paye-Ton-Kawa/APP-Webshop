using WebShop.Client.Pages;
using WebShop.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using WebShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter HttpClient pour l'API Produits avec l'URL de l'API
builder.Services.AddScoped<ProductService>(sp =>
    new ProductService(new HttpClient { BaseAddress = new Uri("https://localhost:7238/") }));

// Ajouter HttpClient pour l'API Commandes
builder.Services.AddScoped<OrderService>(sp =>
    new OrderService(new HttpClient { BaseAddress = new Uri("https://localhost:7288/") }));

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebShop.Client._Imports).Assembly);

app.Run();
