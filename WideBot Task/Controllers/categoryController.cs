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

	[Authorize]
	public class categoryController(ICategoryRepository _CategoryRepo) : ControllerBase
	{
		protected APIResponse _response = new();

		[HttpGet("GetAllProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAll()
		{
			var categories = await _CategoryRepo.GetAllCategory();
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = categories;
			return Ok(_response);
		}
	}
}
