﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WideBot_Task.Model;
using WideBot_Task.Repository.Interface;

namespace WideBot_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepo;
		protected APIResponse _response;
		public UserController(IUserRepository userRepo)
		{
			_userRepo = userRepo;
			_response = new();
		}

		
		
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest model)
		{
			var tokenDto = await _userRepo.Login(model);
			if (tokenDto == null || string.IsNullOrEmpty(tokenDto.AccessToken))
			{

				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Username or password is incorrect");
				return BadRequest(_response);
			}
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = tokenDto;
			return Ok(_response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterationRequest model)
		{
			bool ifUserNameUnique = true;
			if (!ifUserNameUnique)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Username already exists");
				return BadRequest(_response);
			}

			var user = await _userRepo.Register(model);
			if (user == null)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Error while registering");
				return BadRequest(_response);
			}
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			return Ok(_response);
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] Token tokenDTO)
		{
			if (ModelState.IsValid)
			{
				var tokenDTOResponse = await _userRepo.RefreshAccessToken(tokenDTO);
				if (tokenDTOResponse == null || string.IsNullOrEmpty(tokenDTOResponse.AccessToken))
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					_response.ErrorMessages.Add("Token Invalid");
					return BadRequest(_response);
				}
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.Result = tokenDTOResponse;
				return Ok(_response);
			}
			else
			{
				_response.IsSuccess = false;
				_response.Result = "Invalid Input";
				return BadRequest(_response);
			}

		}

	}
}