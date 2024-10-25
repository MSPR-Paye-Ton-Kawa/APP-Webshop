using WebShop.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using WebShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter HttpClient pour l'API Produits avec l'URL de l'API
builder.Services.AddScoped<ProductService>(sp =>
    new ProductService(new HttpClient { BaseAddress = new Uri("http://localhost:5003/") }));

// Ajouter HttpClient pour l'API Commandes
builder.Services.AddScoped<OrderService>(sp =>
    new OrderService(new HttpClient { BaseAddress = new Uri("https://localhost:7288/") }));

// Ajouter HttpClient pour l'API Clients
builder.Services.AddScoped<CustomerService>(sp =>
    new CustomerService(new HttpClient { BaseAddress = new Uri("http://localhost:5001/") }));

// Configuration des composants interactifs côté serveur et côté WebAssembly
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()  // Ajout pour le côté serveur
    .AddInteractiveWebAssemblyComponents();  // Côté WebAssembly

var app = builder.Build();

// Configure le pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Configuration des Razor Components avec le rendu interactif côté serveur et WebAssembly
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode()  // Assure le rendu interactif côté serveur
    .AddAdditionalAssemblies(typeof(WebShop.Client._Imports).Assembly);

app.Run();