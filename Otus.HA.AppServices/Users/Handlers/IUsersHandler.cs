using Otus.HA.AppServices.Users.Data.Dto;
using Otus.HA.AppServices.Users.Data.Requests;

namespace Otus.HA.AppServices.Users.Handlers;

/// <summary>
/// Хэндлер для работы с пользователями.
/// </summary>
public interface IUsersHandler
{
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="request">Запрос на регистрацию пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ID зарегистрированного пользователя.</returns>
    Task<Guid> RegisterUserAsync(CreateUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Упрощенный процесс аутентификации путем передачи идентификатора пользователя и получения токена для дальнейшего прохождения авторизации.
    /// </summary>
    /// <param name="request">Запрос на аутентификацию.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Токен аутентификации.</returns>
    Task<string> LoginAsync(LoginRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получение анкеты пользователя.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Анкета пользователя.</returns>
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
}