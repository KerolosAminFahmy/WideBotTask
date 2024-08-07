using System.Net.Http;
using System.Text.Json;
using WideBot_Task.Model;
using WideBot_Task.Repository.Interface;

namespace WideBot_Task.Repository
{
	public class CategoryRepository(IHttpClientFactory _httpClientFactory) : ICategoryRepository
	{
		

		public async Task<List<string>> GetAllCategory()
		{
			var client = _httpClientFactory.CreateClient("FakeStoreApi");
			var response = await client.GetAsync("products/categories");
			response.EnsureSuccessStatusCode();
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var categories = JsonSerializer.Deserialize<List<string>>
				(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			return categories;
		}
	}
}
