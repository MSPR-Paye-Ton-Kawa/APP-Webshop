using static WebShop.Components.Pages.Orders;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebShop.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Récupérer toutes les commandes
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Order>>("api/Orders");
        }

        // Ajouter une nouvelle commande
        public async Task<Order> CreateOrderAsync(Order newOrder)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Orders", newOrder);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Order>();
        }

        // Mettre à jour une commande existante
        public async Task<Order> UpdateOrderAsync(Order updatedOrder)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Orders/{updatedOrder.OrderId}", updatedOrder);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Order>();
        }

        // Supprimer une commande
        public async Task DeleteOrderAsync(int orderId)
        {
            var response = await _httpClient.DeleteAsync($"api/Orders/{orderId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
