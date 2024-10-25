using System;
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
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customers");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des clients : {ex.Message}");
                return new List<Customer>(); // Retourne une liste vide en cas d’erreur
            }
        }

        // Récupère un client spécifique par ID
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Customer>($"api/Customers/{customerId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du client avec ID {customerId} : {ex.Message}");
                return null;
            }
        }

        // Récupère tous les clients avec leurs types
        public async Task<List<CustomerWithType>> GetAllCustomersWithTypesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CustomerWithType>>("api/Customers/with-types");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des clients avec types : {ex.Message}");
                return new List<CustomerWithType>();
            }
        }

        // Ajoute un nouveau client
        public async Task CreateCustomerAsync(Customer customer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Customers", customer);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erreur lors de la création du client : {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la création du client : {ex.Message}");
            }
        }

        // Met à jour un client existant
        public async Task UpdateCustomerAsync(int customerId, Customer customer)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Customers/{customerId}", customer);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erreur lors de la mise à jour du client avec ID {customerId} : {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour du client avec ID {customerId} : {ex.Message}");
            }
        }

        // Supprime un client
        public async Task DeleteCustomerAsync(int customerId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Customers/{customerId}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erreur lors de la suppression du client avec ID {customerId} : {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la suppression du client avec ID {customerId} : {ex.Message}");
            }
        }
    }
}