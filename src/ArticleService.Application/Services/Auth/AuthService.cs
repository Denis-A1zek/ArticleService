using System.Security.Claims;
using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services.Auth;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Interfaces;
using ArticleService.Domain.Settings;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;

namespace ArticleService.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserToken> _userTokenRepository;
    private readonly IRepository<Role> _roleRepositroy;
    
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
   
    private readonly JwtOptions _jwtOptions;
    
    public AuthService(
        IUnitOfWork unitOfWork, 
        ITokenService tokenService, 
        IPasswordHasher passwordHasher,
        IOptions<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<User>();
        _userTokenRepository = _unitOfWork.GetRepository<UserToken>();
        _roleRepositroy = _unitOfWork.GetRepository<Role>();
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<IResult<string>> RegisterAsync(RegisterUser registerUserDto)
    {
        var userInDb = await GetUserByLoginAsync(registerUserDto.Login);

        if (userInDb is not null)
            return Result.Failure<string>("User exsist");
        
        var hashUserPass = _passwordHasher.Hash(registerUserDto.Password);
        var defaultRole = await GetUserRole();
        var user = new User()
        {
            Login = registerUserDto.Login,
            Password = hashUserPass,
            RoleId = defaultRole.Id
        };

        await _userRepository.CreateAsync(user);
        await _unitOfWork.SaveAsync();
        return Result.Success(user.Login);
    }

    public async Task<IResult<TokenDto>> LoginAsync(LoginUser login)
    {
        var userInDb = await GetUserByLoginAsync(login.Login);
        
        if (userInDb is null)
            return Result.Failure<TokenDto>("User not found");

        var isValidPass = _passwordHasher.Verify(userInDb.Password, login.Password);
        if (!isValidPass)
            return Result.Failure<TokenDto>("Password invalid");
        
        var userToken = await _userTokenRepository
            .GetFirstOrDefaultAsync(u => u.UserId == userInDb.Id);

        var claims = GenerateClaimsForUser(userInDb);

        var accessToken = _tokenService.GenerateToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        if (userToken is null)
        {
            userToken = new UserToken()
            {
                UserId = userInDb.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow
                    .AddDays(_jwtOptions.RefreshTokenValidityInDays)
            };
            await _userTokenRepository.CreateAsync(userToken);
        }
        else
        {
            userToken.RefreshToken = refreshToken;
            userToken.RefreshTokenExpiryTime
                = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenValidityInDays);

            _userTokenRepository.Update(userToken);
        }

        await _unitOfWork.SaveAsync();
        return Result.Success(new TokenDto(accessToken, refreshToken));
    }

    private IList<Claim> GenerateClaimsForUser(User user)
        => new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
        };

    private async Task<Role> GetUserRole()
    {
        var role = await _roleRepositroy.GetFirstOrDefaultAsync(r => r.Name == "User");
        if (role is null)
            throw new Exception("Role not found");
        return role;
    }
    
    private Task<User> GetUserByLoginAsync(string login)
        =>  _userRepository
        .GetFirstOrDefaultAsync(u => u.Login == login, includes: u => u.Role);
}