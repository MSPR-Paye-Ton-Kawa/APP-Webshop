using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebShop.Components.Pages;
using static WebShop.Components.Pages.Customers;
using WebShop.Models;

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
        public async Task<List<Models.Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Customer>>("/Customers");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des clients : {ex.Message}");
                return new List<Customer>(); // Retourne une liste vide en cas d’erreur
            }
        }

        // Récupère un client spécifique par ID
        public async Task<Models.Customer> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Customer>($"/Customers/{customerId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du client avec ID {customerId} : {ex.Message}");
                return null;
            }
        }

        // Ajoute un nouveau client
        public async Task CreateCustomerAsync(Models.Customer customer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Customers", customer);
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
        public async Task UpdateCustomerAsync(int customerId, Models.Customer customer)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/Customers/{customerId}", customer);
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
                var response = await _httpClient.DeleteAsync($"/Customers/{customerId}");
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