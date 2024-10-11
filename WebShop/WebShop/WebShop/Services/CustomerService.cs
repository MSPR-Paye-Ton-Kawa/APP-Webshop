using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebShop.Components.Pages;
using static WebShop.Components.Pages.Customers;

namespace WebShop.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Récupère tous les clients
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customers");
        }

        // Récupère un client spécifique par ID
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _httpClient.GetFromJsonAsync<Customer>($"api/Customers/{customerId}");
        }

        // Récupère tous les clients avec leurs types
        public async Task<List<CustomerWithType>> GetAllCustomersWithTypesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerWithType>>("api/Customers/with-types");
        }

        // Ajoute un nouveau client
        public async Task CreateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Customers", customer);
            response.EnsureSuccessStatusCode();
        }

        // Met à jour un client existant
        public async Task UpdateCustomerAsync(int customerId, Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Customers/{customerId}", customer);
            response.EnsureSuccessStatusCode();
        }

        // Supprime un client
        public async Task DeleteCustomerAsync(int customerId)
        {
            var response = await _httpClient.DeleteAsync($"api/Customers/{customerId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
