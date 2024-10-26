using static WebShop.Components.Pages.Orders;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Models.Order>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Models.Order>>("/Orders");
        }

        // Récupérer une commande par ID
        public async Task<Models.Order> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Models.Order>($"/Orders/{id}");
        }

        // Créer une nouvelle commande (PlaceOrder)
        public async Task<HttpResponseMessage> PlaceOrderAsync(Models.Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("/Orders/place-order", order);
            return response;
        }

        // Mettre à jour une commande
        public async Task<Models.Order> UpdateOrderAsync(int id, Models.Order order)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/Orders/{id}", order);
                response.EnsureSuccessStatusCode();

                // Récupérer la commande mise à jour
                var updatedOrder = await response.Content.ReadFromJsonAsync<Models.Order>();
                return updatedOrder;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour de la commande: {ex.Message}");
                throw;
            }
        }

        // Supprimer une commande
        public async Task<HttpResponseMessage> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/Orders/{id}");
            return response;
        }
    }
}