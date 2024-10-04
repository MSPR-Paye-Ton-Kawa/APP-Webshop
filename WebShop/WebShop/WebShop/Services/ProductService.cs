using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static WebShop.Components.Pages.Products;

namespace WebShop.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{productId}");
        }
    }
}
