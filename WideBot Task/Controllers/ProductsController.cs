using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WideBot_Task.Model;
using WideBot_Task.Repository.Interface;

namespace WideBot_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class ProductsController(IProductRepository _ProductRepo) : ControllerBase
	{
		protected APIResponse _response=new();

		[HttpGet("GetAllProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAll()
		{
			var products = await _ProductRepo.GetAllProduct();
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = products;
			return Ok(_response);
		}
		[HttpGet("{id:int}", Name = "GetProductById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetProductById(int id)
		{
			var product = await _ProductRepo.GetProduct(id);
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = product;
			return Ok(_response);
		}
		[HttpGet("GetProducts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetProductsWithPaginated(int page = 2, int pageSize = 10, string category = null, float? minPrice = 0, float? maxPrice = 1000, string name = null)
		{
			var products = await _ProductRepo.GetProducts(page, pageSize, category, minPrice, maxPrice, name);
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = products;
			return Ok(_response);
		}

	}
}
