using ConsumeApi.Models;
using System.Net.Http;

namespace ConsumeApi.Services
{
    public class StoreApiService
    {

        private readonly HttpClient _httpclient;

        public StoreApiService(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            return await _httpclient.GetFromJsonAsync<List<CategoryDTO>>("api/Cate") ?? new List<CategoryDTO>();
        }

        public async Task<CategoryDTO?> GetCategoryAsync(int id)
        {
            return await _httpclient.GetFromJsonAsync<CategoryDTO>($"api/Cate/{id}");
        }

        // CREATE Category
        public async Task CreateCategoryAsync(CategoryDTO category)
        {
            await _httpclient.PostAsJsonAsync("api/Cate", category);
        }

        // UPDATE Category
        public async Task UpdateCategoryAsync(int id, CategoryDTO category)
        {
            await _httpclient.PutAsJsonAsync($"api/Cate/{id}", category);
        }

        // DELETE Category
        public async Task DeleteCategoryAsync(int id)
        {
            await _httpclient.DeleteAsync($"api/Categ/{id}");
        }


    }
}
