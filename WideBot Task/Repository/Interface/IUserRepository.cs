using WideBot_Task.Model;
using WideBot_Task.Model.Dto;

namespace WideBot_Task.Repository.Interface
{
	public interface IUserRepository
	{
		Task<Token> Login(LoginRequest loginRequest);
		Task<UserDto> Register(RegisterationRequest registerationRequest);
		Task<Token> RefreshAccessToken(Token token);

		Task RevokeRefreshToken(Token token);
	}
}
