using ProductCatalog.Shared.DTOs;
using System.Net.Http.Json;

namespace YourBlazorApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region CATEGORIES
        // Categories
        public async Task<List<CategoryDTO>> GetCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDTO>>("https://maneroproductsapi.azurewebsites.net/api/category");
        }

        public async Task<CategoryContentDTO> GetCategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryContentDTO>($"https://maneroproductsapi.azurewebsites.net/api/category/{id}");
        }
        #endregion

        #region PRODUCTS
        // Products
        public async Task<List<ProductDetailsDTO>> GetProducts()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ProductDetailsDTO>>("https://maneroproductsapi.azurewebsites.net/api/product");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ProductDetailsDTO> GetProduct(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductDetailsDTO>($"https://maneroproductsapi.azurewebsites.net/api/product/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<ProductDTO>> GetProductByName(string name)
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDTO>>($"https://maneroproductsapi.azurewebsites.net/api/product/search/{name}");
        }

        public async Task<List<string>> GetUniqueColors()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("https://maneroproductsapi.azurewebsites.net/api/product/colors");
        }
        #endregion
    }
}
