using ArticleService.Application.Dtos;
using CSharpFunctionalExtensions;

namespace ArticleService.Application.Interfaces.Services.Auth;

public interface IAuthService
{
    Task<IResult<string>> RegisterAsync(RegisterUser registerUserDto);
    Task<IResult<TokenDto>> LoginAsync(LoginUser login);
}