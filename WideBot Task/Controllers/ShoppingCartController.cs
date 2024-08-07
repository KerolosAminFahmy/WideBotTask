using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WideBot_Task.Data;
using WideBot_Task.Model;
using WideBot_Task.Repository.Interface;

namespace WideBot_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class ShoppingCartController(IProductRepository _ProductRepo,ApplicationDbContext _db) : ControllerBase
	{
		protected APIResponse _response = new();

		[HttpPost("{id:int}",Name="AddProductToShoppingCart")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> AddProduct(int id)
		{
			var product = await _ProductRepo.GetProduct(id);
			var UserID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (product != null)
			{

				var IsExis = _db.ShoppingCarts.SingleOrDefault(m => m.ProductId == id && m.UserId == UserID);
				if (IsExis != null) {
					IsExis.price += product.price;
					IsExis.Quantity += 1;
					_db.SaveChanges();
					_response.StatusCode = HttpStatusCode.OK;
					_response.IsSuccess = true;
					_response.Result = IsExis;
					return Ok(_response);

				}
				ShoppingCart Cart=new ShoppingCart()
				{
					UserId= UserID,
					ProductId= id,
					Quantity=1,
					price=product.price
				};


				_db.ShoppingCarts.Add(Cart);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.Result = Cart;
				return Ok(_response);
			}
			_response.StatusCode = HttpStatusCode.NotFound;
			_response.IsSuccess = false;
			return NotFound(_response);
		}
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<ActionResult<APIResponse>> UpdateProduct(int id,ShoppingCart cart)
		{
			if (!ModelState.IsValid) {
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.Result = cart;
				return BadRequest(_response);
			}
			var UserID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var IsExis = _db.ShoppingCarts.SingleOrDefault(m => m.ProductId == id && m.UserId == UserID);
			if (IsExis == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsSuccess = true;
				return NotFound(_response);

			}
			IsExis.price = cart.price;
			IsExis.Quantity=cart.Quantity;
			_db.SaveChanges();
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			return Ok(_response);
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult<APIResponse>> deleteProduct(int id, ShoppingCart cart)
		{
			
			var UserID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var IsExis = _db.ShoppingCarts.SingleOrDefault(m => m.ProductId == id && m.UserId == UserID);
			if (IsExis == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsSuccess = true;
				return NotFound(_response);

			}
			_db.ShoppingCarts.Remove(cart);
			_db.SaveChanges();
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			return Ok(_response);
		}

	}
}
