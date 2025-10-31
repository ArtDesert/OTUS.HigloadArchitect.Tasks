using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Otus.HA.AppServices.Users.Data.Dto;
using Otus.HA.AppServices.Users.Data.Requests;
using Otus.HA.AppServices.Users.Repositories;

namespace Otus.HA.AppServices.Users.Handlers;

internal class UsersHandler : IUsersHandler
{
    private readonly IUsersRepository _repository;
    private readonly IConfiguration _configuration;

    public UsersHandler(IUsersRepository repository, IConfiguration configuration)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<Guid> RegisterUserAsync(CreateUserRequest request, CancellationToken cancellationToken) =>
        await _repository.CreateAsync(request, cancellationToken);

    public async Task<string> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id,
            cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Неверный логин или пароль.");
        }

        return GenerateToken(user.Id);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.GetByIdAsync(id, cancellationToken);
    
    private string GenerateToken(Guid userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var expireMinutes = _configuration.GetValue("Jwt:ExpireMinutes", 15);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}