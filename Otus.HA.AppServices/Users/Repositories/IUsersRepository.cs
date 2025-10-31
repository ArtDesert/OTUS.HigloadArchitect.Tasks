using Otus.HA.AppServices.Users.Data.Dto;
using Otus.HA.AppServices.Users.Data.Requests;

namespace Otus.HA.AppServices.Users.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями.
/// </summary>
public interface IUsersRepository
{
    /// <summary>
    /// Создаёт нового пользователя.
    /// </summary>
    /// <param name="request">Запрос на создание нового пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ID созданного пользователя.</returns>
    Task<Guid> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает пользователя по его ID.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}