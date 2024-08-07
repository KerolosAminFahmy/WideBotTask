using WideBot_Task.Model;

namespace WideBot_Task.Repository.Interface
{
	public interface ICategoryRepository
	{
		Task<List<string>> GetAllCategory();
	}
}
