using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otus.HA.Api.Contracts.Users.Enums;
using Otus.HA.Api.Contracts.Users.Requests;
using Otus.HA.Api.Contracts.Users.Responses;
using Otus.HA.AppServices.Users.Data.Requests;
using Otus.HA.AppServices.Users.Handlers;
using LoginRequest = Otus.HA.AppServices.Users.Data.Requests.LoginRequest;

namespace Otus.HA.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersHandler _handler;

    public UsersController(IUsersHandler handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="request">Запрос на регистрацию пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ID зарегистрированного пользователя.</returns>
    [HttpPost("user/register")]
    public async Task<string> RegisterUserAsync([FromBody, Required] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var appServicesRequest = new CreateUserRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Sex = (AppServices.Users.Data.Enums.Sex)request.Sex,
            Biography = request.Biography,
            City = request.City,
            Password = request.Password,
        };

        var userGuid = await _handler.RegisterUserAsync(appServicesRequest, cancellationToken);

        return userGuid.ToString();
    }

    /// <summary>
    /// Упрощенный процесс аутентификации путем передачи идентификатора пользователя и получения токена для дальнейшего прохождения авторизации.
    /// </summary>
    /// <param name="request">Запрос на аутентификацию.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Токен аутентификации.</returns>
    [HttpPost("login")]
    public async Task<string> LoginAsync(
        [FromBody, Required] Api.Contracts.Users.Requests.LoginRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var guid))
        {
            throw new BadHttpRequestException("Некорректный ID пользователя.");
        }

        var appServicesRequest = new LoginRequest()
        {
            Id = guid,
            Password = request.Password,
        };

        return await _handler.LoginAsync(appServicesRequest, cancellationToken);
    }

    /// <summary>
    /// Получение анкеты пользователя.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Анкета пользователя.</returns>
    [Authorize]
    [HttpGet("user/get/{id}")]
    public async Task<UserProfileResponse> GetUserByIdAsync([FromRoute, Required] string id,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
        {
            throw new BadHttpRequestException("Некорректный ID пользователя.");
        }

        var user = await _handler.GetUserByIdAsync(guid, cancellationToken);

        return new UserProfileResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate,
            Sex = (Sex)user.Sex,
            Biography = user.Biography,
            City = user.City,
        };
    }
}