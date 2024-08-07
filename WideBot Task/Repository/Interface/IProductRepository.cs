using WideBot_Task.Model;

namespace WideBot_Task.Repository.Interface
{
	public interface IProductRepository
	{

		Task<List<Product>> GetAllProduct();
		Task<Product> GetProduct(int id);
		Task<List<Product>> GetProducts(int page, int pageSize, string category, float? minPrice, float? maxPrice, string name);

	}
}
