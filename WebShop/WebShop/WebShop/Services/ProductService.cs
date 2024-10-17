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

        // Récupérer un produit par ID
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{productId}");
        }

        // Créer un nouveau produit
        public async Task<Product> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Product>();
            }

            return null;  // Gérer les erreurs selon ton besoin
        }

        // Mettre à jour un produit existant
        public async Task UpdateProductAsync(Product product)
        {
            await _httpClient.PutAsJsonAsync($"api/products/{product.ProductId}", product);
        }

        // Supprimer un produit par ID
        public async Task DeleteProductAsync(int productId)
        {
            await _httpClient.DeleteAsync($"api/products/{productId}");
        }
    }
}
