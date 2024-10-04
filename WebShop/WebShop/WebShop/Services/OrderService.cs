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

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Order>>("api/Orders");
        }
    }
}
