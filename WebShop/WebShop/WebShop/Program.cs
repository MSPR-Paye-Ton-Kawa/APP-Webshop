using WebShop.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using WebShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter HttpClient pour l'API Gateway (BaseAddress unique pour tous les services)
builder.Services.AddHttpClient("ApiGateway", client =>
{
    client.BaseAddress = new Uri("https://localhost:5000/"); // URL de l'API Gateway
});

// Ajouter ProductService, OrderService, CustomerService en utilisant le m�me HttpClient
builder.Services.AddScoped<ProductService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new ProductService(httpClientFactory.CreateClient("ApiGateway"));
});

// Ajouter HttpClient pour l'API Commandes
builder.Services.AddScoped<OrderService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new OrderService(httpClientFactory.CreateClient("ApiGateway"));
});

// Ajouter HttpClient pour l'API Clients
builder.Services.AddScoped<CustomerService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new CustomerService(httpClientFactory.CreateClient("ApiGateway"));
});

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