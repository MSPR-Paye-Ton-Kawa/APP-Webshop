using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static WebShop.Components.Pages.Products;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Méthode pour récupérer tous les produits
        public async Task<List<Models.Product>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Models.Product>>("/products");
        }

        // Méthode pour récupérer les catégories et les fournisseurs à partir des produits
        public async Task<(List<Models.Category> categories, List<Models.Supplier> suppliers)> GetCategoriesAndSuppliersAsync()
        {
            var products = await GetAllProductsAsync();

            // Extraire les catégories uniques
            var categories = products
                .Select(p => p.Category)
                .GroupBy(c => c.CategoryId) // Utiliser categoryId pour éviter les doublons
                .Select(g => g.First())
                .ToList();

            // Extraire les fournisseurs uniques
            var suppliers = products
                .Select(p => p.Supplier)
                .GroupBy(s => s.SupplierId) // Utiliser supplierId pour éviter les doublons
                .Select(g => g.First())
                .ToList();

            return (categories, suppliers);
        }

        // Récupérer un produit par ID
        public async Task<Models.Product> GetProductByIdAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Models.Product>($"/products/{productId}");
        }

        // Créer un nouveau produit
        public async Task<Models.Product> CreateProductAsync(Models.Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("/products", product);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Produit ajouté avec succès");
                return await response.Content.ReadFromJsonAsync<Models.Product>(); // Renvoyer le produit créé
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur lors de l'ajout du produit: {response.StatusCode} - {errorContent}");

                // Optionnel : Lancer une exception ou retourner null selon ton besoin
                return null;
            }
        }

        // Mettre à jour un produit existant
        public async Task UpdateProductAsync(Models.Product product)
        {
            await _httpClient.PutAsJsonAsync($"/products/{product.ProductId}", product);
        }

        // Supprimer un produit par ID
        public async Task DeleteProductAsync(int productId)
        {
            await _httpClient.DeleteAsync($"/products/{productId}");
        }
    }
}
