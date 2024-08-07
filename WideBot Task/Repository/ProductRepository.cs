using System.Text.Json;
using WideBot_Task.Model;
using WideBot_Task.Repository.Interface;

namespace WideBot_Task.Repository
{
	public class ProductRepository(IHttpClientFactory _httpClientFactory) : IProductRepository
	{
		public async Task<List<Product>> GetAllProduct()
		{
			var client = _httpClientFactory.CreateClient("FakeStoreApi");
			var response = await client.GetAsync("products");
			response.EnsureSuccessStatusCode();
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var products = JsonSerializer.Deserialize<List<Product>>
				(jsonResponse, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			return products;
		}

		public async Task<Product> GetProduct(int id)
		{
			var client = _httpClientFactory.CreateClient("FakeStoreApi");
			var response = await client.GetAsync("products/"+id);
			response.EnsureSuccessStatusCode();
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var products = JsonSerializer.Deserialize<Product>
				(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			return products;
		}
		public async Task<List<Product>> GetProducts(int page, int pageSize, string category, float? minPrice, float? maxPrice, string name)
		{
			var client = _httpClientFactory.CreateClient("FakeStoreApi");
			var response = await client.GetAsync("products");
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var products = JsonSerializer.Deserialize<List<Product>>(jsonResponse, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			if (products == null)
			{
				return new List<Product>();
			}

			if (!string.IsNullOrEmpty(category))
			{
				products = products.Where(p => p.Category.ToLower().Contains(category.ToLower())).ToList();
			}

			if (minPrice.HasValue)
			{
				products = products.Where(p => p.price >= minPrice.Value).ToList();
			}

			if (maxPrice.HasValue)
			{
				products = products.Where(p => p.price <= maxPrice.Value).ToList();
			}

			if (!string.IsNullOrEmpty(name))
			{
				products = products.Where(p => p.Title.ToLower().Contains(name.ToLower())).ToList();
			}

			return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
		}
	}
}
